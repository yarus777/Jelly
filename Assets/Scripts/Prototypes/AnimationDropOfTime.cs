using UnityEngine;
using System.Collections;

public class AnimationDropOfTime : CacheTransform {
	public float time;
	// Use this for initialization
	void Start () {
		Destroy(gameObject, time);
	}

}
