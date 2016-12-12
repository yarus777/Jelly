using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Prism : CacheTransform, IPrism {
	private SpriteRenderer spriteRenderer;
	public List<Sprite> statesCapture;
	private Properties property;

	private new bool active = false;

	private float speed = 0.3f;
	private int numberSelect = 4;

	public GameObject prismEffect;

	public Animator animator;
	
	public List<Properties> targetEffects = new List<Properties>();

	public new SphereCollider collider;

	private bool stateAnimation = false;

	void Awake()
	{
		property = GetComponent<Properties>();
		property.iPrism = this;
		property.iColor = new ColorObject();
		property.iPoints = new Points (PointManager.prism);
		spriteRenderer = GetComponentInChildren<SpriteRenderer> ();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	#region IPrism implementation

	public void DeleteObject ()
	{
		DestroyImmediate (gameObject);
	}

	public void PrepareDelete ()
	{
		property.isMoving = true;
		Invoke ("StartDelete", property.delayDelete);
//		Invoke ("DeleteObject", 0.3f);
	}

	private void StartDelete()
	{
		//Sound
		if(!GamePlay.oneShotPrisBoom)
		{
			GamePlay.soundManager.CreateSoundType (SoundsManager.SoundType.PrismBoom);
			GamePlay.oneShotPrisBoom = true;
		}

		foreach(Properties targetProperty in targetEffects)
		{
			StartEffect (property, targetProperty);
		}
		CancelInvoke("SelectedPrism");
		spriteRenderer.sprite  = statesCapture[numberSelect];
		property.AddPoints ();
		property.AnimationScore ();
	}

	public void Active (StateObjects state)
	{
		switch(state)
		{
			case StateObjects.Normal:
				CancelInvoke("AnimationPrism");
				CancelInvoke("SelectedPrism");
//				CancelInvoke("AnimationPrism");
				spriteRenderer.sprite  = statesCapture[5];

				
//				animator.SetTrigger("MouseUp");
				break;
			case StateObjects.Selected:
				CancelInvoke("AnimationPrism");
				SelectedPrism();
				
//				animator.SetTrigger("MouseDown");

				
				break;
		}
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

	public Colors GetColor()
	{
		switch(numberSelect)
		{
			case 0:
				return Colors.Yellow;
			case 1:
				return Colors.Green;
			case 2:
				return Colors.Blue;
			case 3:
				return Colors.Fiolet;
			case 4:
				return Colors.Red;
			default:
				return Colors.Empty;
		}
	}

	public GameObject GetEffect()
	{
		return prismEffect;
	}

	public void AddPosEffect(Properties target)
	{
		targetEffects.Add (target);
	}

	public void Pause()
	{
		CancelInvoke("SelectedPrism");
		AnimationPrism ();
	}

	public void Resume()
	{
//		Debug.Log ("Selected prism");
		SelectedPrism();
		CancelInvoke("AnimationPrism");
	}

	public void RandomColor()
	{
		numberSelect = Random.Range (0, 5);
	}

	public void SetSpeed(float speed)
	{
		this.speed = speed;
	}

	public void State(PrismState state)
	{
		switch(state)
		{
			case PrismState.MouseDown:
//				GamePlay.moveUI.MoveSwitch(false);
//				GamePlay.prismUI.MoveSwitch(true);
//				animator.SetTrigger("MouseDown");
//				spriteRenderer.sortingOrder = 0;
//				collider.radius = 0.25f;
				break;
			case PrismState.MouseUp:
//				GamePlay.moveUI.MoveSwitch(true);
//				GamePlay.prismUI.MoveSwitch(false);
//				anyMimator.SetTrigger("MouseUp");
//				spriteRenderer.sortingOrder = -1;
//				collider.radius = 0.85f;
				break;
		}
	}

	#endregion

	private void SelectedPrism()
	{
		if(numberSelect<4)
		{
			numberSelect++;
		}
		else
		{
			numberSelect = 0;
		}
		spriteRenderer.sprite = statesCapture[numberSelect];
		//GamePlay.prismUI.StatePrism (numberSelect);
		Invoke ("SelectedPrism", speed);
	}

	private void AnimationPrism()
	{
		if(stateAnimation)
		{
			spriteRenderer.sprite = statesCapture[5];
		}
		else
		{
			spriteRenderer.sprite = statesCapture[numberSelect];
		}
		stateAnimation = !stateAnimation;
		Invoke ("AnimationPrism", 0.5f);
	}

	public void StartEffect(Properties start, Properties target)
	{

		GameObject effect = start.iPrism.GetEffect ();
		GameObject ob = Instantiate (effect) as GameObject;
		ob.transform.position = start.transform.position;
		ob.transform.position -= new Vector3 (0, 0, 1);
		ob.GetComponent<MoveToPosition> ().PrepareMove (target.transform.position);
	}
}
