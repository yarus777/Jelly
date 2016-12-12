using UnityEngine;
using System.Collections;

public class CameraSize : MonoBehaviour {

	public enum SideStreight
	{
		Width,
		Height
	}

	public SideStreight side;

	// Use this for initialization
	void Start () {
		switch(side)
		{
			case SideStreight.Width:
				SizeWidth();
				break;
		}
	}

	private void SizeWidth()
	{
//		float sWidth = Screen.width;
//		float sHeight = Screen.height;
		float ortographicSize = Camera.main.orthographicSize;
		float aspect = Camera.main.aspect;
//		float sizeUnitY = ortographicSize * 2;
//		Debug.Log ("Scale Y:"+sizeUnitY);
		float sizeUnitX = ortographicSize*aspect *2;
//		Debug.Log ("Scale X:"+sizeUnitX);
		float zoneScaleW = GetComponent<SpriteRenderer>().sprite.bounds.size.x*transform.localScale.x;
		float coefUnits = zoneScaleW/sizeUnitX;
//		Debug.Log("CoefUnits: "+coefUnits);
		Camera.main.orthographicSize *= coefUnits;
	}
}
