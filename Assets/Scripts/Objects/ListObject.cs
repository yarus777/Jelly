using UnityEngine;
using System.Collections;

public class ListObject : CacheTransform {

//	private Vector3 startPos;
	private Vector3 toPos;
	private float speed = 0.02f;
	public SpriteRenderer sR;
	private float rotationSpeed;


	// Use this for initialization
	void Start () {
	}

	public void StartAnimation(StateAnimation direction)
	{
		float randomX = Random.Range(10,25);
		float randomY = Random.Range(2,6);

		switch(direction)
		{
			case StateAnimation.Forward:
				break;
			case StateAnimation.Back:
				break;
		}

		toPos = new Vector3 (transform.position.x+randomX, transform.position.y-randomY, transform.position.z);
		rotationSpeed = Random.Range (-5.0f, 5.0f);
		MoveAnimation ();
	}

	public void MoveAnimation()
	{
		if(transform.position.y>toPos.y)
		{
			if(sR.color.a>=speed)
			{
				sR.color-= new Color(0,0,0,0.001f);
			}
			transform.Rotate(0,0,rotationSpeed);
			transform.position = Vector3.MoveTowards (transform.position, toPos, speed);
			Invoke("MoveAnimation",GamePlay.timePhysics);
		}
		else
		{
			Destroy(gameObject);
		}
	}

}
