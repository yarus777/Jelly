using System;
using System.Collections.Generic;

using UnityEngine;
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
    private static Colors selectedColor = Colors.Empty;

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
    public static int countElectro;

    public static List<Properties> deleteObjectOnStroke;

    public static List<Properties> electroForDelete;
    public static List<Properties> bombForDelete;
    public static List<Properties> prismForDelete;

    /// <summary>
    /// Количество использованных электромонстриков в уровне
    /// </summary>
    public static int countDeleteElectro;

    public static bool stateFireball;

    public static bool startBonusTime;

    public static bool bonusTime;

    public static bool bonusTimeInterfase;

    public static bool startGame;

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


    public static bool isTutorialActive;

    private static int _lastOpenedLvl;

    public static int LastOpenedLvl
    {
        get
        {
            return PlayerPrefs.GetInt("lastOpenLevel", 1);          
        }
        set
        {
            _lastOpenedLvl = value;
            PlayerPrefs.SetInt("lastOpenLevel", _lastOpenedLvl);
            PlayerPrefs.Save();
            Debug.Log("SET: lastOpenLevel:" + _lastOpenedLvl);
        }
    }

    private static int _maxCompleteLevel;

    public static int maxCompleteLevel {
        get { return PlayerPrefs.GetInt("maxCompleteLevel", 0); }
        set {
            _maxCompleteLevel = value;
            PlayerPrefs.SetInt("maxCompleteLevel", _maxCompleteLevel);
            PlayerPrefs.Save();
            Debug.Log("SET: maxCompleteLevel:" + _maxCompleteLevel);
        }
    }

    private static int _fullStarsLevelCount;

    public static int FullStarsLevelCount {
        get { return PlayerPrefs.GetInt("fullStarsLevelCount", 0); }
        set {
            _fullStarsLevelCount = value;
            PlayerPrefs.SetInt("fullStarsLevelCount", _fullStarsLevelCount);
            PlayerPrefs.Save();
        }
    }

    private static int _deletedBombsCount;

    public static int DeletedBombsCount {
        get { return PlayerPrefs.GetInt("deletedBombsCount", 0); }
        set {
            _deletedBombsCount = value;
            PlayerPrefs.SetInt("deletedBombsCount", _deletedBombsCount);
            PlayerPrefs.Save();
        }
    }

    private static int _deletedPrismsCount;

    public static int DeletedPrismsCount {
        get { return PlayerPrefs.GetInt("deletedPrismsCount", 0); }
        set {
            _deletedPrismsCount = value;
            PlayerPrefs.SetInt("deletedPrismsCount", _deletedPrismsCount);
            PlayerPrefs.Save();
        }
    }


    private static int _deletedGreenJamCount;

    public static int DeletedGreenJamCount {
        get { return PlayerPrefs.GetInt("deletedGreenJamCount", 0); }
        set {
            _deletedGreenJamCount = value;
            PlayerPrefs.SetInt("deletedGreenJamCount", _deletedGreenJamCount);
            PlayerPrefs.Save();
        }
    }


    public static int countStarsLevel;


    public static StateInterfaceGame interfaceGame;

    public static TutorialManager tutorial;

    public static bool enableTutorial = false;

    public static TeacherManager teacher;

    public static AudioSource musicSource;
    public static bool musicOn;
    public static bool soundOn;

    public static bool endTargetAnimation;

    public static bool enableButtonInterface = true;

    public static Finger finger;

    public static bool isAnimationFinger;

    public static string lifeTimeString;

    public static float timePhysics = 0.02f;

    public static WordsManager wordsManager;

    public static MovesUI moveUI;
    public static PrismUI prismUI;
    public static PU puActive = PU.Empty;

    public static GameInterface gameUI;
    //public static GameFieldScene gameUI;

    public static bool notDeleteObject;


    public static LoadVideo loadVideo;

    public static EUI stateUI;

    public static bool onlyOneMoves;

    #endregion

    public static void Restart() {
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
        notDeleteObject = false;
        stateUI = EUI.Empty;
        puActive = PU.Empty;
    }

    public static void SwithFireball() {
        stateFireball = !stateFireball;
    }

    public static void AddDeleteElectro() {
        countDeleteElectro++;
    }

    public static void ResetDeleteElectro() {
        countDeleteElectro = 0;
        stateFireball = false;
    }

    /// <summary>
    /// Adds the objects.
    /// </summary>
    /// <param name="transformGameObj">Transform game object.</param>
    public static void AddObjects(Transform transformGameObj) {
        var property = transformGameObj.GetComponent<Properties>();
        if (property != null) {
            if (property.canSelected) {
                switch (property.GetTypeObject()) {
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
    public static void SelectedObj(Properties property) {
        if (property.iColor == null) {
            return;
        }

        if (selectedObjects.Count < 1) {
            soundManager.CreateSelect(SoundsManager.Duration.Up);

            if (finger != null) {
                finger.Pause();
            }

            property.SetState(StateObjects.Selected);
            selectedObjects.Add(property);
            if (property.iColor.GetColor() != Colors.Prism) {
                selectedColor = property.iColor.GetColor();
                GameData.manager.SetShadows(StatementShadow.On, selectedColor);
            }
            lastPos = property.transform.localPosition;
            lastProperty = property;

            SwitchPrismUI(property);
        } else {
            if (isNear(lastPos, property.transform.localPosition)) {
                if (property.GetState() == StateObjects.Normal) {
                    if (selectedColor == Colors.Empty && lastProperty.iColor.GetColor() == Colors.Prism) {
                        lastProperty.iPrism.Pause();
                    }
                    if (selectedColor == Colors.Empty && lastProperty.iColor.GetColor() == Colors.Prism &&
                        property.iColor.GetColor() != Colors.Prism) {
                        selectedColor = property.iColor.GetColor();
                        GameData.manager.SetShadows(StatementShadow.On, selectedColor);
                    } else if (selectedColor != Colors.Empty && property.iColor.GetColor() != Colors.Prism &&
                               selectedColor != property.iColor.GetColor()) {
                        return;
                    } else if (lastProperty.iColor.GetColor() == Colors.Prism && selectedColor != Colors.Empty) {
                        lastProperty.iPrism.Pause();
                    }


//					Debug.Log("AAA");
                    SwitchPrismUI(property);

                    soundManager.CreateSelect(SoundsManager.Duration.Up);

                    lastPos = property.transform.localPosition;

                    lineManager.CreateLineAtPositions(property, lastProperty, selectedColor);
                    property.SetState(StateObjects.Selected);
                    selectedObjects.Add(property);

                    lastProperty = property;
                } else {
                    if (property.GetInstanceID() == selectedObjects[selectedObjects.Count - 2].GetInstanceID()) {
                        if (property.iColor.GetColor() == Colors.Prism && selectedObjects.Count < 3) {
                            selectedColor = Colors.Empty;
                            GameData.manager.SetShadows(StatementShadow.Off, selectedColor);
                        }

                        SwitchPrismUI(property);

                        soundManager.CreateSelect(SoundsManager.Duration.Down);

                        lineManager.DeleteLine(lineManager.Count() - 1);
                        selectedObjects[selectedObjects.Count - 1].SetState(StateObjects.Normal);
                        property.SetState(StateObjects.Selected);
                        selectedObjects.RemoveAt(selectedObjects.Count - 1);
                        lastPos = property.transform.localPosition;
                        lastProperty = property;
                    }
                }
            }
        }
    }

    public static void SwitchPrismUI(Properties property) {
        if (property != null && property.iPrism != null) {
            moveUI.MoveSwitch(false);
            //GamePlay.prismUI.MoveSwitch(true);
        } else {
            //if(GamePlay.moveUI!=null&&GamePlay.prismUI!=null)
            if (moveUI != null) {
                if (puActive == PU.Empty) {
                    moveUI.MoveSwitch(true);
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
    public static bool isNear(Vector3 firstPosition, Vector3 secondPosition) {
        var distance = Vector3.Distance(firstPosition, secondPosition);
        var diagonal = GameData.distanceBetwObject*Mathf.Sqrt(2f);
        return (distance <= diagonal + 0.1f &&
                distance > 0);
    }

    /// <summary>
    /// Delete the objects.
    /// </summary>
    public static void DeleteObjects() {
        if (notDeleteObject) {
            return;
        }

        if (selectedObjects.Count > 2) {
            if (selectedObjects[selectedObjects.Count - 1] != null &&
                selectedObjects[selectedObjects.Count - 1].iPrism != null) {
                selectedObjects[selectedObjects.Count - 1].iPrism.Pause();
            }

            if (tutorial != null) {
                if (!tutorial.CorrectTutorial(selectedObjects)) {
//					GameData.manager.SetShadows(StatementShadow.Off, Colors.Empty);

                    foreach (var prop in selectedObjects) {
                        prop.SetState(StateObjects.Normal);
                    }
                    Reset();
                    finger.PrepareResume();
                    return;
                }
            }
            if (tutorial != null) {
                tutorial.needStep = true;
            }

            WordsInstance(selectedObjects.Count);

            isAnimationFinger = false;
            firstMove = false;
            isStroke = true;
            ScoreSelected(selectedObjects);
            GameData.limit.ChangeLimit(Limit.Moves, -1);
            GameData.limit.ChangeLimit(Limit.NotLimit, -1);
            var currentDelay = 0f;
            var stepDelay = 0.075f;

            deleteObjectOnStroke = new List<Properties>();
            electroForDelete = new List<Properties>();
            bombForDelete = new List<Properties>();
            prismForDelete = new List<Properties>();

            ObjectTypes[] types = {ObjectTypes.Jelly};

            var propertyCreate = GameData.manager.GetLastPropOfType(selectedObjects, types);

            if (propertyCreate != null) {
                if (selectedObjects.Count >= GameData.countForBomb && selectedObjects.Count < GameData.countForArrow) {
                    if (GameData.parser.isBombs) {
                        propertyCreate.createPUs = ObjectTypes.Bomb;
                        propertyCreate.selectedColor = selectedColor;
                    }
                } else if (selectedObjects.Count >= GameData.countForArrow &&
                           selectedObjects.Count < GameData.countForPrism) {
                    if (GameData.parser.isArrows) {
                        propertyCreate.createPUs = ObjectTypes.Electro;
                        propertyCreate.selectedColor = selectedColor;
                    }
                } else if (selectedObjects.Count >= GameData.countForPrism) {
                    if (GameData.parser.isPrisms) {
                        propertyCreate.createPUs = ObjectTypes.Prism;
                        propertyCreate.selectedColor = Colors.Prism;
                    }
                }
            }

            foreach (var prop in selectedObjects) {
                if (prop != null && !prop.isDelete) {
                    switch (prop.GetTypeObject()) {
                        case ObjectTypes.Jelly:
                            DeleteJelly(prop, currentDelay, false);
                            currentDelay += stepDelay;
                            prop.deleteLine = true;
                            break;
                        case ObjectTypes.Electro:
                            if (!prop.iElectro.stateActive) {
                                electroForDelete.Add(prop);
                                prop.iElectro.stateActive = true;
                                prop.delayDelete = currentDelay;
                            }
                            currentDelay += stepDelay;
                            break;
                        case ObjectTypes.Puddle:
                            if (!prop.iPuddle.stateActive) {
                                Debug.Log("Puddle");
                                prop.iPuddle.stateActive = true;
                                prop.delayDelete = currentDelay;
                                prop.iPuddle.Attack(selectedObjects.Count - 1);
                            }
//							isPuddle = true;
                            break;
                        case ObjectTypes.Bomb:
                            if (!prop.iBomb.stateActive) {
                                bombForDelete.Add(prop);
                                prop.iBomb.stateActive = true;
                                prop.delayDelete = currentDelay;
                            }
                            currentDelay += stepDelay;
                            break;
                        case ObjectTypes.Prism:
                            if (!prop.iPrism.stateActive) {
                                prismForDelete.Add(prop);
                                prop.iPrism.stateActive = true;
                                prop.delayDelete = currentDelay;
                            }
                            currentDelay += stepDelay;
                            break;
                        case ObjectTypes.Empty:
                            break;
                    }
                }
            }
            Debug.LogWarning("GamePLay");
            while (electroForDelete.Count > 0
                   || bombForDelete.Count > 0
                   || prismForDelete.Count > 0
                ) {
                if (electroForDelete.Count > 0) {
                    DeleteElectro(electroForDelete[0], electroForDelete[0].delayDelete);
                    electroForDelete.RemoveAt(0);
                }
                if (bombForDelete.Count > 0) {
                    DeleteBomb(bombForDelete[0], bombForDelete[0].delayDelete);
                    bombForDelete.RemoveAt(0);
                }
                if (prismForDelete.Count > 0) {
                    DeletePrism(prismForDelete[0], prismForDelete[0].delayDelete);
                    prismForDelete.RemoveAt(0);
                }
            }
        } else if (selectedObjects.Count == 1) {
            CreatePU(selectedObjects[0]);
        }

        if (tutorial == null) {
            GameData.manager.SetShadows(StatementShadow.Off, selectedColor);
        } else {
            if (!tutorial.isTutorial()) {
                GameData.manager.SetShadows(StatementShadow.Off, selectedColor);
            }
        }


        lvlManager.Stroke();
        Reset();
        if (finger != null) {
            finger.PrepareResume();
        }
    }

    private static void CreatePU(Properties property) {
        if (property.GetTypeObject() == ObjectTypes.Jelly) {
            if (DecCountPU(puActive, 1)) {
                var type = ObjectTypes.Jelly;
                switch (puActive) {
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
            } else {
                ShowInventory();
            }
        }
    }

    private static void ShowInventory() {
    }

    private static void WordsInstance(int count) {
        if (count == 7) {
            wordsManager.InstanceText(Words.Cool);
        } else if (count == 8) {
            wordsManager.InstanceText(Words.Super);
        } else if (count >= 9 && count < 12) {
            wordsManager.InstanceText(Words.Great);
        } else if (count >= 12 && count < 15) {
            wordsManager.InstanceText(Words.Splendid);
        } else if (count >= 15 && count < 17) {
            wordsManager.InstanceText(Words.Amazing);
        } else if (count >= 17 && count < 20) {
            wordsManager.InstanceText(Words.Brilliant);
        } else if (count >= 20) {
            wordsManager.InstanceText(Words.Awesome);
        }
    }

    public static void DeleteJelly(Properties property, float delay, bool electroLine) {
        if (!electroLine) {
            foreach (var blackHero in GameData.manager.ArroundObject(ObjectTypes.BlackJelly, property)) {
                blackHero.iBlackHero.Attack(1);
//				blackHero.isDelete = true;
            }

            foreach (var ice in GameData.manager.ArroundObject(ObjectTypes.Ice, property)) {
                ice.iIce.PrepareDelete(delay);
                ice.isDelete = true;
            }

            foreach (var snow in GameData.manager.ArroundObject(ObjectTypes.Snow, property)) {
                snow.iSnow.DeleteObject();
                snow.isDelete = true;
            }

            foreach (var stone in GameData.manager.ArroundObject(ObjectTypes.StoneJelly, property)) {
                stone.iStone.DeleteObject();
                stone.isDelete = true;
            }
            foreach (var slime in GameData.manager.ArroundObject(ObjectTypes.Slime, property)) {
                slime.iSlime.PrepareDelete(delay);
                slime.isDelete = true;
            }
        }
        property.delayDelete = delay;
        property.iJelly.PrepareDelete();
        property.isDelete = true;
    }

    public static void DeleteBlackHero(Properties property, float delay) {
        property.delayDelete = delay;
        property.iBlackHero.Attack(1);
//		property.isDelete = true;
    }

    public static void DeleteElectro(Properties property, float delay) {
        property.delayDelete = delay;
        var deleteElectroV = new ElectroLine();
        deleteElectroV.SetListProperty(property, RemovingDirection.Vertical, delay);
        var deleteElectroH = new ElectroLine();
        deleteElectroH.SetListProperty(property, RemovingDirection.Horizontal, delay);
        property.isDelete = true;
        property.iElectro.PrepareDelete();
    }

    public static void DeleteBomb(Properties property, float delay) {
        //Debug.Log("Delete Bomb");
        DeletedBombsCount++;
        ProgressController.instance.SetProgress("Exploder", DeletedBombsCount);

        property.delayDelete = delay;
        var deleteBomb = new BombLine();
        deleteBomb.SetListProperty(property, delay);
        property.isDelete = true;
        property.iBomb.PrepareDelete();
    }

    public static void DeletePrism(Properties property, float delay) {
        //Debug.Log("Delete Prism");
        DeletedPrismsCount++;
        ProgressController.instance.SetProgress("Sweet Tooth", DeletedPrismsCount);


        property.delayDelete = delay;
        var deletePrism = new PrismLine();


        //deletePrism.SetListProperty (property, delay, property.iPrism.GetColor());
        deletePrism.SetListProperty(property, delay, selectedColor);

        property.isDelete = true;
        property.iPrism.PrepareDelete();
    }

    public static void DeleteJam(Properties property) {
        //Debug.Log("DeleteJam " + property.iColor.GetColor());

        if (property.iColor.GetColor() == Colors.Green) {
            DeletedGreenJamCount++;
            ProgressController.instance.SetProgress("Jelly keeper", DeletedGreenJamCount);
        }


        var jam = GameData.manager.GetJam(property);
        if (jam != null) {
            jam.iJam.PrepareDelete();
            property.iPoints = new Points(property.iPoints.GetPoint() + PointManager.jam);
            GameData.manager.DeleteObject(jam);
        }
    }

    public static void DeleteStone(Properties property, float delay) {
        property.delayDelete = delay;
        property.iStone.DeleteObject();
        property.isDelete = true;
    }

    public static void DeletePuddle(Properties property, float delay) {
        property.delayDelete = delay;
        property.iPuddle.Attack(GameData.countElectroAttack);
        //property.isDelete = true;
    }

    public static void DeleteSnow(Properties property, float delay) {
        property.delayDelete = delay;
        property.iSnow.DeleteObject();
        property.isDelete = true;
    }

    public static void DeleteIce(Properties property, float delay) {
        property.delayDelete = delay;
        property.iIce.PrepareDelete(delay);
        property.isDelete = true;
    }

    /// <summary>
    /// Сбрасывает выделение.
    /// </summary>
    public static void Reset() {
        lineManager.Remove();
        if (selectedObjects.Count < 3) {
            foreach (var prop in selectedObjects) {
                prop.SetState(StateObjects.Normal);
//				if(prop.iPrism!=null)
//				{
//					prop.iPrism.State(PrismState.MouseUp);
//				}
            }
        }
        selectedObjects.Clear();
        selectedColor = Colors.Empty;

        SwitchPrismUI(null);

        soundManager.ResetSelect();

//		
        if (tutorial != null && tutorial.needStep) {
            tutorial.ResetTutorial();
        }

//		SwitchPUsUI(false);
    }

    /// <summary>
    /// Sets the input.
    /// </summary>
    /// <param name="state">If set to <c>true</c> state.</param>
    public static void SetInput(bool state) {
        isInput = state;
    }

    /// <summary>
    /// Scores the selected.
    /// </summary>
    /// <returns>The selected.</returns>
    /// <param name="selectedObjects">Selected objects.</param>
    public static void ScoreSelected(List<Properties> selectedObjects) {
        for (var i = 0; i < selectedObjects.Count; i++) {
            var countIncreaseBonus = 3;
            var valueIncreaseBonus = i/countIncreaseBonus;
            if (valueIncreaseBonus > 0) {
                selectedObjects[i].iPoints.SetBonusPoint(PointManager.bonusSelected*valueIncreaseBonus);
            }
        }
        //starManager.SetFull ();
    }

    public static int CountSelectedForElectro() {
        var countElectro = selectedObjects.Count/GameData.countForArrow;
        return countElectro;
    }

    /// <summary>
    /// Победа когда все задания выполнены.
    /// </summary>
    /// <returns><c>true</c>, if level was windowed, <c>false</c> otherwise.</returns>
    public static bool WinLevel() {
        var isWin = true;
        foreach (var task in GameData.taskLevel) {
            if (!task.EndTask()) {
                isWin = false;
                break;
            }
        }

        if (isWin) {
            Debug.Log("isWin");
            ProgressController.instance.SetProgress("Explorer", maxCompleteLevel);
            if (maxCompleteLevel < GameData.numberLoadLevel) {
                maxCompleteLevel = GameData.numberLoadLevel;
            }
        }

        return isWin;
    }

    /// <summary>
    /// Проигрыш когда закончился лимит.
    /// </summary>
    /// <returns><c>true</c>, if level was lost, <c>false</c> otherwise.</returns>
    public static bool LoseLevel() {
        return GameData.limit.EndLimit();
    }

    /// <summary>
    /// Restarts the level.
    /// </summary>
    public static void RestartLevel() {
        SceneManager.LoadScene("SplashScreen");
    }

    /// <summary>
    /// Add the task value.
    /// </summary>
    /// <param name="type">Type.</param>
    /// <param name="value">Value.</param>
    public static void AddTaskValue(Task type, int value) {
        foreach (var task in GameData.taskLevel) {
            if (task.GetTaskType() == type) {
                task.SetCurrent(value);
                OnTaskValueUpdated();
                //break;
            }
        }
    }

    public static event Action TaskValueUpdated;
    private static void OnTaskValueUpdated()
    {
        if (TaskValueUpdated != null)
        {
            TaskValueUpdated();
        }
    }

    /// <summary>
    /// Changes the image jam.
    /// </summary>
    public static void ChangeImageJam() {
        var propeties = GameData.manager.GetAllObjectsOfType(ObjectTypes.Jam);
        foreach (var jam in propeties) {
            GameData.manager.ChangeImageJam(jam);

            jam.iJam.ChangeImage();
        }
    }

    public static bool NeedDeleteDiamond() {
        var diamonds = GameData.manager.GetAllObjectsOfType(ObjectTypes.Diamond);
        foreach (var property in diamonds) {
            if (property.iDiamond.IsLastPostionJ()) {
                return true;
            }
        }
        return false;
    }

    public static void DeleteDiamond() {
        var diamonds = GameData.manager.GetAllObjectsOfType(ObjectTypes.Diamond);
        foreach (var property in diamonds) {
            if (property.iDiamond.IsLastPostionJ()) {
                property.iDiamond.PrepareDelete();
            }
        }
    }

    public static void CreateElectro(int count) {
        List<Properties> jellies;
        int randomObject;
        for (var i = 0; i < count; i++) {
            jellies = GameData.manager.GetAllObjectsOfTypeInField(ObjectTypes.Jelly);
            randomObject = Random.Range(0, jellies.Count);
            GameField.ReplaceObject(jellies[randomObject], ObjectTypes.Electro, Colors.Empty);
            jellies.RemoveAt(randomObject);
        }
    }


    public static bool NeedCreateElectro() {
        return countElectro > 0;
    }

    public static void DeleteColorJelly(Colors color) {
        var deleteObjects = GameData.manager.GetAllObjectsOfTypeInField(ObjectTypes.Jelly);
        foreach (var property in deleteObjects) {
            if (property.iColor.GetColor() == color) {
                DeleteJelly(property, 0, false);
            }
        }

        ResetDeleteElectro();
    }

    public static void CreateBonus() {
        //startBonusTime = true;
        var countMoves = GameData.limit.GetValueLimit();
        //Временная заглушка количества удалений
        GameData.countElectroForBonus = countMoves/5 + 1;
        if (GameData.countElectroForBonus > 10) {
            GameData.countElectroForBonus = 10;
        }
        if (GameData.countElectroForBonus < 3) {
            GameData.countElectroForBonus = 3;
        }


        if (countMoves > GameData.countElectroForBonus) {
            countMoves = GameData.countElectroForBonus;
        }

        CreateElectro(countMoves);
        GameData.limit.ChangeLimit(Limit.Moves, -countMoves);
        //	starManager.SetFull ();
    }

    public static bool DestroyBonus() {
        var electos = GameData.manager.GetAllObjectsOfType(ObjectTypes.Electro);
        var bombs = GameData.manager.GetAllObjectsOfType(ObjectTypes.Bomb);
        var prism = GameData.manager.GetAllObjectsOfType(ObjectTypes.Prism);

        if (electos.Count < 1 && bombs.Count < 1 && prism.Count < 1) {
            return false;
        }
        foreach (var property in electos) {
            DeleteElectro(property, 0);
        }
        foreach (var property in bombs) {
            DeleteBomb(property, 0);
        }

        foreach (var property in prism) {
            property.iPrism.RandomColor();
            property.iPrism.SetSpeed(0.1f);
            property.iPrism.Resume();
            property.iPrism.stateActive = true;
            DeletePrism(property, 0.5f);
        }

        //GameData.manager.OffsetObject ();
        return true;
    }

    public static void CreateScoreText(Properties property) {
        var obj = MonoBehaviour.Instantiate(GameData.pool.GetObject(ObjectTypes.ScoreText, Colors.Empty));
        obj.transform.position = new Vector3(property.transform.position.x,
            property.transform.position.y,
            property.transform.position.z - GameData.distanceBetwObject);
        var countPoints = property.iPoints.GetPoint();
        if (bonusTime) {
            countPoints *= GameData.multiplyBonusTimePoints;
        }
        obj.GetComponent<ScoreText>().StartAnimation(countPoints);
    }

    public static void AttackPuddle(List<Properties> property) {
        foreach (var prop in property) {
            if (prop.GetTypeObject() == ObjectTypes.Puddle) {
                prop.iPuddle.Attack(property.Count - 1);
            }
        }
    }

    public static void AttackStringDig(Properties property) {
        var posIJ = GameData.manager.ReturnIJPosObject(property);
        var objects = GameData.manager.GetObjectInString(ObjectTypes.BlackJelly, posIJ[1]);
        var countDelete = 0;
        foreach (var prop in objects) {
            if (!prop.isDelete) {
                countDelete++;
            }
        }
        if (countDelete == 1) {
            AddTaskValue(Task.Dig, 1);
        }
    }

    public static bool Feed2Save() {
        var objs = GameData.manager.GetAllObjectsOfTypeInField(ObjectTypes.Feed2);
        var needDelete = false;
        foreach (var property in objs) {
            var posIJ = GameData.manager.ReturnIJPosObject(property);
            var prop = GameData.manager.ReturnObjectOfIJPos(posIJ[0], posIJ[1] + 1);
            if (prop != null && prop.GetTypeObject() == ObjectTypes.Jelly &&
                prop.iColor.GetColor() == property.iColor.GetColor()) {
                ((Jelly) (prop.iJelly)).AnimationSave(property);
                needDelete = true;
            }
        }
        return needDelete;
    }

    public static bool NeedCreateSlime() {
        if (!deleteSlime) {
            var objs = GameData.manager.GetAllObjectsOfTypeInField(ObjectTypes.Slime);
            var spaceSlime = new List<Properties>();
            foreach (var prop in objs) {
                var posIJ = GameData.manager.ReturnIJPosObject(prop);
//				int count = 1;
                var jellies = GameData.manager.ReturnTypeAround90InField(ObjectTypes.Jelly, posIJ);
                foreach (var pJelly in jellies) {
                    spaceSlime.Add(pJelly);
                }
            }

            if (spaceSlime.Count > 0) {
                var random = Random.Range(0, spaceSlime.Count);
                GameField.ReplaceObject(spaceSlime[random], ObjectTypes.Slime, Colors.Empty);
            }
            deleteSlime = true;
            return true;
        }
        return false;
    }

    public static void LoadSoundSettings() {
        var sound = PlayerPrefs.GetInt("sound");
        Debug.Log("sound " + sound);
        if (sound == 0) {
            soundOn = true;
        } else {
            soundOn = false;
        }
    }


    public static int GetCountPU(PU powerUp) {
        switch (powerUp) {
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

    public static void IncCountPU(PU powerUp, int count) {
        switch (powerUp) {
            case PU.Arrow:
                PlayerPrefs.SetInt("Arrows", PlayerPrefs.GetInt("Arrows") + count);
                break;
            case PU.Bomb:
                PlayerPrefs.SetInt("Bombs", PlayerPrefs.GetInt("Bombs") + count);
                break;
            case PU.Prism:
                PlayerPrefs.SetInt("Prisms", PlayerPrefs.GetInt("Prisms") + count);
                break;
        }
    }

    public static bool DecCountPU(PU powerUp, int count) {
        var countPU = 0;
        var status = false;

        switch (powerUp) {
            case PU.Arrow:
                countPU = PlayerPrefs.GetInt("Arrows");
                if (countPU - count >= 0) {
                    PlayerPrefs.SetInt("Arrows", countPU - count);

                    status = true;
                }
                break;
            case PU.Bomb:
                countPU = PlayerPrefs.GetInt("Bombs");
                if (countPU - count >= 0) {
                    PlayerPrefs.SetInt("Bombs", countPU - count);
                    status = true;
                }
                break;
            case PU.Prism:
                countPU = PlayerPrefs.GetInt("Prisms");
                if (countPU - count >= 0) {
                    PlayerPrefs.SetInt("Prisms", countPU - count);
                    status = true;
                }
                break;
        }
        PlayerPrefs.Save();
        return status;
    }
}