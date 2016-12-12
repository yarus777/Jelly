using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Electro : CacheTransform, IElectro {
	private Properties property;

	private new bool active = false;

	public SpriteRenderer electroSR;
	public SpriteRenderer lightSR;
	private SpriteRenderer effectSR;

	void Awake()
	{
		property = GetComponent<Properties>();
		property.iElectro = this;
		property.iColor = new ColorObject();
		property.iPoints = new Points (PointManager.electro);

		foreach(SpriteRenderer sr in GetComponentsInChildren<SpriteRenderer>())
		{
			switch(sr.name)
			{
				case "Electro":
					electroSR = sr;
					break;
				case "Light":
					lightSR = sr;
					break;
				case "EffectElectro":
					effectSR = sr;
					break;
			}
		}
	}

	// Update is called once per frame
	void Update () {
	
	}


	#region IElectro implementation

	public void PrepareDelete ()
	{
		property.isMoving = true;
		Invoke("StartDeleting",property.delayDelete);
	}

	public void DeleteObject()
	{
		property.AddPoints ();
		DestroyImmediate (gameObject);
	}

	public bool stateActive 
	{
		set
		{
			active = value;			
		}
		get
		{
			return active;		
		}
	}

	public void Active(StateObjects state)
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

	private void StartDeleting()
	{
		electroSR.enabled = false;
		lightSR.enabled = false;

		GameObject boom = Instantiate(GameData.pool.GetObject(ObjectTypes.Lightning, 0)) as GameObject;
//		boom.transform.position = transform.position;  
		boom.transform.position = new Vector3 (transform.position.x, transform.position.y, -1);

		if(GamePlay.bonusTime)
		{
			if(!GamePlay.oneShotElectro)
			{
				GamePlay.soundManager.CreateSoundType (SoundsManager.SoundType.Arrow);
				GamePlay.oneShotElectro = true;
			}
		}
		else
		{
			GamePlay.soundManager.CreateSoundType (SoundsManager.SoundType.Arrow);
			GamePlay.oneShotElectro = true;
		}

		Animator[] animators = boom.GetComponentsInChildren<Animator> ();
		foreach (Animator anim in animators) 
		{
			anim.SetTrigger("boom");		
		}
		GamePlay.AddDeleteElectro ();
		property.AnimationScore ();


	}
}
