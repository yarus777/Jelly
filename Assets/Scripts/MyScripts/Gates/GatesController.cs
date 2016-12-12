namespace Assets.Scripts.MyScripts.Gates {
    using UnityEngine;

    internal class GatesController : MonoBehaviour {
        private Gate[] _gates;
        public Gate CurrentGates { get; private set; }

        public void Load(int maxCompleteLevel) {
            _gates = GetComponentsInChildren<Gate>();
            foreach (var gate in _gates) {
                gate.Opened += OnGateOpened;
            }
            InitGates(maxCompleteLevel);
        }

        private void InitGates(int maxOpenedLevel) {
            foreach (var gate in _gates) {
            }
        }

        private void OnDestroy() {
            if (_gates != null) {
                foreach (var gate in _gates) {
                    gate.Opened -= OnGateOpened;
                }
            }
        }

        private void OnGateOpened(Gate gate) {
        }
    }
}