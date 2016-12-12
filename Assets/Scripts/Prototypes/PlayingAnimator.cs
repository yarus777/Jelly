using UnityEngine;
using System.Collections;

public class PlayingAnimator : MonoBehaviour {
	public Animator animatorController;

	// Use this for initialization
	void Start () {
		if(animatorController!=null)
		{

			Invoke("PlayAnimatonRandomFrame", GamePlay.timePhysics);
		}
	}

	void PlayAnimatonRandomFrame()
	{
		animatorController.enabled = true;
	}
}
