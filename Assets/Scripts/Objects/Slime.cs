using UnityEngine;
using System.Collections;

public class Slime : CacheTransform, ISlime {
	private Properties property;
	private float timeAnimSlime = 0.68f;
	private SpriteRenderer spriteRenderer;

	void Awake()
	{
		property = GetComponent<Properties> ();
		property.iSlime = this;
		property.iPoints = new Points (PointManager.slime);
		spriteRenderer = GetComponentInChildren<SpriteRenderer> ();
//		WaitAnimationSlime ();
	}

	// Use this for initialization
	void Start () {

	}

	#region ISlime implementation
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
		spriteRenderer.enabled = false;
		property.AddPoints ();
		property.isMoving = true;
		property.isDelete = true;
//		property.AnimationScore ();
		GamePlay.deleteSlime = true;
		
		if(!GamePlay.oneShotSlimeDestroy)
		{
			GamePlay.soundManager.CreateSoundType(SoundsManager.SoundType.SlimeDestroy);
			GamePlay.oneShotSlimeDestroy = true;
		}

		WaitAnimationSlime ();

//		Visible (false);
	}

	public void DeleteObject()
	{
		MonoBehaviour.DestroyImmediate (gameObject);
	}
	#endregion

	public void WaitAnimationSlime()
	{
		GameObject obj = Instantiate (Resources.Load<GameObject> ("Prefabs/Slime/deadSlime/DeadSlime")) as GameObject;
		obj.transform.position = property.transform.position;
		obj.GetComponent<Animator> ().SetTrigger ("go");
		Invoke ("NotMove", timeAnimSlime);
		Invoke ("AnimationScore", timeAnimSlime / 2f);
	}

	public void NotMove()
	{
		property.isMoving = false;
	}

	public void AnimationScore()
	{
		property.AnimationScore ();
	}
}
