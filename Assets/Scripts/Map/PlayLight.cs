using UnityEngine;
using System.Collections;

public class PlayLight : MonoBehaviour {

	public float timePlay;


	void Awake () {
		Invoke ("Play", timePlay);
	}

	void Play()
	{
		GetComponent<Animator> ().enabled = true;
	}

}
