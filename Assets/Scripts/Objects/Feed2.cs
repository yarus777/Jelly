using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Feed2 : CacheTransform, IFeed2 {
	protected Properties property;
	private int currentHp;
//	private int maxHp;
	private int countSpriteHp;
	private TextMesh textCountHp;

	void Awake () {
		property = GetComponent<Properties>();
		property.iFeed2 = this;
		property.iColor = new ColorObject();
		property.iPoints = new Points (PointManager.feed2);
		textCountHp = GetComponentInChildren<TextMesh> ();
	}


	#region IFeed2 implementation

	public void Attack ()
	{
		if(!property.isDelete)
		{
			if(currentHp-1>0)
			{
				SetCurHp(currentHp - 1);

			}
			else
			{
				textCountHp.text = "";
				property.isDelete = true;
				property.isMoving = true;
				property.AddPoints ();
				property.AnimationScore ();
				GamePlay.AddTaskValue (Task.Feed2, 1);
			}
		}
	}

	public int GetCurHp()
	{
		return currentHp;
	}

	public void SetCurHp (int value)
	{
		currentHp = value;
		UpdateCountHp ();
	}

	public void SetMaxHp (int value)
	{
//		maxHp = value;
	}

	public void UpdateCountHp ()
	{
		textCountHp.text = currentHp.ToString();
	}

	public void PrepareDelete()
	{
		DeleteObject ();
	}

	#endregion



	private void DeleteObject()
	{
		MonoBehaviour.DestroyImmediate (gameObject);
	}
}
