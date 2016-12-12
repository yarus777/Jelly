using UnityEngine;
using System.Collections;

public class Line : CacheTransform {
	private float scaleXStraight = 0.2f;
	private float scaleXDiagonal = 0.25f;
	// Use this for initialization
	void Start () {
		
	}

	public void Delete()
	{
		DestroyImmediate (gameObject);
	}

	public void ChangeScaleX(LineScale type)
	{
		switch(type)
		{
			case LineScale.Straight:
				transform.localScale = new Vector3 (scaleXStraight, transform.localScale.y, transform.localScale.z);
				break;
			case LineScale.Diagonal:
				transform.localScale = new Vector3 (scaleXDiagonal, transform.localScale.y, transform.localScale.z);
				break;
		}

	}
}