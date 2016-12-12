using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Puddle : CacheTransform, IPuddle {
	private int currentHp;
	private int maxHp;
	private int countSpriteHp;
	private TextMesh textCountHp;

	private SpriteRenderer sprite;

	public List<Sprite> confetti;

	public GameObject confettiObj; 

	public SpriteRenderer effectObject;

	public Properties property;

	private new bool active = false;

	private int countAttack;

	private MeshRenderer meshRenderer;
	// Use this for initialization
	void Awake() {
		property = GetComponent<Properties>();
		property.iPuddle = this;
		property.iColor = new ColorObject();
		property.iPoints = new Points (PointManager.puddle);
		textCountHp = GetComponentInChildren<TextMesh> ();
		sprite = GetComponentInChildren<SpriteRenderer> ();
		meshRenderer = GetComponentInChildren<MeshRenderer> ();
	}

	#region IPuddle implementation

	public void SetCurHp (int value)
	{
		currentHp = value;

		UpdateCountHp ();
//		property.SetState (StateObjects.Normal);
	}

	public void SetMaxHp(int value)
	{
		maxHp = value;
	}
	
	public void Attack (int count)
	{
		countAttack = count;
		Invoke ("AttackGO", property.delayDelete);
	}

	public void AttackGO()
	{
		if(!property.isDelete)
		{
			if(currentHp-countAttack>0)
			{
				SetCurHp(currentHp - countAttack);
				property.SetState(StateObjects.Normal);
				//				AnimationConfetti();
				if(!GamePlay.oneShotFeed)
				{
					GamePlay.soundManager.CreateSoundType(SoundsManager.SoundType.Puddle);
					GamePlay.oneShotFeed = true;
				}
				stateActive = false;
			}
			else
			{
				property.SetState(StateObjects.Normal);
				property.isDelete = true;
				property.isMoving = true;
				textCountHp.text = "";
				
				if(!GamePlay.oneShotFeed)
				{
					GamePlay.soundManager.CreateSoundType(SoundsManager.SoundType.PuddleBoom);
					GamePlay.oneShotFeed = true;
				}
				
				AnimationBoom();
				//				property.isDelete = true;
				//				property.AddPoints ();
				//				property.AnimationScore ();
				//				textCountHp.text = "";
				////				GameField.ReplaceObject (property, ObjectTypes.Jelly, property.iColor.GetColor ());
				//				GamePlay.AddTaskValue (Task.Feed1, 1);
				//
				//				if(!GamePlay.oneShotFeed)
				//				{
				//					GamePlay.soundManager.CreateSoundType(SoundsManager.SoundType.Puddle);
				//					GamePlay.oneShotFeed = true;
				//				}
			}
		}
	}

	private void PrepareDelete()
	{

		property.isDelete = true;
		property.AddPoints ();
		property.AnimationScore ();

		//				GameField.ReplaceObject (property, ObjectTypes.Jelly, property.iColor.GetColor ());
		GamePlay.AddTaskValue (Task.Feed1, 1);
		

	}

	private void AnimationBoom()
	{
//		Debug.Log ("AnimationBoom");
		GameObject boom = Instantiate(Resources.Load<GameObject>("Prefabs/Puddle/PuddleBoom")) as GameObject;
		//GameObject boom = Instantiate(GameData.pool.GetObject(ObjectTypes.BoomBomb, Colors.Blue)) as GameObject;
		boom.transform.position = transform.position;
		Invoke ("SpriteEnable", 0.625f);
		Invoke ("PrepareDelete", 0.8f);
	}

	public void SpriteEnable()
	{
		sprite.enabled = false;
	}
	
	public void UpdateCountHp()
	{
		textCountHp.text = currentHp.ToString ();
	}

	public void Visible(bool state)
	{
		sprite.enabled = state;
		meshRenderer.enabled = state;
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

	public void Active (StateObjects state)
	{
		switch(state)
		{
		case StateObjects.Normal:
			effectObject.enabled = false;
			break;
		case StateObjects.Selected:
			effectObject.enabled = true;
			break;
		}
	}

	#endregion

	private void AnimationConfetti()
	{
		GameObject newObj = Instantiate (confettiObj) as GameObject;
		newObj.name = confettiObj.name;
		newObj.transform.parent = confettiObj.transform.parent;
		newObj.transform.localPosition = confettiObj.transform.localPosition;
		newObj.GetComponent<SpriteRenderer>().sortingOrder = confettiObj.GetComponent<SpriteRenderer>().sortingOrder;
		newObj.GetComponent<SpriteRenderer> ().sprite = GetSpriteState ();
		confettiObj.GetComponent<SpriteRenderer> ().sortingOrder++;
		confettiObj.GetComponent<Confetti> ().DeleteObject ();
		confettiObj = newObj;
	}

	private Sprite GetSpriteState()
	{
		int currentSprite = confetti.Count - (int)(((float)confetti.Count / (float)maxHp) * (float)currentHp);
		if(currentSprite>confetti.Count-1)
		{
			currentSprite = confetti.Count-1;
		}
		return confetti [currentSprite];
	}

	public void DeleteObject()
	{
		DestroyImmediate (gameObject);
	}


}
