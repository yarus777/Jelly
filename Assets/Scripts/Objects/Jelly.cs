using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Jelly : CacheTransform, IJelly {
	protected Properties property;
	private GameObject boom;
	private GameObject save;
//	private float startTimeAnimationBoom;
	private float timeAnimationBoom = 0.25f;
	private float timeAnimationSave = 0.5f;	


	private new AnimationObject animation;

	private SpriteRenderer spriteRenderer;

	public Animator animatorJelly;
	public GameObject effect;
	private List<GameObject> effects = new List<GameObject>();

	// Use this for initialization
	void Awake () {
		property = GetComponent<Properties>();
		property.iJelly = this;
		property.iColor = new ColorObject();
		property.iPoints = new Points (PointManager.jelly);
		spriteRenderer = GetComponentInChildren<SpriteRenderer> ();
	}

	void Start()
	{
		foreach(Animator animator in GetComponentsInChildren<Animator> ())
		{
			switch(animator.transform.name)
			{
				case "Jelly":
					animatorJelly = animator;
					break;
			}
		}
	}

	#region IJelly implementation

	public void SetStatePicture(StateObjects state)
	{
		if(animatorJelly!=null)
		{
			switch (state) 
			{
				case StateObjects.Selected:
					AnimationEffects();
					animatorJelly.SetTrigger (StateObjects.Selected.ToString());
					animatorJelly.ResetTrigger (StateObjects.Normal.ToString());
					break;
				case StateObjects.Normal: 
					animatorJelly.ResetTrigger (StateObjects.Selected.ToString());
					animatorJelly.SetTrigger (StateObjects.Normal.ToString());
					break;
			}
		}

	}

	public void PrepareDelete ()
	{
		property.isMoving = true;
		Invoke("StartDeleting", property.delayDelete);
	}

	public void DeleteObject()
	{
		property.AddPoints ();
		//GamePlay.DeleteJam(property);
		property.CreatePUs ();
		DestroyImmediate(gameObject);
	}

	public void Visible(bool state)
	{
		spriteRenderer.enabled = state;
	}

	#endregion

	public void StartDeleting()
	{
		AnimationBoom ();
	}

	#region Animation Boom
	private void AnimationBoom()
	{

		if(property.deleteLine)
		{
			GamePlay.soundManager.CreateDeSelect(SoundsManager.Duration.Up);
		}

		foreach(SpriteRenderer spriteRenderer in GetComponentsInChildren<SpriteRenderer>())
		{
			spriteRenderer.enabled = false;
		}
		boom = Instantiate (GameData.pool.GetObject(ObjectTypes.Boom, property.iColor.GetColor())) as GameObject;
		boom.transform.parent = GameField.parentObject.transform;
		boom.transform.localPosition = transform.localPosition;
		boom.GetComponent<Animator>().SetTrigger("boom");
//		startTimeAnimationBoom = Time.time;
		Invoke("WaitAnimationBoom", timeAnimationBoom);

	}

	private void WaitAnimationBoom()
	{
//		if(Time.time - startTimeAnimationBoom < timeAnimationBoom)
//		{
//			Invoke("WaitAnimationBoom",0f);
//			return;
//		}
		DestroyImmediate (boom);
		property.AnimationScore ();
		GamePlay.DeleteJam(property);
	}
	#endregion 

	public void AnimationSave(Properties saveProperty)
	{
		this.property.isMoving = true;
		foreach(SpriteRenderer spriteRenderer in GetComponentsInChildren<SpriteRenderer>())
		{
			spriteRenderer.enabled = false;
		}
		save = Instantiate (GameData.pool.GetObject(ObjectTypes.Feed2Save, property.iColor.GetColor())) as GameObject;
		save.transform.parent = GameField.parentObject.transform;
		save.transform.localPosition = transform.localPosition;
		save.GetComponent<Feed2Save> ().PrepareDelete (saveProperty);
		DeleteObjectOfTime (timeAnimationSave);

//		save.GetComponentInChildren<Animator>().SetTrigger("save");
//		startTimeAnimationSave = Time.time;
//		WaitAnimationSave ();
	}

	private void DeleteObjectOfTime(float time)
	{
		Destroy(gameObject, time);
	}

	private void AnimationEffects()
	{
		//EffectCreate ();
//		switch(property.iColor.GetColor())
//		{
//			case Colors.Fiolet:
//				EffectFiolet();
//				break;
//			case Colors.Yellow:
//				EffectYellow();
//				break;
//			case Colors.Red:
//				EffectRed();
//				break;
//			case Colors.Blue:
//				EffectBlue();
//				break;
//			case Colors.Green:
//				EffectGreen();
//				break;
//		}
	}

	private void EffectCreate()
	{
		if(effect!=null)
		{
			if(effects.Count>2)
			{
				Destroy(effects[0]);
				effects.RemoveAt(0);
			}
			GameObject obj = Instantiate (effect) as GameObject;
			obj.transform.parent = property.transform.parent;
			obj.transform.localPosition = property.transform.localPosition;
			effects.Add (obj);
		}
	}

//	private void EffectYellow()
//	{
//		if(effects.Count>2)
//		{
//			Destroy(effects[0]);
//			effects.RemoveAt(0);
//		}
//		GameObject obj = Instantiate (effect) as GameObject;
//		obj.transform.parent = property.transform.parent;
//		obj.transform.localPosition = property.transform.localPosition;
//		obj.transform.localPosition += new Vector3 (0f, 0.6f, 0f);
//		effects.Add (obj);
//	}
//
//	private void EffectRed()
//	{
//		if(effects.Count>2)
//		{
//			Destroy(effects[0]);
//			effects.RemoveAt(0);
//		}
//		GameObject obj = Instantiate (effect) as GameObject;
//		obj.transform.parent = property.transform.parent;
//		obj.transform.localPosition = property.transform.localPosition;
//		obj.transform.localPosition += new Vector3 (0f, 0.6f, 0f);
//		effects.Add (obj);
//	}
//
//	private void EffectBlue()
//	{
//	}

	private void EffectGreen()
	{
	}

//	private void WaitAnimationSave ()
//	{
//		if(Time.time - startTimeAnimationSave < timeAnimationSave)
//		{
//			Invoke("WaitAnimationSave", 0f);
//			return;
//		}
//		if(saveProperty!=null)
//		{
//			saveProperty.iFeed2.Attack();
//		}
//
//		DestroyImmediate (save);
//		DestroyImmediate (gameObject);
//	}
}
