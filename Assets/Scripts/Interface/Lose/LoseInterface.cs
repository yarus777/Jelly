using UnityEngine;
using System.Collections;

public class LoseInterface : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GamePlay.soundManager.CreateSoundTypeUI (SoundsManager.UISoundType.WindowLevelLose, false);
		//AdSDK.ShowFullscreen();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
