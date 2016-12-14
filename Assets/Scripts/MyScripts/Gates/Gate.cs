namespace Assets.Scripts.MyScripts.Gates {
    using System;
    using Lives;

    public enum GateState {
        Locked,
        Waiting,
        Opened
    }

    internal class Gate {
        private GateState _state;

        public readonly int Level;
        private readonly Timer _timer;

        public event Action<Gate, GateState> StateChanged;

        public Gate(int levelNumber, Timer timer, GateState state, TimeSpan timeLeft) {
            Level = levelNumber;
            _timer = timer;
            _timer.Tick += OnTimer;
            _timer.Interval = (float) timeLeft.TotalSeconds;
            if (state == GateState.Waiting && timeLeft.TotalSeconds <= 0) {
                ChangeState(GateState.Opened);
            }
            else {
                ChangeState(state);
            }
        }

        public void SetCurrentLevel(int currentLevel) {
            if (currentLevel == Level) {
                ChangeState(GateState.Waiting);
            }
            else if (currentLevel > Level) {
                ChangeState(GateState.Opened);
            }
        }

        public void AddTime(TimeSpan interval) {
            _timer.AddTime(interval);
        }

        public override string ToString() {
            return string.Format("Gates: level {0}, state {1}, time: {2}", Level, _state, _timer.TimeLeft);
        }

        public State Save() {
            return new State(Level, _state, (float)_timer.TimeLeft.TotalSeconds);
        }

        public GateState Status {
            get { return _state; }
        }

        public TimeSpan TimeLeft { get { return _timer.TimeLeft; } }

        private void ChangeState(GateState newState) {
            if (newState == _state) {
                return;
            }
            _state = newState;
            switch (_state) {
                case GateState.Waiting:
                    _timer.StartTimer();
                    break;
                case GateState.Opened:
                    _timer.Interval = 0;
                    break;
                default: 
                    _timer.StopTimer();
                    break;
            }
            if (StateChanged != null) {
                StateChanged.Invoke(this, newState);
            }
        }

        private void OnTimer() {
            ChangeState(GateState.Opened);
        }

        public class State {
            public readonly int Level;
            public readonly GateState Status;
            public readonly float TimeLeftValue;

            public State(int level, GateState state, float timeLeftValue) {
                Level = level;
                Status = state;
                TimeLeftValue = timeLeftValue;
            }

            public JSONObject ToJSON() {
                var json = new JSONObject();
                json.AddField("state", (int) Status);
                json.AddField("level", Level);
                json.AddField("timer", TimeLeftValue);
                return json;
            }

            public static State FromJson(JSONObject json) {
                var status = (GateState) int.Parse(json["state"].ToString().Trim('\"'));
                var level = int.Parse(json["level"].ToString().Trim('\"'));
                var timer = float.Parse(json["timer"].ToString().Trim('\"'));
                return new State(level, status, timer);
            }
        }
    }
}