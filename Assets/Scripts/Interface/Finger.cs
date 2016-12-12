using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Finger : CacheTransform {
	private List<Vector3> coordinates;
	private int number;
//	private float speed = 0.02f;
	private float speed = 0.075f;
	private float speedHighLight = 0.0075f;
	private bool blockHighLight = false;

	private float delayResume = 1.5f;

	void Awake () {
	}

	public void InitCoordinate(List<Vector3> coordinates)
	{
		number = 1;
		this.coordinates = coordinates;
		transform.position = coordinates [0];
		transform.GetComponent<Renderer>().material.color = new Color (1f, 1f, 1f, 1f);
	}

	public void InitCoordinate()
	{
		number = 1;
		transform.position = coordinates [0];
		transform.GetComponent<Renderer>().material.color = new Color (1f, 1f, 1f, 1f);
	}

	public void PlayAnimation()
	{
		CancelInvoke ();
		MoveCoordinate ();
	}

	public void MoveCoordinate()
	{
		if(number>coordinates.Count-1)
		{
//			Invoke ("Wait", 0f);
			Wait();
			return;
		}
		if(Vector3.Distance(transform.position, coordinates[number])<0.02f)
		{
			transform.position = coordinates[number];
			number++;
			Invoke ("MoveCoordinate", GamePlay.timePhysics);
			return;
		}

		if(!blockHighLight)
		{
			if(number==coordinates.Count-1)
			{
//				Invoke ("HightLight", 0f);
				HightLight();
			}
		}

		transform.position = Vector3.MoveTowards(transform.position, coordinates[number], speed);
		Invoke ("MoveCoordinate", GamePlay.timePhysics);
	}

	private void Wait()
	{

		Invoke ("StartMove", 1f);
	}

	private void StartMove()
	{
		CancelInvoke (); 
		InitCoordinate();
		blockHighLight = false;
//		Invoke ("MoveCoordinate", 0f);
		MoveCoordinate ();
	}

	private void HightLight()
	{
		if(transform.GetComponent<Renderer>().material.color.a>0)
		{
			transform.GetComponent<Renderer>().material.color = new Color(transform.GetComponent<Renderer>().material.color.r, 
			                                              transform.GetComponent<Renderer>().material.color.g, 
			                                              transform.GetComponent<Renderer>().material.color.b,
			                                              transform.GetComponent<Renderer>().material.color.a-speedHighLight);
			Invoke ("HightLight", 0.1f);
		}
		else
		{
			blockHighLight = true;
		}
	}

	public void Pause()
	{
		transform.GetComponent<Renderer>().material.color = new Color (1f, 1f, 1f, 0f);
		CancelInvoke ();
	}

	public void PrepareResume()
	{
		CancelInvoke ("Resume");
		Invoke ("Resume", delayResume);
	}

	private void Resume()
	{
		InitCoordinate ();
		PlayAnimation ();
	}

	public void DestroyFinger()
	{
		Destroy (gameObject, 0f);
	}
}
