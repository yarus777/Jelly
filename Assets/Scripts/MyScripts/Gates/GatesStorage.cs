namespace Assets.Scripts.MyScripts.Gates {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Lives;
    using UnityEngine;

    internal class GatesStorage : UnitySingleton<GatesStorage> {
        private const string KEY = "gates_data";
        private const string FILENAME = "gates";
        public Gate CurrentGates { get; private set; }

        public IEnumerable<Gate> Gates {
            get { return _gates; }
        }

        private List<Gate> _gates;

        protected override void LateAwake() {
            base.LateAwake();
            _gates = new List<Gate>();
            var initialGatesData = new JSONObject(Resources.Load<TextAsset>(FILENAME).text).list.Select(x => Parse(x));
            long savingTime;
            var savedGatesData = LoadGates(out savingTime);
            foreach (var gateData in initialGatesData) {
                Gate gate;
                var timer = gameObject.AddComponent<Timer>();
                var savedData = savedGatesData.Find(x => x.Level == gateData.Level);
                if (savedData != null) {
                    var timePassed = LivesManager.GetTimestamp(DateTime.UtcNow) - savingTime;
                    gate = new Gate(gateData.Level, timer, savedData.Status,
                        TimeSpan.FromSeconds(savedData.TimeLeftValue - timePassed));
                } else {
                    gate = new Gate(gateData.Level, timer, GateState.Locked,
                        TimeSpan.FromSeconds(gateData.TimeLeftValue));
                }
                gate.StateChanged += OnGateStateChanged;
                _gates.Add(gate);
            }
            CurrentGates = _gates.FirstOrDefault(x => x.Status == GateState.Waiting);
            
        }

        public event Action<Gate, GateState> StateChanged;

        private void OnGateStateChanged(Gate gate, GateState gateState)
        {
            if (gateState == GateState.Opened)
            {
                CurrentGates = null;
            }
            if (StateChanged != null)
            {
                StateChanged(gate, gateState);
            }
        }

        public void OnCurrentLevelChanged(int currentLevel) {
            foreach (var gate in _gates) {
                gate.SetCurrentLevel(currentLevel);
            }
            CurrentGates = _gates.FirstOrDefault(x => x.Status == GateState.Waiting);
        }

        private Gate.State Parse(JSONObject json) {
            var level = int.Parse(json["level"].ToString().Trim('\"'));
            var timeString = json["time"].ToString().Trim('\"');
            var regex = new Regex(@"(?<days>\d{2}):(?<hours>\d{2}):(?<minutes>\d{2}):(?<seconds>\d{2})");
            var match = regex.Match(timeString);
            var timeLeft = new TimeSpan(
                int.Parse(match.Groups["days"].Value),
                int.Parse(match.Groups["hours"].Value),
                int.Parse(match.Groups["minutes"].Value),
                int.Parse(match.Groups["seconds"].Value));
            return new Gate.State(level, GateState.Locked, (float) timeLeft.TotalSeconds);
        }

        protected override void OnDestroy() {
            base.OnDestroy();
            SaveGates();
        }

        private List<Gate.State> LoadGates(out long savingTime) {
            var list = new List<Gate.State>();
            savingTime = 0;
            if (!PlayerPrefs.HasKey(KEY)) {
                return list;
            }
            var json = new JSONObject(PlayerPrefs.GetString(KEY));
            savingTime = long.Parse(json["time"].ToString().Trim('\"'));
            return json["gates"].list.Select(x => Gate.State.FromJson(x)).ToList();
        }

        private void SaveGates() {
            var gatesJson = new JSONObject(JSONObject.Type.ARRAY);
            foreach (var gate in _gates) {
                gatesJson.Add(gate.Save().ToJSON());
            }
            var json = new JSONObject();
            json.AddField("time", LivesManager.GetTimestamp(DateTime.UtcNow).ToString());
            json.AddField("gates", gatesJson);
            PlayerPrefs.SetString(KEY, json.ToString());
            PlayerPrefs.Save();
            Debug.Log("SAVE: " + json);
        }
    }
}