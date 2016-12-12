using UnityEngine;
using System.Collections;

public class ListManager : MonoBehaviour {

	public GameObject listObject;
	public StateAnimation direction;
	public float timeNewAnimation;

	// Use this for initialization
	void Start () {
		StartAnimation ();
	}

	// Update is called once per frame
	void Update () {
	
	}

	void StartAnimation()
	{
		UpdateAnimation ();
	}

	void CreateList()
	{
		GameObject obj = Instantiate (listObject) as GameObject;
		obj.transform.parent = listObject.transform.parent;
		obj.transform.localPosition = listObject.transform.localPosition;
		obj.transform.localRotation = listObject.transform.localRotation;
		obj.GetComponent<SpriteRenderer> ().enabled = true;
		obj.GetComponent<ListObject> ().StartAnimation (direction);
	}

	void UpdateAnimation()
	{
		CreateList ();
		float random = Random.Range (0, timeNewAnimation);
		Invoke ("UpdateAnimation", random);
	}
}
