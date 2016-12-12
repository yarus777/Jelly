using UnityEngine;
using System.Collections;

public class AnimationObject : CacheTransform {
	public Sprite[] sprites;
	public int index;
	public StateAnimation state;
	public SpriteRenderer sRenderer;
	private float speedAnimation = 0.03f;

	void Awake()
	{
		sRenderer = GetComponent<SpriteRenderer> ();
	}

	public void InitAnimation(Sprite[] sprites, StateAnimation state)
	{
		index = 0;
		//block init sprite
		//this.sprites = sprites;
		this.state = state;
	}

	private void NextSprite()
	{
		if(index < sprites.Length -1)
		{
			index++;
			sRenderer.sprite = sprites [index];
		}
		else
		{
			CancelInvoke ("Animated");
		}
	}
	
	private void PreviewSprite()
	{
		if(index >0)
		{
			index--;
			sRenderer.sprite = sprites[index];
		}
		else
		{
			CancelInvoke ("Animated");
		}
	}

	private void Animated()
	{
		switch(state)
		{
			case StateAnimation.Forward:
				NextSprite();
				break;
			case StateAnimation.Back:
				PreviewSprite();
				break;
		}

		Invoke("Animated", speedAnimation);
		return;
	}

	public void StartAnimation(StateAnimation state)
	{
		//block animation
		//return;
		this.state = state;
		CancelInvoke ("Animated");
		Animated ();
	}

}
