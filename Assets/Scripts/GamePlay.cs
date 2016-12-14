using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

/// <summary>
/// Game play.
/// </summary>
public static class GamePlay {
	#region Variables
	/// <summary>
	/// Список выделенных объектов.
	/// </summary>
	public static List<Properties> selectedObjects = new List<Properties>();
	/// <summary>
	/// The color of the selected object.
	/// </summary>
	private static 	Colors selectedColor = Colors.Empty;
	/// <summary>
	/// Флаг ввода.
	/// </summary>
	public static bool isInput;
	/// <summary>
	/// The level manager.
	/// </summary>
	public static LevelManager lvlManager;
	/// <summary>
	/// The last position selected objects.
	/// </summary>
	public static Vector3 lastPos;
	/// <summary>
	/// The last property.
	/// </summary>
	public static Properties lastProperty;
	/// <summary>
	/// The line manager.
	/// </summary>
	public static LineManager lineManager = new LineManager();
	/// <summary>
	/// Количество очков за ход.
	/// </summary>
	//public static int scoreStroke;

	public static int countElectro = 0;

	public static List<Properties> deleteObjectOnStroke;

	public static List<Properties> electroForDelete;
	public static List<Properties> bombForDelete;
	public static List<Properties> prismForDelete;
	
	/// <summary>
	/// Количество использованных электромонстриков в уровне
	/// </summary>
	public static int countDeleteElectro = 0;
	
	public static bool stateFireball = false;

	public static bool startBonusTime = false;

	public static bool bonusTime = false;

	public static bool bonusTimeInterfase = false;

	public static bool startGame = false;

	public static AllObjectManager allObjectManager;

	public static StarManager starManager;

	public static BackManager backManager;

	public static bool deleteSlime;

	public static bool firstMove;

	public static bool isStroke;

	public static SoundsManager soundManager;

	public static bool oneShotElectro;
	public static bool oneShotBomb;
	public static bool oneShotPot;
	public static bool oneShotIce;
	public static bool oneShotJam;
	public static bool oneShotFillTheBucket;
	public static bool oneShotFilledBucket;
	public static bool oneShotFeed;
	public static bool oneShotPU;
	public static bool oneShotSlimeOpen;
	public static bool oneShotSlimeDestroy;

	public static bool oneShotDigAttack;
	public static bool oneShotDigDrop;

	public static bool oneShotPrisBoom;
	private static int _maxCompleteLevel;
	public static int maxCompleteLevel{
		get{
            
            return PlayerPrefs.GetInt("maxCompleteLevel",0);
            
		}
		set{
			_maxCompleteLevel = value;
            PlayerPrefs.SetInt("maxCompleteLevel", _maxCompleteLevel);
            PlayerPrefs.Save();
            Debug.Log("SET: maxCompleteLevel:" + _maxCompleteLevel);
        }
	}

    private static int _fullStarsLevelCount;
    public static int FullStarsLevelCount
    {
        get
        {
            return PlayerPrefs.GetInt("fullStarsLevelCount", 0);

        }
        set
        {
            _fullStarsLevelCount = value;
            PlayerPrefs.SetInt("fullStarsLevelCount", _fullStarsLevelCount);
            PlayerPrefs.Save();
        }
    }

    private static int _deletedBombsCount;
    public static int DeletedBombsCount
    {
        get
        {
            return PlayerPrefs.GetInt("deletedBombsCount", 0);

        }
        set
        {
            _deletedBombsCount = value;
            PlayerPrefs.SetInt("deletedBombsCount", _deletedBombsCount);
            PlayerPrefs.Save();
        }
    }

    private static int _deletedPrismsCount;
    public static int DeletedPrismsCount
    {
        get
        {
            return PlayerPrefs.GetInt("deletedPrismsCount", 0);

        }
        set
        {
            _deletedPrismsCount = value;
            PlayerPrefs.SetInt("deletedPrismsCount", _deletedPrismsCount);
            PlayerPrefs.Save();
        }
    }



    private static int _deletedGreenJamCount;
    public static int DeletedGreenJamCount
    {
        get
        {
            return PlayerPrefs.GetInt("deletedGreenJamCount", 0);

        }
        set
        {
            _deletedGreenJamCount = value;
            PlayerPrefs.SetInt("deletedGreenJamCount", _deletedGreenJamCount);
            PlayerPrefs.Save();
        }
    }


    public static int countStarsLevel;
    private static StateInterfaceMap _interfaceMap = StateInterfaceMap.Start;
	public static StateInterfaceMap interfaceMap{
		get{
			return _interfaceMap;
		}
		set{
			_interfaceMap = value;
            //Debug.Log("interfaceMap:" + interfaceMap);
        }
    }
	public static StateInterfaceGame interfaceGame;

	public static TutorialManager tutorial;

	public static bool enableTutorial = false;

	public static GameObject interfacePause;

	public static bool blockPauseButton = false;

	public static TeacherManager teacher;

	public static AudioSource musicSource;
	public static bool musicOn;
	public static bool soundOn;

	public static bool endTargetAnimation = false;

	public static bool enableButtonInterface = true;

	public static Finger finger;

	public static bool isAnimationFinger = false;

	public static string lifeTimeString;

	public static MapController mapController;

	public static float timePhysics = 0.02f;

	public static WordsManager wordsManager;

	public static MovesUI moveUI;
	public static PrismUI prismUI;
	public static PU puActive = PU.Empty;

	public static BackUI backUI;
    public static MapUI mapUI;

	public static SphereCollider pauseCollider;
	public static SphereCollider inventoryCollider;

	public static GameInterface gameUI;


	public static bool notDeleteObject = false;


	public static LoadVideo loadVideo;

	public static EUI stateUI;

    public static MapLock mapLocker;
    public static UnlockInterface unlockInterface;


	public static bool onlyOneMoves = false;
	#endregion 

	public static void Restart()
	{

		blockPauseButton = false;
		countElectro = 0;
		countDeleteElectro = 0;
		countStarsLevel = 0;
		stateFireball = false;
		startBonusTime = false;
		bonusTime = false;
		bonusTimeInterfase = false;
		startGame = false;
		deleteSlime = false;
		firstMove = true;
		isStroke = false;
		onlyOneMoves = false;
		oneShotElectro = false;
		oneShotBomb = false;
		oneShotPot = false;
		oneShotIce = false;
		oneShotJam = false;
		oneShotFillTheBucket = false;
		oneShotFilledBucket = false;
		oneShotFeed = false;
		oneShotPU = false;
		oneShotSlimeOpen = false;
		oneShotSlimeDestroy = false;
		oneShotDigAttack = false;
		oneShotDigDrop = false;
		oneShotPrisBoom = false;
		tutorial = null;
		interfaceGame = StateInterfaceGame.Game;
		endTargetAnimation = false;
		enableButtonInterface = true;
		isAnimationFinger = false;
		GamePlay.interfaceMap = StateInterfaceMap.Start;
		GamePlay.backUI = null;
		notDeleteObject = false;
		stateUI = EUI.Empty;
		puActive = PU.Empty;
	}

	public static void SwithFireball()
	{
		stateFireball = !stateFireball;
	}

	public static void AddDeleteElectro()
	{
		countDeleteElectro++;
	}

	public static void ResetDeleteElectro()
	{
		countDeleteElectro = 0;
		stateFireball = false;
	}

	/// <summary>
	/// Adds the objects.
	/// </summary>
	/// <param name="transformGameObj">Transform game object.</param>
	public static void AddObjects(Transform transformGameObj)
	{
		Properties property = transformGameObj.GetComponent<Properties> ();
		if(property != null)
		{
			if(property.canSelected){
				switch(property.GetTypeObject())
				{
					case ObjectTypes.Jelly:
						SelectedObj(property);
						break;
					case ObjectTypes.Electro:
						SelectedObj(property);
						break;
					case ObjectTypes.Puddle:
						SelectedObj(property);
						break;
					case ObjectTypes.Bomb:
						SelectedObj(property);
						break;
					case ObjectTypes.Prism:
						SelectedObj(property);
						break;
					default:
						break;
				}
			}
		}
	}

	/// <summary>
	/// Select the objects.
	/// </summary>
	/// <param name="property">Property.</param>
	public static void SelectedObj(Properties property)
	{
		if(property.iColor == null)
		{
			return;
		}

		if (selectedObjects.Count < 1) 
		{
//			Debug.Log("Tut1");
			soundManager.CreateSelect(SoundsManager.Duration.Up);

			if(finger!=null)
			{
				finger.Pause();
			}

			property.SetState(StateObjects.Selected);
			selectedObjects.Add(property);
			if(property.iColor.GetColor()!= Colors.Prism)
			{
				selectedColor = property.iColor.GetColor();
				GameData.manager.SetShadows(StatementShadow.On,selectedColor);
			}
			lastPos = property.transform.localPosition;
			lastProperty = property;	

			SwitchPrismUI(property);

//			if(property.iPrism!=null)
//			{
//				property.iPrism.State(PrismState.MouseDown);
//			}
		}
		else
		{
			if(isNear(lastPos, property.transform.localPosition))
			{
				if(property.GetState() == StateObjects.Normal) 
				{
//					Debug.Log("Tut2");

//					if(property.iPrism!=null)
//					{
						
//					}



					if (selectedColor==Colors.Empty && lastProperty.iColor.GetColor()== Colors.Prism)
					{
						lastProperty.iPrism.Pause();
					}
					if(selectedColor==Colors.Empty&&lastProperty.iColor.GetColor()== Colors.Prism&& property.iColor.GetColor()!=Colors.Prism)
					{
						selectedColor = property.iColor.GetColor();
						GameData.manager.SetShadows(StatementShadow.On,selectedColor);
					}
					else if(selectedColor!=Colors.Empty && property.iColor.GetColor() != Colors.Prism && selectedColor!=property.iColor.GetColor())
					{
						return;
					}
					else if(lastProperty.iColor.GetColor()== Colors.Prism&& selectedColor!=Colors.Empty)
					{
						lastProperty.iPrism.Pause();
					}



//					Debug.Log("AAA");
					SwitchPrismUI(property);
					        
					soundManager.CreateSelect(SoundsManager.Duration.Up);

					lastPos = property.transform.localPosition;
					
					lineManager.CreateLineAtPositions(property, lastProperty,selectedColor);
					property.SetState(StateObjects.Selected);
					selectedObjects.Add(property);
					
					lastProperty = property;

				}
				else
				{
					if(property.GetInstanceID() == selectedObjects[selectedObjects.Count-2].GetInstanceID())
					{
						if(property.iColor.GetColor() == Colors.Prism&&selectedObjects.Count<3)
						{
							selectedColor = Colors.Empty;
							GameData.manager.SetShadows(StatementShadow.Off, selectedColor);
						}

						SwitchPrismUI(property);

						soundManager.CreateSelect(SoundsManager.Duration.Down);

						lineManager.DeleteLine(lineManager.Count()-1);
						selectedObjects[selectedObjects.Count-1].SetState(StateObjects.Normal);
						property.SetState(StateObjects.Selected);
						selectedObjects.RemoveAt(selectedObjects.Count-1);
						lastPos = property.transform.localPosition;
						lastProperty = property;
					}
				}
			}
		}
	}

	public static void SwitchPrismUI(Properties property)
	{
		if(property!=null&&property.iPrism!=null)
		{
			GamePlay.moveUI.MoveSwitch(false);
			//GamePlay.prismUI.MoveSwitch(true);
		}
		else
		{
			//if(GamePlay.moveUI!=null&&GamePlay.prismUI!=null)
            if (GamePlay.moveUI != null)
			{
				if(puActive == PU.Empty)
				{
					GamePlay.moveUI.MoveSwitch(true);
				}
				//GamePlay.prismUI.MoveSwitch(false);
			}
		}
	}

	/// <summary>
	/// Проверка расположения объекта.
	/// </summary>
	/// <returns><c>true</c>, if near was ised, <c>false</c> otherwise.</returns>
	/// <param name="firstPosition">First position.</param>
	/// <param name="secondPosition">Second position.</param>
	public static bool isNear(Vector3 firstPosition, Vector3 secondPosition)
	{
		float distance = Vector3.Distance(firstPosition, secondPosition);
		float diagonal = GameData.distanceBetwObject * Mathf.Sqrt (2f);
		return (distance <= diagonal+0.1f &&
		       distance > 0 );
	}

	/// <summary>
	/// Delete the objects.
	/// </summary>
	public static void DeleteObjects()
	{
		if(notDeleteObject)
		{
			return;
		}

		if(selectedObjects.Count > 2)
		{
			if(selectedObjects[selectedObjects.Count-1]!=null &&
			   selectedObjects[selectedObjects.Count-1].iPrism!=null)
			{
				selectedObjects[selectedObjects.Count-1].iPrism.Pause();
			}

			if(tutorial!=null)
			{
				if(!tutorial.CorrectTutorial(selectedObjects))
				{
//					GameData.manager.SetShadows(StatementShadow.Off, Colors.Empty);

					foreach(Properties prop in selectedObjects)
					{
						prop.SetState(StateObjects.Normal);
					}
					Reset();
					GamePlay.finger.PrepareResume();
					return;
				}
			}
			if(tutorial!=null)
			{
				tutorial.needStep = true;
			}

			WordsInstance(selectedObjects.Count);

			GamePlay.isAnimationFinger = false;
			firstMove = false;
			isStroke = true;
			ScoreSelected(selectedObjects);
			GameData.limit.ChangeLimit(Limit.Moves, -1);
			GameData.limit.ChangeLimit(Limit.NotLimit, -1);
			float currentDelay = 0f;
			float stepDelay = 0.075f;

			deleteObjectOnStroke = new List<Properties>();
			electroForDelete = new List<Properties>();
			bombForDelete = new List<Properties>();
			prismForDelete = new List<Properties>();

			ObjectTypes[] types = {ObjectTypes.Jelly};

			Properties propertyCreate = GameData.manager.GetLastPropOfType(selectedObjects, types);

			if(propertyCreate!=null)
			{
				if(selectedObjects.Count >= GameData.countForBomb && selectedObjects.Count < GameData.countForArrow)
				{
					if(GameData.parser.isBombs)
					{
						propertyCreate.createPUs = ObjectTypes.Bomb;
						propertyCreate.selectedColor = selectedColor;
					}
				}
				else if(selectedObjects.Count >= GameData.countForArrow && selectedObjects.Count < GameData.countForPrism)
				{
					if(GameData.parser.isArrows)
					{
						propertyCreate.createPUs = ObjectTypes.Electro;
						propertyCreate.selectedColor = selectedColor;
					}
				}
				else if(selectedObjects.Count >= GameData.countForPrism)
				{
					if (GameData.parser.isPrisms)
					{
						propertyCreate.createPUs = ObjectTypes.Prism;
						propertyCreate.selectedColor = Colors.Prism;
					}
				}
			}

			foreach(Properties prop in selectedObjects)
			{
				if(prop != null&&!prop.isDelete)
				{
					switch(prop.GetTypeObject())
					{
						case ObjectTypes.Jelly:
							DeleteJelly(prop, currentDelay, false);
							currentDelay+=stepDelay;
							prop.deleteLine = true;
							break;
						case ObjectTypes.Electro:
							if(!prop.iElectro.stateActive)
							{
								electroForDelete.Add(prop);
								prop.iElectro.stateActive = true;
								prop.delayDelete = currentDelay;
							}
							currentDelay+=stepDelay;
							break;
						case ObjectTypes.Puddle:
							if(!prop.iPuddle.stateActive)
							{
								Debug.Log("Puddle");
								prop.iPuddle.stateActive = true;
								prop.delayDelete = currentDelay;
								prop.iPuddle.Attack(selectedObjects.Count-1);
								
							}
//							isPuddle = true;
							break;
						case ObjectTypes.Bomb:
							if(!prop.iBomb.stateActive)
							{
								bombForDelete.Add(prop);
								prop.iBomb.stateActive = true;
								prop.delayDelete = currentDelay;
							}
							currentDelay+=stepDelay;
							break;
						case ObjectTypes.Prism:
							if(!prop.iPrism.stateActive)
							{
								prismForDelete.Add(prop);
								prop.iPrism.stateActive = true;
								prop.delayDelete = currentDelay;
							}
							currentDelay+=stepDelay;
							break;
						case ObjectTypes.Empty:
							break;
					}
				}
			}
            Debug.LogWarning("GamePLay");
			while(electroForDelete.Count>0
			      ||bombForDelete.Count>0
			      ||prismForDelete.Count>0
			      )
			{
				if(electroForDelete.Count>0)
				{
					DeleteElectro(electroForDelete[0], electroForDelete[0].delayDelete);	
					electroForDelete.RemoveAt(0);
				}
				if(bombForDelete.Count>0)
				{
					DeleteBomb(bombForDelete[0], bombForDelete[0].delayDelete);
					bombForDelete.RemoveAt(0);
				}
				if(prismForDelete.Count>0)
				{
					DeletePrism(prismForDelete[0], prismForDelete[0].delayDelete);
					prismForDelete.RemoveAt(0);
				}
			}


		}
		else if(selectedObjects.Count == 1)
		{
			CreatePU(selectedObjects[0]);
		}

		if(tutorial==null)
		{
			GameData.manager.SetShadows (StatementShadow.Off, selectedColor);
		}
		else
		{
			if(!tutorial.isTutorial())
			{
				GameData.manager.SetShadows (StatementShadow.Off, selectedColor);
			}
			else
			{

//				GameData.manager.SetShadows (StatementShadow.Off, selectedColor);
			}
		}


		
		lvlManager.Stroke();
		Reset ();
		if(GamePlay.finger!=null)
		{
			GamePlay.finger.PrepareResume ();
		}
	}

	private static void CreatePU(Properties property)
	{
		if(property.GetTypeObject() == ObjectTypes.Jelly)
		{
			if(DecCountPU(GamePlay.puActive, 1))
			{
				ObjectTypes type = ObjectTypes.Jelly;
				switch(GamePlay.puActive)
				{
					case PU.Arrow:
						type = ObjectTypes.Electro;
						GameField.ReplaceObject(property, type, property.iColor.GetColor());
						break;
					case PU.Bomb:
						type = ObjectTypes.Bomb;
						GameField.ReplaceObject(property, type, property.iColor.GetColor());
						break;
					case PU.Prism:
						type = ObjectTypes.Prism;
						GameField.ReplaceObject(property, type, Colors.Prism);
						break;
				}

			}
			else
			{
				ShowInventory();
			}
		}

	}

	private static void ShowInventory()
	{

	}

	private static void WordsInstance(int count)
	{
		if(count == 7)
		{
			wordsManager.InstanceText(Words.Cool);
		}
		else if(count == 8)
		{
			wordsManager.InstanceText(Words.Super);
		}
		else if(count >=9 && count <12)
		{
			wordsManager.InstanceText(Words.Great);
		}
		else if(count >=12 && count <15)
		{
			wordsManager.InstanceText(Words.Splendid);
		}
		else if(count >=15 && count <17)
		{
			wordsManager.InstanceText(Words.Amazing);
		}
		else if(count >=17 && count <20)
		{
			wordsManager.InstanceText(Words.Brilliant);
		}
		else if(count >=20)
		{
			wordsManager.InstanceText(Words.Awesome);
		}
	}

	public static void DeleteJelly(Properties property, float delay, bool electroLine)
	{
		if(!electroLine)
		{
			foreach(Properties blackHero in GameData.manager.ArroundObject(ObjectTypes.BlackJelly, property))
			{
				blackHero.iBlackHero.Attack(1);
//				blackHero.isDelete = true;
			}

			foreach(Properties ice in GameData.manager.ArroundObject(ObjectTypes.Ice, property))
			{
				ice.iIce.PrepareDelete(delay);
				ice.isDelete = true;
			}

			foreach(Properties snow in GameData.manager.ArroundObject(ObjectTypes.Snow, property))
			{
				snow.iSnow.DeleteObject();
				snow.isDelete = true;
			}

			foreach(Properties stone in GameData.manager.ArroundObject(ObjectTypes.StoneJelly, property))
			{
				stone.iStone.DeleteObject();
				stone.isDelete = true;
			}
			foreach(Properties slime in GameData.manager.ArroundObject(ObjectTypes.Slime, property))
			{
				slime.iSlime.PrepareDelete(delay);
				slime.isDelete = true;
			}
		}
		property.delayDelete = delay;
		property.iJelly.PrepareDelete();
		property.isDelete = true;
	}

	public static void DeleteBlackHero(Properties property, float delay)
	{
		property.delayDelete = delay;
		property.iBlackHero.Attack(1);
//		property.isDelete = true;
	}

	public static void DeleteElectro(Properties property, float delay)
	{
		property.delayDelete = delay;
		ElectroLine deleteElectroV = new ElectroLine ();
		deleteElectroV.SetListProperty (property, RemovingDirection.Vertical, delay);
		ElectroLine deleteElectroH = new ElectroLine ();
		deleteElectroH.SetListProperty (property, RemovingDirection.Horizontal, delay);
		property.isDelete = true;
		property.iElectro.PrepareDelete();
	}

	public static void DeleteBomb(Properties property, float delay)
	{
        //Debug.Log("Delete Bomb");
	    DeletedBombsCount++;
        ProgressController.instance.SetProgress("Exploder", DeletedBombsCount);

		property.delayDelete = delay;
		BombLine deleteBomb = new BombLine ();
		deleteBomb.SetListProperty (property, delay);
		property.isDelete = true;
		property.iBomb.PrepareDelete();
	}

	public static void DeletePrism(Properties property, float delay)
	{
        //Debug.Log("Delete Prism");
        DeletedPrismsCount++;
        ProgressController.instance.SetProgress("Sweet Tooth", DeletedPrismsCount);


		property.delayDelete = delay;
		PrismLine deletePrism = new PrismLine ();


		//deletePrism.SetListProperty (property, delay, property.iPrism.GetColor());
        deletePrism.SetListProperty(property, delay, selectedColor);

		property.isDelete = true;
		property.iPrism.PrepareDelete();
	}

	public static void DeleteJam(Properties property){
        //Debug.Log("DeleteJam " + property.iColor.GetColor());

        if (property.iColor.GetColor() == Colors.Green)
	    {
	        DeletedGreenJamCount++;
            ProgressController.instance.SetProgress("Jelly keeper", DeletedGreenJamCount);
	    }



		Properties jam = GameData.manager.GetJam (property);
		if(jam != null)
		{
			jam.iJam.PrepareDelete ();
			property.iPoints = new Points(property.iPoints.GetPoint() + PointManager.jam);
			GameData.manager.DeleteObject (jam);
		}
	}
	
	public static void DeleteStone(Properties property, float delay)
	{
		property.delayDelete = delay;
		property.iStone.DeleteObject();
		property.isDelete = true;
	}

	public static void DeletePuddle(Properties property, float delay)
	{
		property.delayDelete = delay;
		property.iPuddle.Attack(GameData.countElectroAttack);
		//property.isDelete = true;
	}

	public static void DeleteSnow(Properties property, float delay)
	{
		property.delayDelete = delay;
		property.iSnow.DeleteObject();
		property.isDelete = true;
	}

	public static void DeleteIce(Properties property, float delay)
	{
		property.delayDelete = delay;
		property.iIce.PrepareDelete(delay);
		property.isDelete = true;
	}

	/// <summary>
	/// Сбрасывает выделение.
	/// </summary>
	public static void Reset()
	{
		lineManager.Remove ();
		if(selectedObjects.Count < 3)
		{
			foreach(Properties prop in selectedObjects)
			{
				prop.SetState(StateObjects.Normal);
//				if(prop.iPrism!=null)
//				{
//					prop.iPrism.State(PrismState.MouseUp);
//				}
			}
		}
		selectedObjects.Clear ();
		selectedColor = Colors.Empty;

		SwitchPrismUI (null);

		soundManager.ResetSelect ();

//		
		if(tutorial!=null&& tutorial.needStep)
		{
			tutorial.ResetTutorial ();
		}

//		SwitchPUsUI(false);
	}

	/// <summary>
	/// Sets the input.
	/// </summary>
	/// <param name="state">If set to <c>true</c> state.</param>
	public static void SetInput(bool state)
	{
		isInput = state;
	}

	/// <summary>
	/// Scores the selected.
	/// </summary>
	/// <returns>The selected.</returns>
	/// <param name="selectedObjects">Selected objects.</param>
	public static void ScoreSelected(List<Properties> selectedObjects)
	{
		for (int i=0; i< selectedObjects.Count; i++)
		{
			int countIncreaseBonus = 3;
			int valueIncreaseBonus = i/countIncreaseBonus;
			if(valueIncreaseBonus > 0)
			{
				selectedObjects[i].iPoints.SetBonusPoint(PointManager.bonusSelected * valueIncreaseBonus);
			}
		}
		//starManager.SetFull ();
	}

	public static int CountSelectedForElectro()
	{
		int countElectro = (int)(selectedObjects.Count/GameData.countForArrow);
		return countElectro;
	}

	/// <summary>
	/// Победа когда все задания выполнены.
	/// </summary>
	/// <returns><c>true</c>, if level was windowed, <c>false</c> otherwise.</returns>
	public static bool WinLevel()
	{
		bool isWin = true;
		foreach (TaskLevel task in GameData.taskLevel)
		{
			if(!task.EndTask())
			{
				isWin = false;
				break;
			}
		}

		if(isWin)
		{
            Debug.Log("isWin");
            ProgressController.instance.SetProgress("Explorer", GamePlay.maxCompleteLevel);
			if(GamePlay.maxCompleteLevel < GameData.numberLoadLevel)
			{
                GamePlay.maxCompleteLevel = GameData.numberLoadLevel;               
			}
		}

		return isWin;
	}

	/// <summary>
	/// Проигрыш когда закончился лимит.
	/// </summary>
	/// <returns><c>true</c>, if level was lost, <c>false</c> otherwise.</returns>
	public static bool LoseLevel()
	{
		return GameData.limit.EndLimit ();
	}

	/// <summary>
	/// Restarts the level.
	/// </summary>
	public static void RestartLevel()
	{
        UnityEngine.SceneManagement.SceneManager.LoadScene("SplashScreen");
	}	

	/// <summary>
	/// Add the task value.
	/// </summary>
	/// <param name="type">Type.</param>
	/// <param name="value">Value.</param>
	public static void AddTaskValue(Task type, int value)
	{
		foreach (TaskLevel task in GameData.taskLevel)
		{
			if(task.GetTaskType() == type)
			{
				task.SetCurrent(value);
				//break;
			}
		}
	}

	/// <summary>
	/// Changes the image jam.
	/// </summary>
	public static void ChangeImageJam()
	{

		List<Properties> propeties = GameData.manager.GetAllObjectsOfType (ObjectTypes.Jam);
		foreach(Properties jam in propeties)
		{
			GameData.manager.ChangeImageJam(jam);

			jam.iJam.ChangeImage();
		}
	}

	public static bool NeedDeleteDiamond()
	{
		List<Properties> diamonds = GameData.manager.GetAllObjectsOfType (ObjectTypes.Diamond);
		foreach(Properties property in diamonds)
		{
			if(property.iDiamond.IsLastPostionJ())
			{
				return true;
			}
		}
		return false;
	}

	public static void DeleteDiamond()
	{
		List<Properties> diamonds = GameData.manager.GetAllObjectsOfType (ObjectTypes.Diamond);
		foreach(Properties property in diamonds)
		{
			if(property.iDiamond.IsLastPostionJ())
			{
				property.iDiamond.PrepareDelete();
			}
		}
	}

	public static void CreateElectro(int count)
	{
		List<Properties> jellies;
		int randomObject;
		for(int i=0; i< count; i++)
		{
			jellies = GameData.manager.GetAllObjectsOfTypeInField (ObjectTypes.Jelly);
			randomObject = Random.Range(0, jellies.Count);
			GameField.ReplaceObject(jellies[randomObject], ObjectTypes.Electro, Colors.Empty); 
			jellies.RemoveAt(randomObject);
		}
	}

//	public static void CreateBomb(int[] posIJ, Vector3 pos)
//	{
//		Properties obj = GameField.CreateGameObject (pos, GameData.pool.GetObject (ObjectTypes.Bomb, selectedColor), selectedColor, ObjectTypes.Bomb, 0);
//		GameData.manager.AddObject (posIJ [0], posIJ [1], obj);
//		DeleteJam (obj);
//	}

	public static bool NeedCreateElectro()
	{
		return countElectro > 0;
	}

	public static void DeleteColorJelly(Colors color)
	{
		List<Properties> deleteObjects = GameData.manager.GetAllObjectsOfTypeInField (ObjectTypes.Jelly);
		foreach(Properties property in deleteObjects)
		{
			if(property.iColor.GetColor() == color)
			{
				GamePlay.DeleteJelly(property, 0, false);
			}
		}

		ResetDeleteElectro ();

	}

	public static void CreateBonus()
	{
		//startBonusTime = true;
		int countMoves = GameData.limit.GetValueLimit ();
		//Временная заглушка количества удалений
		GameData.countElectroForBonus = countMoves / 5 + 1;
		if(GameData.countElectroForBonus>10)
		{
			GameData.countElectroForBonus = 10;
		}
		if(GameData.countElectroForBonus<3)
		{
			GameData.countElectroForBonus = 3;
		}
//		if(countMoves>25)
//		{
//			GameData.countElectroForBonus = 6;
//		}
//		else if(countMoves>20)
//		{
//			GameData.countElectroForBonus = 5;
//		}
//		else if(countMoves>15)
//		{
//			GameData.countElectroForBonus = 4;
//		}
//		else if(countMoves>10)
//		{
//			GameData.countElectroForBonus = 3;
//		}

		if(countMoves>GameData.countElectroForBonus)
		{
			countMoves = GameData.countElectroForBonus;
		}
		
		CreateElectro (countMoves);
		GameData.limit.ChangeLimit (Limit.Moves, -countMoves);
	//	starManager.SetFull ();
	}

	public static bool DestroyBonus()
	{
		List<Properties> electos = GameData.manager.GetAllObjectsOfType (ObjectTypes.Electro);
		List<Properties> bombs = GameData.manager.GetAllObjectsOfType (ObjectTypes.Bomb);
		List<Properties> prism = GameData.manager.GetAllObjectsOfType (ObjectTypes.Prism);

		if(electos.Count<1&&bombs.Count<1&&prism.Count<1)
		{
			return false;
		}
		foreach(Properties property in electos)
		{
			GamePlay.DeleteElectro(property, 0);
		}
		foreach(Properties property in bombs)
		{
			GamePlay.DeleteBomb(property, 0);
		}

		foreach(Properties property in prism)
		{
			property.iPrism.RandomColor();
			property.iPrism.SetSpeed(0.1f);
			property.iPrism.Resume();
			property.iPrism.stateActive = true;
			GamePlay.DeletePrism(property, 0.5f);
		}

		//GameData.manager.OffsetObject ();
		return true;
	}

	public static void CreateScoreText(Properties property)
	{
		GameObject obj = MonoBehaviour.Instantiate (GameData.pool.GetObject (ObjectTypes.ScoreText, Colors.Empty)) as GameObject;
		obj.transform.position = new Vector3(property.transform.position.x,
		                                     property.transform.position.y,
		                                     property.transform.position.z-GameData.distanceBetwObject);
		int countPoints = property.iPoints.GetPoint ();
		if(bonusTime)
		{
			countPoints*=GameData.multiplyBonusTimePoints;
		}
		obj.GetComponent<ScoreText> ().StartAnimation (countPoints);
	}

	public static void AttackPuddle(List<Properties> property)
	{
		foreach(Properties prop in property)
		{
			if(prop.GetTypeObject() == ObjectTypes.Puddle)
			{
				prop.iPuddle.Attack(property.Count - 1);
			}
		}
	}

	public static void AttackStringDig(Properties property)
	{
		int[] posIJ = GameData.manager.ReturnIJPosObject (property);
		List<Properties> objects = GameData.manager.GetObjectInString (ObjectTypes.BlackJelly, posIJ [1]);
		int countDelete = 0;
		foreach(Properties prop in objects)
		{
			if(!prop.isDelete)
			{
				countDelete++;
			}
		}
		if(countDelete == 1)
		{
			GamePlay.AddTaskValue(Task.Dig, 1);
		}
	}

	public static bool Feed2Save()
	{
		List<Properties> objs = GameData.manager.GetAllObjectsOfTypeInField (ObjectTypes.Feed2);
		bool needDelete = false;
		foreach(Properties property in objs)
		{
			int[] posIJ = GameData.manager.ReturnIJPosObject(property);
			Properties prop = GameData.manager.ReturnObjectOfIJPos(posIJ[0], posIJ[1]+1);
			if(prop!=null && prop.GetTypeObject()==ObjectTypes.Jelly && prop.iColor.GetColor()==property.iColor.GetColor())
			{
				((Jelly)(prop.iJelly)).AnimationSave(property);
				needDelete = true;
			}
		}
		return needDelete;
	}

	public static bool NeedCreateSlime()
	{
		if(!GamePlay.deleteSlime)
		{
			List<Properties> objs = GameData.manager.GetAllObjectsOfTypeInField (ObjectTypes.Slime);
			List<Properties> spaceSlime = new List<Properties>();
			foreach(Properties prop in objs)
			{
				int[] posIJ = GameData.manager.ReturnIJPosObject (prop);
//				int count = 1;
				List<Properties> jellies = GameData.manager.ReturnTypeAround90InField(ObjectTypes.Jelly, posIJ);
				foreach(Properties pJelly in jellies)
				{
					spaceSlime.Add(pJelly);  
				}
			}

			if(spaceSlime.Count>0)
			{
				int random = Random.Range(0, spaceSlime.Count);
				GameField.ReplaceObject(spaceSlime[random], ObjectTypes.Slime, Colors.Empty);
			}
			GamePlay.deleteSlime = true;
			return true;
		}
		return false;
	}

	public static void LoadSoundSettings()
	{
		int sound = PlayerPrefs.GetInt ("sound");
		/*int music = PlayerPrefs.GetInt ("music");
		if(music==0)
		{
			musicOn = true;
			musicSource.volume = 1f;
//			musicSource.Play();
		}
		else
		{
			musicOn = false;
			musicSource.volume = 0;
		}*/
		if(sound==0)
		{
			soundOn = true;
		}
		else
		{
			soundOn = false;
		}
	}

    public static void MusicSettings()
    {
        int music = PlayerPrefs.GetInt("music");
        string nameOfScene = SceneManager.GetActiveScene().name;

        if (music == 0)
        {
            musicOn = true;
            musicSource.volume = 1;

            if ("GameField".Equals(nameOfScene) && GamePlay.soundOn) return;

            musicSource.UnPause();

        }
        else
        {
            musicOn = false;
            musicSource.volume = 0;
            musicSource.Pause();
        }
    }
    public static void SoundSettings()
    {
        int sound = PlayerPrefs.GetInt("sound");
        string nameOfScene = SceneManager.GetActiveScene().name;

        if (sound == 0)
        {
            soundOn = true;
            if ("GameField".Equals(nameOfScene) && GamePlay.musicOn) musicSource.Pause();
        }
        else
        {
            soundOn = false;
            if ("GameField".Equals(nameOfScene) && GamePlay.musicOn) musicSource.UnPause();
        }
    }
    /*public static void ChangeCountLife(int value)
	{
		GamePlay.currentCountLife+=value;
		if(GamePlay.currentCountLife>10)
		{
			currentCountLife = 10;
		}
		PlayerPrefs.SetInt("countLife", GamePlay.currentCountLife);
		if(GamePlay.currentCountLife >= 9) 
		{
			SaveLastTime();
		}
	}*/

	public static void SaveLastTime()
	{
		long nowTime = System.DateTime.Now.ToFileTime ();
		nowTime /= 10000000;
		PlayerPrefs.SetString ("lastTimeLife", nowTime.ToString ());
        PlayerPrefs.Save();
	}

	public static void EnableButtonsMap(bool state)
	{
		foreach(Transform ob in mapController.levels)
		{
			if(ob!=null)
			{
				ob.GetComponent<SphereCollider>().enabled = state;
			}
		}
	}

	public static int GetCountPU(PU powerUp)
	{
		switch(powerUp)
		{
			case PU.Arrow:
				return PlayerPrefs.GetInt("Arrows");
			case PU.Bomb:
				return PlayerPrefs.GetInt("Bombs");
			case PU.Prism:
				return PlayerPrefs.GetInt("Prisms");
			default:
				return 0;
		}
	}

	public static void IncCountPU(PU powerUp,int count)
	{
		switch(powerUp)
		{
			case PU.Arrow:
				PlayerPrefs.SetInt("Arrows",PlayerPrefs.GetInt("Arrows")+count);
				break;
			case PU.Bomb:
				PlayerPrefs.SetInt("Bombs",PlayerPrefs.GetInt("Bombs")+count);
				break;
			case PU.Prism:
				PlayerPrefs.SetInt("Prisms",PlayerPrefs.GetInt("Prisms")+count);
				break;
		}
	}

	public static bool DecCountPU(PU powerUp,int count)
	{
		int countPU = 0;
		bool status = false;

		switch(powerUp)
		{
			case PU.Arrow:
				countPU = PlayerPrefs.GetInt("Arrows");
				if(countPU-count>=0)
				{
					PlayerPrefs.SetInt("Arrows",countPU-count);
                    
					status = true;
				}
				break;
			case PU.Bomb:
				countPU = PlayerPrefs.GetInt("Bombs");
				if(countPU-count>=0)
				{
					PlayerPrefs.SetInt("Bombs",countPU-count);
					status = true;
				}
				break;
			case PU.Prism:
				countPU = PlayerPrefs.GetInt("Prisms");
				if(countPU-count>=0)
				{
					PlayerPrefs.SetInt("Prisms",countPU-count);
					status = true;
				}
				break;
		}
        PlayerPrefs.Save();
        return status;
	}
}

