using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Star : CacheTransform {
	public Sprite[] spritesForFilling;
	public bool isFull = false;
	public int point;
	public int step;
	public int prewValueStar;
	public SpriteRenderer sRenderer;

	void Awake()
	{
		sRenderer = GetComponent<SpriteRenderer> ();
	}

	void Start()
	{
	}

	public void SetPoint(int point)
	{
		this.point = point;
		step = (int)((point - prewValueStar)/(spritesForFilling.Length - 1));
	}

	public void IsFull()
	{
		if(GameData.score >= point)
		{
			isFull = true;
			GetComponent<Animator>().SetTrigger("go");
			sRenderer.sprite = spritesForFilling[spritesForFilling.Length-1];
			GamePlay.countStarsLevel++;
//			Debug.Log("Stars: "+GamePlay.countStarsLevel);
		}
		else 
		{
			SetSpriteToStar();
		}
	}

	private void SetSpriteToStar()
	{
		int currentPoints = prewValueStar;
		for(int i=0; i< spritesForFilling.Length; i++)
		{
			if(GameData.score >= currentPoints)
			{
				sRenderer.sprite = spritesForFilling[i];
				currentPoints += step;
			}
		}
	}
}
