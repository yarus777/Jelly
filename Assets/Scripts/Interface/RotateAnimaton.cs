using UnityEngine;
using System.Collections;

public class RotateAnimaton : CacheTransform {
	public StateAnimation state;
	public float speed = 1f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(state == StateAnimation.Back)
		{
			transform.Rotate (0, 0, speed);
		}
		else if(state == StateAnimation.Forward)
		{
			transform.Rotate (0, 0, -speed);
		}

	}
}
