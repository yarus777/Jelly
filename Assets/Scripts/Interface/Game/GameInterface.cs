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
	public InventoryButtons inventory;

	void Awake()
	{
		GamePlay.gameUI = this;
		/*if(GameData.numberLoadLevel<21)
		{
			back.sprite = Resources.Load<Sprite>("Atlases/Interface/Game/domiki");
			pause.states[0] = Resources.Load<Sprite>("Atlases/Interface/Game/Pause/pausered");
			pause.states[1] = Resources.Load<Sprite>("Atlases/Interface/Game/Pause/pausered1");
			inventory.states[0] = Resources.Load<Sprite>("Atlases/Interface/Game/Inventary/bt_int__red_inventory_1");
			inventory.states[1] = Resources.Load<Sprite>("Atlases/Interface/Game/Inventary/bt_int__red_inventory_2");
		}
		else if(GameData.numberLoadLevel>=21&&GameData.numberLoadLevel<41)
		{
			back.sprite = Resources.Load<Sprite>("Atlases/Interface/Game/park");
			pause.states[0] = Resources.Load<Sprite>("Atlases/Interface/Game/Pause/pausemaroon");
			pause.states[1] = Resources.Load<Sprite>("Atlases/Interface/Game/Pause/pausemaroon1");
			inventory.states[0] = Resources.Load<Sprite>("Atlases/Interface/Game/Inventary/bt_int__maron_inventory_1");
			inventory.states[1] = Resources.Load<Sprite>("Atlases/Interface/Game/Inventary/bt_int__maron_inventory_2");
		}
		else if(GameData.numberLoadLevel>=41&&GameData.numberLoadLevel<101)
		{
			back.sprite = Resources.Load<Sprite>("Atlases/Interface/Game/restoran");
			pause.states[0] = Resources.Load<Sprite>("Atlases/Interface/Game/Pause/pausegr");
			pause.states[1] = Resources.Load<Sprite>("Atlases/Interface/Game/Pause/pausegr1");
			inventory.states[0] = Resources.Load<Sprite>("Atlases/Interface/Game/Inventary/bt_int__green_inventory_1");
			inventory.states[1] = Resources.Load<Sprite>("Atlases/Interface/Game/Inventary/bt_int__green_inventory_2");
		}*/
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
