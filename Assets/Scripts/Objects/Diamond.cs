using UnityEngine;
using System.Collections;

public class Diamond : CacheTransform, IDiamond{
	private Properties property;
	private float speedAnimation = 1.15f;
	private Animator animator;

	private SpriteRenderer spriteRenderer;

	void Awake()
	{
		property = GetComponent<Properties>();
		property.iDiamond = this;
		property.iPoints = new Points (PointManager.diamond);

		spriteRenderer = GetComponentInChildren<SpriteRenderer> ();
		animator = GetComponentInChildren<Animator> ();
	}

	#region IBrilliant implementation
	
	public bool IsLastPostionJ ()
	{
		return GameData.manager.ReturnIJPosObject (property) [1] == 0; 
	}
	
	public void PrepareDelete ()
	{
		property.isMoving = true;
//		foreach(SpriteRenderer spriteRenderer in GetComponentsInChildren<SpriteRenderer>())
//		{
//			spriteRenderer.enabled = false;
//		}
		StartAnimation ();
		Invoke ("DeleteObject", speedAnimation);
	}

	private void StartAnimation()
	{
		if(!GamePlay.oneShotPot)
		{
			GamePlay.soundManager.CreateSoundType (SoundsManager.SoundType.Pot);
			GamePlay.oneShotPot = true;
		}

		animator.SetTrigger ("open");

	}

	public void DeleteObject()
	{
		property.AnimationScore ();
		GameData.diamondManager.DeleteCurrentCountDiamond ();
		GameData.manager.DeleteObject (property);
		GamePlay.AddTaskValue (Task.Diamond, 1);
		property.AddPoints ();
		DestroyImmediate (gameObject);
	}

	public void Visible(bool state)
	{
		spriteRenderer.enabled = state; 
	}

	#endregion
}
