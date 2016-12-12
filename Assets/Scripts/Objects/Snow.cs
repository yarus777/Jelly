using UnityEngine;
using System.Collections;

public class Snow : CacheTransform, ISnow {
	private Properties property;

	void Awake()
	{
		property = GetComponent<Properties>();
		property.iSnow = this;
		property.iColor = new ColorObject ();
		property.iPoints = new Points (PointManager.snow);
	}

	#region ISnow implementation

	public void DeleteObject ()
	{
		if(!property.isDelete)
		{
			property.AddPoints ();
			property.AnimationScore();
			GameField.ReplaceObject (property, ObjectTypes.Ice, property.iColor.GetColor ());
		}
	}

	#endregion
}
