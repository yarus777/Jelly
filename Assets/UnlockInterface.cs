using UnityEngine;
using System.Collections;

public class UnlockInterface : MonoBehaviour {
    public TextMesh timer;
	// Use this for initialization
	void Awake () {
        GamePlay.unlockInterface = this;
	}
    void FixedUpdate()
    {
        timer.text = GamePlay.mapLocker.activeGate.SecondsToHHMMSS((int)GamePlay.mapLocker.activeGate.ostTime);
    }
}
