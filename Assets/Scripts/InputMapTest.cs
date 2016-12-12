using UnityEngine;
using System.Collections;

public class InputMapTest : MonoBehaviour {
	private float speedMove = 5f;
	private float speedZoom = 1f;
//	private float lastDistance = 0;
//	private float startCameraSize;
	// Use this for initialization
	void Start () {
//		startCameraSize = Camera.main.orthographicSize;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.touchCount == 1)
		{
			if(Input.GetTouch(0).phase == TouchPhase.Moved)
			{
				transform.position-=new Vector3(Input.GetTouch(0).deltaPosition.x*speedMove*Time.deltaTime, 
				                                Input.GetTouch(0).deltaPosition.y*speedMove*Time.deltaTime, 0);
			}
		}
//		if(Input.touchCount == 2)
//		{
//			if(Input.GetTouch(0).phase == TouchPhase.Began && Input.GetTouch(1).phase == TouchPhase.Began)
//			{
//				lastDistance = Vector2.Distance(Input.GetTouch(0).deltaPosition, Input.GetTouch(1).deltaPosition);
//			}
////			if(Input.GetTouch(0).phase == TouchPhase.Moved && Input.GetTouch(1).phase == TouchPhase.Moved)
////			{
////				if(Vector2.Distance(Input.GetTouch(0).deltaPosition, Input.GetTouch(1).deltaPosition) - lastDistance>0)
////				{
////					Camera.main.orthographicSize -= speedZoom*Time.deltaTime;
////				}
////				else
////				{
////					Camera.main.orthographicSize += speedZoom*Time.deltaTime;
////				}
////				lastDistance = Vector2.Distance(Input.GetTouch(0).deltaPosition, Input.GetTouch(1).deltaPosition);
////			}
//		}
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}
	}

	void OnGUI()
	{
		if(GUI.Button(new Rect(0,0,50,50), "+"))
		{
			Camera.main.orthographicSize -= speedZoom;
		}
		if(GUI.Button(new Rect(0,50,50,50), "-"))
		{
			Camera.main.orthographicSize += speedZoom;
		}
	}
}
