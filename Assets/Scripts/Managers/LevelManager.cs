using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.MyScripts.Lives;

/// <summary>
/// Level manager.
/// </summary>
public class LevelManager : CacheTransform {
	public GameObject startText;
	Limit typeLevel;

	private bool startTutorial = false;


	public LevelManager()
	{
		GamePlay.lvlManager = this;

	}


	void Start () {
		GameData.Reset ();
		GamePlay.Restart ();

		GamePlay.Reset ();

		GameData.parser.ParseLevel (GameData.numberLoadLevel);
		if (GameData.parser.levelType == Task.Diamond) {
			GameData.diamondManager.SetData (GameData.parser.minDiamondInScene, 
			                                 GameData.parser.maxDiamondInScene, 
			                                 GameData.parser.chanceDiamond, true);
		}

		GameData.sizeX = GameData.parser.colls;
		GameData.sizeY = GameData.parser.rows;
		GameData.sizeYVisible = GameData.parser.vRows;
		GameField.startPos = new Vector3 ((GameData.maxX-GameData.sizeX)/2f*GameData.distanceBetwObject, (GameData.maxY - GameData.sizeYVisible)/2f*GameData.distanceBetwObject, 0);
		GameData.taskLevel.Add(new TaskLevel (GameData.parser.levelType, GameData.parser.levelGoal));
		GameData.limit = new Limitation (GameData.parser.limitType, GameData.parser.countLimit);
		GameData.manager = new ObjectManager (GameData.sizeX,GameData.sizeY);

		foreach(SChanceColor chance in GameData.parser.chanceObjects)
		{
			GameData.colorChanses.Add(chance);
		}


		GameField.EditorLevel ();
		GamePlay.backManager = new BackManager ();
		GamePlay.backManager.CreateGrid ();
		GamePlay.backManager.Clear ();
		GamePlay.backManager.Visible ();
		Instantiate (Resources.Load<GameObject> ("Prefabs/Interface/GameInterface"));
		Stroke ();

	}

	public void PauseStroke()
	{
		CancelInvoke ("Stroke");
	}

	public void ResumeStroke()
	{
		Stroke ();
	}

	private void ResetShareds()
	{
		List<Properties> allObj = GameData.manager.GetAllObects (true);
		foreach(Properties prop in allObj)
		{
			foreach(SpriteRenderer sR in prop.GetComponentsInChildren<SpriteRenderer>())
			{
				sR.color = new Color(1f,1f,1f,1f);
			}
		}
	}

	public void Stroke()
	{
		if(!GamePlay.endTargetAnimation)
		{
			TargetAnimation();
			return;
		}

		if(!GamePlay.startGame)
		{
			GamePlay.SetInput (false);
			if(GameData.sizeY>GameData.sizeYVisible)
			{
				Invoke("StartOffset", 1.2f);
				return;
			}
			else
			{
				GamePlay.startGame = true;
				GameData.timer.Run();
			}
		}

		if(IsMoveObject ())
		{
			return;
		}

		if(IsNeedOffset ())
		{
			return;
		}

		if(GamePlay.allObjectManager.NeedOffset())
		{
			WaitMoveUp();
			return;
		}

		if(GamePlay.startBonusTime)
		{

			GamePlay.blockPauseButton = true;
			IsStartBonusTime();
			return;
		}

		if(GamePlay.bonusTime)
		{
			GamePlay.blockPauseButton = true;
			IsCreateBonus();
			return;
		}


		if(IsNeedDeleteDiamond ())
		{
			return;
		}
		
		if(IsCreateElectro())
		{
			return;
		}

		if(IsNeedFeed2())
		{
			return;
		}

		if(IsNeedSlime())
		{
			return;
		}

		if(IsWin ())
		{
			GamePlay.blockPauseButton = true;
			return;
		}

		if(IsLose())
		{
			return;
		}

		GameData.manager.NotMoves ();
		IsNextMove ();
	}

	public bool IsMoveObject()
	{
		if (GameData.manager.IsMoveObject ()) 
		{
			Invoke ("Stroke", GamePlay.timePhysics);
			return true;
		} 
		return false;
	}

	public bool IsNeedDeleteDiamond()
	{
		if(GamePlay.NeedDeleteDiamond())
		{
			Invoke("DeleteDiamond",0.3f);
			Invoke("Stroke", 0.3f);
			return true;
		}
		return false;
	}

	public bool IsWin()
	{
		if(GamePlay.WinLevel())
		{
			if(GameData.timer!=null)
			{
				GameData.timer.Pause();
			}
			if(GameData.limit.GetTypeLimit()== Limit.Moves)
			{
				Invoke("IsDestroyOne",0.5f);
				return true;
			}

			GameData.manager.OffsetObject();

			Instantiate(Resources.Load<GameObject>("Prefabs/Interface/LevelComplete"));
			GamePlay.blockPauseButton = true;
			Invoke("WinPopup", 2f);
			GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.PopupLevelComplete, false);
			return true;
		}
		return false;
	}



	private void WinPopup()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene("WinLose");
	}


	public bool IsLose()
	{
		if(GamePlay.LoseLevel())
		{
			GameData.manager.OffsetObject();
			if(!GamePlay.onlyOneMoves)
				EndMoves();
			else
				Capitulate();
			return true;
		}
		return false;
	}

	private void EndMoves()
	{
			GameObject obj = Instantiate (Resources.Load<GameObject> ("Prefabs/Interface/NotMoves")) as GameObject;
			GamePlay.blockPauseButton = true;
			obj.transform.localPosition = new Vector3 (5.5f, 8.25f, -3);
			GamePlay.soundManager.CreateSoundTypeUI (SoundsManager.UISoundType.WindowNotMoves, false);

	}

	public bool IsNeedOffset()
	{
		if(GameData.manager.OffsetObject())
		{
			Invoke ("Stroke", GamePlay.timePhysics);
			return true;
		}
		return false;
	}

	public bool IsCreateElectro()
	{
		if(GamePlay.NeedCreateElectro())
		{
			Invoke("CreateElectro", GamePlay.timePhysics);
			Invoke ("Stroke", GamePlay.timePhysics);
			return true;
		}
		return false;
	}

	public void IsNextMove()
	{
		GamePlay.deleteSlime = false;
		GamePlay.isStroke = false;
		GameData.windowInterface = 0;
		GamePlay.SetInput (true);
		GamePlay.soundManager.ResetDeSelect ();
		Tutorial ();
		if(GamePlay.tutorial==null || GamePlay.tutorial!=null && !GamePlay.tutorial.isTutorial())
		{
			startText.SetActive(true);
		}
	}

	public void IsCreateBonus()
	{
		if(GameData.limit.GetValueLimit()>0)
		{
			GamePlay.CreateBonus ();
			Invoke ("IsDestroyBonus", 1f);
		}
		else
		{
			GamePlay.bonusTime = false;

			Invoke("Stroke", GamePlay.timePhysics);
		}
	}

	public void IsDestroyBonus()
	{
		GamePlay.DestroyBonus ();
		Invoke ("Stroke", GamePlay.timePhysics);
	}

	public void IsDestroyOne()
	{
        Debug.Log("IsDestroyOne");
		if(GamePlay.DestroyBonus())
		{
			Invoke ("Stroke", GamePlay.timePhysics);
		}
		else
		{
			if(GameData.limit.GetValueLimit()>0 && GameData.parser.isBonusTime)
			{
				GamePlay.startBonusTime = true;
				Invoke ("Stroke",	GamePlay.timePhysics);
				return;
			}

			GameData.manager.OffsetObject();
			Instantiate(Resources.Load<GameObject>("Prefabs/Interface/LevelComplete"));
			GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.PopupLevelComplete, false);
			Invoke("WinPopup", 2f);
		}
	}

	public void IsStartBonusTime()
	{
		if(GamePlay.bonusTimeInterfase)
		{
			GamePlay.startBonusTime = false;
			GamePlay.bonusTimeInterfase = false;
			GamePlay.bonusTime = true;
			Invoke ("Stroke", GamePlay.timePhysics);
		}
		else
		{
			if(TutorialBonusTime())
			{
				return;
			}
			GamePlay.bonusTimeInterfase = true;
			Instantiate(Resources.Load("Prefabs/Interface/BonusTime"));
			GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.BonusTime, false);
			Invoke ("Stroke", 2f);
		}
	}

	private bool TutorialBonusTime()
	{
		if(GamePlay.tutorial == null)
		{
			GamePlay.tutorial = new TutorialManager ();
			if(!GamePlay.tutorial.isBonusTime())
			{
				GamePlay.tutorial = null;
			}

		}

		if(GamePlay.tutorial != null && GamePlay.tutorial.levelIsTutorial(GameData.numberLoadLevel) && GamePlay.tutorial.isBonusTime())
		{
			GamePlay.tutorial.NextStep();
			return true;
		}
		return false;
	}

	private void DeleteDiamond(){
		GamePlay.DeleteDiamond ();
		GameData.manager.OffsetObject();
	}

	private void CreateElectro()
	{
		GamePlay.CreateElectro (GamePlay.countElectro);
		GamePlay.countElectro = 0;
	}

	private void StartOffset()
	{
		GamePlay.allObjectManager.StartMove(MoveOffset.Down, (GameData.sizeY-GameData.sizeYVisible)*GameData.distanceBetwObject);
		WaitStartGame ();
	}

	private void WaitStartGame()
	{
		if(GamePlay.allObjectManager.IsMoving())
		{
			Invoke("WaitStartGame", 0.5f);
			return;
		}
		GamePlay.startGame = true;
		GameData.timer.Invoke ("Run", GamePlay.timePhysics);
		Stroke ();
	}

	private void WaitMoveUp()
	{
		if(GamePlay.allObjectManager.IsMoving())
		{
			Invoke("WaitMoveUp", 0.5f);
			return;
		}

		DestroyUpLayer ();
		Stroke ();
	}

	private void DestroyUpLayer()
	{
		GameData.manager.DestroyYToSizeYField (GameData.sizeY-GamePlay.allObjectManager.countOffset);
		GameData.sizeY -= GamePlay.allObjectManager.countOffset;
	}

	private bool IsNeedFeed2()
	{
		if(GamePlay.Feed2Save())
		{
			Invoke("Stroke",GamePlay.timePhysics);
			return true;
		}
		return false;
	}

	private bool IsNeedSlime()
	{
		if(!GamePlay.firstMove&&GamePlay.isStroke)
		{
			if(GamePlay.NeedCreateSlime())
			{
				Invoke("Stroke",GamePlay.timePhysics);
				return true;
			}
		}
		return false;
	}

	//----------------Tutorial
	private void Tutorial()
	{
		if(!startTutorial)
		{
			GamePlay.tutorial = new TutorialManager();
			if(GamePlay.tutorial.levelIsTutorial(GameData.numberLoadLevel)&&!GamePlay.tutorial.isBonusTime())
			{
				GamePlay.tutorial.StartTutorial();
			}
			else
			{
				GamePlay.tutorial = null;
			}
			startTutorial = true;
		}
		else
		{
			if(GamePlay.tutorial!=null&&GamePlay.tutorial.needStep)
			{
				GamePlay.tutorial.NextStep();
				GamePlay.tutorial.needStep = false;
			}
		}
	}

	private void TargetAnimation()
	{
		GamePlay.soundManager.CreateSoundTypeUI (SoundsManager.UISoundType.PopupTask, false);
		GamePlay.blockPauseButton = true;
		Instantiate (Resources.Load<GameObject> ("Prefabs/Interface/TargetAnimation"));
		GamePlay.SetInput (false);
		Invoke ("EndTargetAnimation", 2.333f);
	}

	private void EndTargetAnimation()
	{
		GamePlay.blockPauseButton = false;
		GamePlay.endTargetAnimation = true;
		Invoke ("Stroke", GamePlay.timePhysics);
	}

	public void Capitulate()
	{
		Instantiate (Resources.Load<GameObject> ("Prefabs/Interface/LevelFailed"));
		GamePlay.soundManager.CreateSoundTypeUI (SoundsManager.UISoundType.PopupLevelFailed, false);
		Invoke ("LoadLose", 2f);
	}

	private void LoadLose()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene("WinLose");
	}


}
