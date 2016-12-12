using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ice : CacheTransform, IIce {
	private Properties property;
	//private Animator[] animators;
	private float speedAnimation = 0.25f;

	private SpriteRenderer[] spriteRenderers;
//	private Animator plate;
//	private Animator glass;

//	enum Controller
//	{
//		Glass,
//		Plate
//	}

	void Awake()
	{
		property = GetComponent<Properties>();
		property.iIce = this;
		property.iColor = new ColorObject ();
		property.iPoints = new Points (PointManager.ice);

		//animators = GetComponentsInChildren<Animator> ();

		spriteRenderers = GetComponentsInChildren<SpriteRenderer> ();
	}

	#region IIce implementation

	public void PrepareDelete (float delay)
	{
		if(!property.isDelete)
		{
			property.isMoving = true;
			property.isDelete = true;
			Invoke("StartDelete", delay);
		}
	}

	public void StartDelete()
	{
		GamePlay.AddTaskValue (Task.Save, 1);
		StartAnimation();
		property.AddPoints ();
		Invoke("DeleteObject", speedAnimation);
		property.AnimationScore ();
	}

	private void StartAnimation()
	{
		if(!GamePlay.oneShotIce)
		{
			GamePlay.soundManager.CreateSoundType(SoundsManager.SoundType.Ice);
			GamePlay.oneShotIce = true;
		}

//		foreach(Animator animator in animators)
//		{
//			animator.SetTrigger("boom");
//		}


	}

	public void DeleteObject()
	{
		GameField.ReplaceObject (property, ObjectTypes.Jelly, property.iColor.GetColor ());
	}

	public void Visible(bool state)
	{
		foreach(SpriteRenderer spriteRenderer in spriteRenderers)
		{
			spriteRenderer.enabled = state;
		}
	}

	#endregion


}
