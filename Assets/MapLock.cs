using UnityEngine;
using System.Collections;

public class MapLock : MonoBehaviour {
    public Gate[] gates;
    public Gate activeGate;
    void Awake() {
        GamePlay.mapLocker = this;
    }
    public void UpdateGate() {
        foreach (var item in gates) {
            item.UpdateState();
        }
    }
}
