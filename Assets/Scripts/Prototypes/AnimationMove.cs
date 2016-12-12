using UnityEngine;
using System.Collections;

public class AnimationMove : CacheTransform {
//	private MoveOffset offset;
	private Vector3 toPos;
	private float speed = 0.02f;
	public float upLimit;

	void Start()
	{
		toPos = transform.position;
		toPos.y += upLimit;
		StartAnimation (MoveOffset.Up);
	}

	public void StartAnimation(MoveOffset offset)
	{
//		this.offset = offset;
		switch(offset)
		{
			case MoveOffset.Up:
				AnimationMoveUp();
				break;
		}
	}

	private void AnimationMoveUp()
	{
		if(transform.position.y < toPos.y)
		{
			transform.position = Vector3.MoveTowards(transform.position, toPos, speed);
			Invoke("AnimationMoveUp", GamePlay.timePhysics);
		}
	}
}
