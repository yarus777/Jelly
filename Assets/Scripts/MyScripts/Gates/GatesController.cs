namespace Assets.Scripts.MyScripts.Gates {
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    internal class GatesController : MonoBehaviour {
        private List<GateUI> _gates;

        public void Load() {
            _gates = GetComponentsInChildren<GateUI>().ToList();
            var gates = GatesStorage.Instance.Gates.ToList();
            foreach (var gate in _gates) {
                var data = gates.FirstOrDefault(x => x.Level == gate.Level);
                if (data == null) {
                    Destroy(gate.gameObject);
                    continue;
                }
                gate.StateChanged += OnGateStateChanged;
                gate.Init(data);
            }
        }

        private void OnGateStateChanged(GateUI gate, GateState gateState) {
            if (gateState == GateState.Opened) {
                _gates.Remove(gate);
                Destroy(gate.gameObject);
            }
        }
    }
}