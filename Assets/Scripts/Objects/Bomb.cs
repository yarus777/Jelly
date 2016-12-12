using UnityEngine;
using System.Collections;

public class Bomb : CacheTransform, IBomb {
	private Properties property;

	private bool active = false;

//	private Animator animator;

//	public SpriteRenderer bombSR;
	public SpriteRenderer lightSR;
	private SpriteRenderer effectSR;

	public Animator bombCreateAnim;
	public Animator bombIdleAnim;

	void Awake()
	{
		property = GetComponent<Properties>();
		property.iBomb = this;
		property.iColor = new ColorObject();
		property.iPoints = new Points (PointManager.bomb);
//		animator = GetComponentInChildren<Animator> ();

		foreach(SpriteRenderer sr in GetComponentsInChildren<SpriteRenderer>())
		{
			switch(sr.name)
			{
				case "Light":
					lightSR = sr;
					break;
				case "EffectBomb":
					effectSR = sr;
					break;
			}
		}
		Invoke ("IdleAnimation", 0.25f);
	}

	public void IdleAnimation()
	{
		bombCreateAnim.enabled = false;
		bombIdleAnim.enabled = true;
	}

	#region IBomb implementation

	public void DeleteObject ()
	{
		DestroyImmediate (gameObject);
	}

	public void PrepareDelete ()
	{
		property.isMoving = true;
		Invoke("StartDeleting", property.delayDelete);
	}

	public void StartDeleting()
	{
		AnimationBoom ();
		property.AddPoints ();
		property.AnimationScore();
	}

	private void AnimationBoom()
	{
		//Debug.Log ("property.iColor.GetColor() "+ property.iColor.GetColor ());
		GameObject boom = Instantiate(GameData.pool.GetObject(ObjectTypes.BoomBomb, property.iColor.GetColor())) as GameObject;
		boom.transform.position = transform.position;

		if(GamePlay.bonusTime)
		{
			if(!GamePlay.oneShotBomb)
			{
				GamePlay.soundManager.CreateSoundType (SoundsManager.SoundType.Bomb);
				GamePlay.oneShotBomb = true;
			}
		}
		else
		{
			GamePlay.soundManager.CreateSoundType (SoundsManager.SoundType.Bomb);
			GamePlay.oneShotBomb = true;
		}

		Animator animator = boom.GetComponentInChildren<Animator> ();
		animator.SetTrigger("boom");


	}

	public bool stateActive {
		set
		{
			active = value;			
		}
		get
		{
			return active;		
		}
	}

	public void Active (StateObjects state)
	{
		switch(state)
		{
			case StateObjects.Selected:
				lightSR.enabled = true;
				effectSR.enabled = true;
				break;
			case StateObjects.Normal:
				lightSR.enabled = false;
				effectSR.enabled = false;
				break;
		}
	}
	#endregion


}
