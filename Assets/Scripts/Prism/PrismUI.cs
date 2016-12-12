using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PrismUI : MovesUI {

	public List<Sprite> sprites;
	public SpriteRenderer spriteRenderer;

	private float speedAlpha = 0.0075f;
	private bool stateSelected = false;

	public override void Start ()
	{
		MoveSwitch (false);
		GamePlay.prismUI = this;
	}

	public void StatePrism(int number)
	{
		spriteRenderer.sprite = sprites [number];
		//sound
		GamePlay.soundManager.CreateSoundType (GetSoundPrism(number));
	}

	public SoundsManager.SoundType GetSoundPrism(int number)
	{
		switch(number)
		{
			case 0:
				return SoundsManager.SoundType.PrismSelect01;
			case 1:
				return SoundsManager.SoundType.PrismSelect02;
			case 2:
				return SoundsManager.SoundType.PrismSelect03;
			case 3:
				return SoundsManager.SoundType.PrismSelect04;
			case 4:
				return SoundsManager.SoundType.PrismSelect05;
			default:
				return SoundsManager.SoundType.PrismSelect01;
		}

	}

	public override void MoveSwitch (bool state)
	{
		if(state)
		{
			spriteRenderer.enabled = state;
		}

		stateSelected = state;
		AnimationAlpha ();
	}

	public void AnimationAlpha()
	{
		if(stateSelected)
		{
			spriteRenderer.color += new Color(0,0,0, 1-spriteRenderer.color.a);
			if(spriteRenderer.color.a >= 1f)
			{
				return;
			}
		}
		else
		{
			spriteRenderer.color -= new Color(0,0,0, speedAlpha);
			if(spriteRenderer.color.a <= 0f)
			{
				spriteRenderer.enabled = false;
				return;
			}
		}
		Invoke ("AnimationAlpha", GamePlay.timePhysics);
	}
}
