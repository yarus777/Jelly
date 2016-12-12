using UnityEngine;
using System.Collections;

public class NotSilver : MonoBehaviour {
	public SpriteRenderer spriteRenderer;
	public Animator animator;
	private bool endAnimation = true;
	private float animationSpeed = 1f;

	public void StartAnimation()
	{
		if(endAnimation)
		{
			spriteRenderer.enabled = true;
			animator.SetTrigger ("open");
			Invoke("RendererOff",animationSpeed);
			endAnimation = false;
			GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.WindowOpen, false);
		}
	}

	public void RendererOff()
	{
		endAnimation = true;
		spriteRenderer.enabled = false;
	}
}
