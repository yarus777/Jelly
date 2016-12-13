namespace Assets.Scripts.MyScripts.Gates {
    using System.Linq;
    using UnityEngine;

    internal class GatesController : MonoBehaviour {
        private GateUI[] _gates;

        public void Load() {
            _gates = GetComponentsInChildren<GateUI>();
            var gates = GatesStorage.Instance.Gates.ToList();
            foreach (var gate in _gates) {
                var data = gates.FirstOrDefault(x => x.Level == gate.Level);
                if (data == null) {
                    Destroy(gate.gameObject);
                    continue;
                }
                gate.Init(data);
            }
        }
    }
}