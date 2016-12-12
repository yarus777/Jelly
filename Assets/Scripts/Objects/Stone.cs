using UnityEngine;
using System.Collections;

public class Stone : CacheTransform, IStone {
	private Properties property;
	private SpriteRenderer spriteRenderer;
	
	// Use this for initialization
	void Awake () {
		property = GetComponent <Properties>();
		property.iStone = this;
		property.iPoints = new Points (PointManager.stone);
		spriteRenderer = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	#region IStone implementation

	public void DeleteObject ()
	{
		property.AddPoints ();
		property.AnimationScore ();
		GameField.ReplaceObject (property, ObjectTypes.BlackJelly, Colors.Empty);
		//DestroyImmediate (gameObject);
	}
	public void Visible(bool state)
	{
		spriteRenderer.enabled = state;
	}

	#endregion
}
