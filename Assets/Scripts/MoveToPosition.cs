using UnityEngine;
using System.Collections;

public class MoveToPosition : MonoBehaviour {
	private Vector3 startPos;
	private Vector3 toPos;
	public float startTime;
	private float speed;
	private float coefSpeed = 3.2f;

	// Use this for initialization
	void Start () {
	
	}

	public void PrepareMove(Vector3 position)
	{
		startPos = transform.position;
		toPos = new Vector3 (position.x, position.y, transform.position.z);
		startTime = Time.time;
		Move ();	
	}

	public void Move()
	{
		transform.position = Vector3.Lerp (startPos, toPos, (Time.time-startTime)*coefSpeed);
		Invoke ("Move", GamePlay.timePhysics);
	}
}
