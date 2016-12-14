using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameInterface : MonoBehaviour {
	public TextMesh score;
	public TextMesh nameScore;
	public TextMesh nameLimit;
	public TextMesh countLimit;
	public TextMesh targetName;
	public TextMesh targetCount;

	public List<SpriteRenderer> panels;

	public GameObject stars;

	public SpriteRenderer back;
	public PauseButtons pause;

	void Awake()
	{
		GamePlay.gameUI = this;
	}

	public void Update()
	{
		UpdateScore ();
		UpdateNameLimit ();
		UpdateCountLimit ();
		UpdateTargetName ();
		UpdateTargetCount ();
	}


	public void UpdateScore()
	{
		score.text = GameData.score.ToString ();
	}

	public void UpdateNameLimit()
	{
		if(GameData.limit.GetTypeLimit() == Limit.Moves)
		{
			nameLimit.text = StringConstants.GetText(StringConstants.TextType.Moves).ToLower();
		}
		else
		{
			nameLimit.text = GameData.limit.GetLimitName ().ToLower();
		}
	}

	public void UpdateCountLimit()
	{
		countLimit.text = GameData.limit.GetLimitCount ();
	}

	public void UpdateTargetName()
	{
		targetName.text = GameData.taskLevel [0].NameTask ();
	}

	public void UpdateTargetCount()
	{
		string tCount = string.Format ("{0}/{1}", GameData.taskLevel [0].GetCurrent ().ToString (), GameData.taskLevel [0].GetGoal ().ToString ());
		targetCount.text = tCount;
	}
}
