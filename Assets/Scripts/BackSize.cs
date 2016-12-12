using UnityEngine;
using System.Collections;

public class BackSize : MonoBehaviour {

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
	/// <summary>
	/// Масштабирование подложки под камеру
	/// </summary>
	private void SizeWidth()
	{
		float ortographicSize = Camera.main.orthographicSize;
		float aspect = Camera.main.aspect;
		float sizeUnitX = ortographicSize*aspect *2;
		float zoneScaleW = GetComponent<SpriteRenderer>().sprite.bounds.size.x*transform.localScale.x;
		float coefUnits = zoneScaleW/sizeUnitX;
		transform.localScale = new Vector3 (1f/coefUnits, 1f/coefUnits, 1f);
	}

}
