namespace Assets.Scripts.MyScripts.Gates {
    using System;
    using Lives;
    using Popups;
    using UnityEngine;
    using UnityEngine.UI;

    public enum GateState {
        Locked,
        Waiting,
        Opened
    }

    internal class Gate : MonoBehaviour {
        [SerializeField]
        private int _levelNumber;

        [Header("Unlock Time")]
        [SerializeField]
        private int _hours;

        [SerializeField]
        private int _minutes;

        [SerializeField]
        private int _seconds;

        [Header("Visual State")]
        [SerializeField]
        private Button _unlockButton;

        [SerializeField]
        private GatesTimer _timer;

        private TimeSpan _unlockTime;
        private GateState _state;

        public int Level {
            get { return _levelNumber; }
        }

        private void Awake() {
            _unlockTime = new TimeSpan(_hours, _minutes, _seconds);
            ChangeState(GateState.Locked);
        }

        public void OnClick() {
            if (_state != GateState.Waiting) {
                return;
            }
            PopupsController.Instance.Show(PopupType.UnlockGates);
        }

        public event Action<Gate> Opened;

        private void ChangeState(GateState newState) {
            if (newState == _state) {
                return;
            }
            _state = newState;
            switch (newState) {
                case GateState.Locked:
                    _timer.StopTimer();
                    UpdateVisualState();
                    break;
                case GateState.Waiting:
                    _timer.StartTimer((float) _unlockTime.TotalSeconds);
                    UpdateVisualState();
                    break;
                case GateState.Opened:
                    if (Opened != null) {
                        Opened(this);
                    }
                    break;
            }
        }

        private void UpdateVisualState() {
            _unlockButton.interactable = _state == GateState.Waiting;
        }

        public class State {
            public readonly GateState Status;
            public readonly float TimeLeftValue;
            public readonly long SavingTime;

            public State(GateState state, float timeLeftValue = 0, long savingTime = 0) {
                Status = state;
                TimeLeftValue = timeLeftValue;
                SavingTime = savingTime;
            }

            public State() {
                Status = GateState.Locked;
                //TimeLeftValue = LIVES_REFILL_INTERVAL;
                SavingTime = LivesManager.GetTimestamp(DateTime.UtcNow);
            }

            public JSONObject ToJSON() {
                var json = new JSONObject();
                json.AddField("state", (int) Status);
                if (Status == GateState.Waiting) {
                    json.AddField("timer", TimeLeftValue);
                    json.AddField("time", SavingTime.ToString());
                }
                return json;
            }

            public static State FromJson(JSONObject json) {
                var status = (GateState) Enum.Parse(typeof (GateState), json["state"].ToString().Trim('\"'), true);
                if (status != GateState.Waiting) {
                    return new State(status);
                }
                var timer = float.Parse(json["timer"].ToString().Trim('\"'));
                var time = long.Parse(json["time"].ToString().Trim('\"'));
                return new State(status, timer, time);
            }
        }
    }
}