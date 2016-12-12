using UnityEngine;
using System.Collections;

public class AnimationTransparent : CacheTransform {
	private StateAnimation state;
	private TextMesh textMesh;
	private float startAlpha = 1f;
	private float speed = 0.002f;
	private float upperLimit = 1f;
	private float lowerLimit = 0f;


	void Awake()
	{
		textMesh = GetComponent<TextMesh> ();
		textMesh.color = new Color(textMesh.color.r, 
		                           textMesh.color.g, 
		                           textMesh.color.b, 
		                           startAlpha);
	}

	void Start()
	{
		StartAnimation (StateAnimation.Back);
	}

	public void StartAnimation(StateAnimation state)
	{
		switch(state)
		{
			case StateAnimation.Forward:
				AnimationForward();
				CancelInvoke("AnimationBack");
				break;
			case StateAnimation.Back:
				AnimationBack();
				CancelInvoke("AnimationForward");
				break;
		}
		this.state = state;
	}

	private void AnimationForward()
	{
		if(textMesh.color.a<upperLimit)
		{
			ChangeAlpha();
			Invoke("AnimationForward", GamePlay.timePhysics);
		}
		else
		{
			textMesh.color = new Color(textMesh.color.r, 
			                           textMesh.color.g, 
			                           textMesh.color.b, 
			                           upperLimit);
		}
	}

	private void AnimationBack()
	{
		if(textMesh.color.a>lowerLimit)
		{
			ChangeAlpha();
			Invoke("AnimationBack", GamePlay.timePhysics);
		}
		else
		{
			textMesh.color = new Color(textMesh.color.r, 
			                           textMesh.color.g, 
			                           textMesh.color.b, 
			                           lowerLimit);
		}
	}

	private void ChangeAlpha()
	{
		switch(state)
		{
			case StateAnimation.Forward:
				textMesh.color = new Color(textMesh.color.r, 
				                           textMesh.color.g, 
				                           textMesh.color.b, 
				                           textMesh.color.a+speed);
				break;
			case StateAnimation.Back:
				textMesh.color = new Color(textMesh.color.r, 
				                           textMesh.color.g, 
				                           textMesh.color.b, 
				                           textMesh.color.a-speed);
				break;
		}
	}
}
