using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Dig : CacheTransform, IBlackHero {
	private Properties property;

	private GameObject boom = null;
	private float startTimeAnimation;
	private float timeAnimation = 0.333f;

//	private bool oneDelete = true;

	private SpriteRenderer spriteRenderer;
	public int countHP;

	public List<Sprite> states;

	void Awake()
	{
		property = GetComponent<Properties>();
		property.iBlackHero = this;
		property.iColor = new ColorObject ();
		property.iPoints = new Points (PointManager.blackHero);
		spriteRenderer = GetComponent<SpriteRenderer> ();
	}
	// Update is called once per frame
	void Update () {
	
	}

	#region IBlackHero implementation

	public void Attack(int count)
	{
		if(!property.isDelete)
		{
			if(countHP-count>0)
			{
				if(!GamePlay.oneShotDigAttack)
				{
					GamePlay.soundManager.CreateSoundType(SoundsManager.SoundType.DigAttack);
					GamePlay.oneShotDigAttack = true;
				}
				SetHp(countHP - count);
			}
			else
			{
				PrepareDelete();
			}
		}
	}

	public void SetHp (int value)
	{
		countHP = value;
		UpdateTexture ();
	}

	public void PrepareDelete ()
	{
		if(!property.isDelete)
		{
			if(!GamePlay.oneShotDigDrop)
			{
				GamePlay.soundManager.CreateSoundType(SoundsManager.SoundType.DigDrop);
				GamePlay.oneShotDigDrop = true;
			}
//			Debug.Log("Prepare dig delete");
			GamePlay.AttackStringDig(property);
			property.isMoving = true;
			Invoke ("StartDeleting", property.delayDelete);
			property.isDelete = true;

		}

	}

	public void DeleteObject()
	{
		property.AddPoints ();
//		GamePlay.AddTaskValue (Task.ClearBHero, 1);
		DestroyImmediate (gameObject);
	}

	public void Visible(bool state)
	{
		spriteRenderer.enabled = state;
	}

	#endregion

	private void UpdateTexture()
	{
		if(countHP>0)
		{
			spriteRenderer.sprite = states [countHP - 1];
		}
	}

	private void StartDeleting()
	{
		AnimationBoom();
	}

	private void AnimationBoom()
	{
//		foreach(SpriteRenderer spriteRenderer in GetComponentsInChildren<SpriteRenderer>())
//		{
//			spriteRenderer.enabled = false;
//		}
//		
//		boom = Instantiate (GameData.pool.GetObject(ObjectTypes.Boom, property.iColor.GetColor())) as GameObject;
//		boom.transform.parent = GameField.parentObject.transform;
//		boom.transform.localPosition = transform.localPosition;
//		boom.GetComponent<Animator>().SetTrigger("boom");
//		startTimeAnimation = Time.time;
		Invoke("WaitAnimationBoom", timeAnimation);
	}

	private void WaitAnimationBoom()
	{
//		if(Time.time - startTimeAnimation < timeAnimation)
//		{
//			Invoke("WaitAnimationBoom", 0f);
//			return;
//		}
		DestroyImmediate (boom);
		property.AnimationScore ();
	}
}
