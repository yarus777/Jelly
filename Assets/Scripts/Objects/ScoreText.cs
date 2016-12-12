using UnityEngine;
using System.Collections;

public class ScoreText : CacheTransform {
	private TextMesh textMesh;
	AnimationMove moveScript;
	AnimationTransparent transparentScript;
	private float timeAnimation = 0.8f;

	void Awake()
	{
		textMesh = GetComponent<TextMesh> ();
		moveScript = GetComponent<AnimationMove> ();
		transparentScript = GetComponent<AnimationTransparent> ();
	}

	public void StartAnimation(int countScore)
	{
		if(textMesh!=null)
		{
			textMesh.text = countScore.ToString();
		}

		if(moveScript!=null)
		{
			moveScript.StartAnimation(MoveOffset.Up);
		}

		if(transparentScript!=null)
		{
			transparentScript.StartAnimation(StateAnimation.Back);
		}
		Destroy(gameObject, timeAnimation);
	}
}
