using UnityEngine;
using System.Collections;

public class SoundStarOpen : MonoBehaviour {

	// Use this for initialization
	void Awake () 
	{
		GamePlay.soundManager.CreateSoundTypeUI (SoundsManager.UISoundType.StarOpen, false);
	}
}
