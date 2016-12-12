using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// Класс данных.
/// </summary>
public static class GameData{

	/// <summary>
	/// Максимальный размер поля по ширине.
	/// </summary>
	public static int maxX = 7;

	/// <summary>
	/// Максимальный размер поля по высоте.
	/// </summary>
	public static int maxY = 9;

	/// <summary>
	/// Текущий размер поля по ширине.
	/// </summary>
	public static int sizeX;

	/// <summary>
	/// Текущий размер поля по высоте.
	/// </summary>
	public static int sizeY;

	/// <summary>
	/// Видимая часть поля
	/// </summary>
	public static int sizeYVisible;

	/// <summary>
	/// Расстояние между объектами.
	/// </summary>
	public static float distanceBetwObject = 1.86f;

	/// <summary>
	/// Очки набранные в течении уровня. 
	/// </summary>
	public static int score; 

	/// <summary>
	/// The window interface.
	/// </summary>
	public static int windowInterface;

	/// <summary>
	/// The parser.
	/// </summary>
	public static Parser parser = new Parser();

	/// <summary>
	/// Задание для уровня.
	/// </summary>
	public static List<TaskLevel> taskLevel;

	/// <summary>
	/// Лимит для уровня.
	/// </summary>
	public static Limitation limit;

	/// <summary>
	/// The timer.
	/// </summary>
	public static GameTimer timer;

	/// <summary>
	/// Шанс выпадения цвета.
	/// </summary>
	public static List<SChanceColor> colorChanses = new List<SChanceColor>();

	/// <summary>
	/// The pool.
	/// </summary>
	public static ObjectsPool pool = new ObjectsPool();

	/// <summary>
	/// The ObjectManager.
	/// </summary>
	public static ObjectManager manager;

	/// <summary>
	/// The speed move object.
	/// </summary>
	public static float speedMoveObject = 0.15f;

	/// <summary>
	/// The speed move all down.
	/// </summary>
	public static float speedMoveAllDown = 0.1f;

	/// <summary>
	/// The speed move all up.
	/// </summary>
	public static float speedMoveAllUp = 0.15f;

	/// <summary>
	/// The acceleration move object.
	/// </summary>
	public static float acceleration = 0.01f;

	/// <summary>
	/// The diamond manager.
	/// </summary>
	public static PotManager diamondManager = new PotManager();

	/// <summary>
	/// Номер загруженого/загружаемого уровня
	/// </summary>
	private static int _numberLoadLevel;
	public static int numberLoadLevel {
		get {
			return _numberLoadLevel;
		}
		set {
			_numberLoadLevel = value;
			Debug.Log ("numberLoadLevel: " + numberLoadLevel);
		}
	}

	/// <summary>
	/// Количество удаленных объектов для появления молнии
	/// </summary>
	public static int countForArrow = 7;

	/// <summary>
	/// Количество удаленных объектов для появления бомбы
	/// </summary>
	public static int countForBomb = 5;

    /// <summary>
    /// Количество удаленных объектов для появления призмы
    /// </summary>
	public static int countForPrism = 9;

	/// <summary>
	/// Количество электро монстров, создаваемых в бонус тайме за 1 проход
	/// </summary>
	public static int countElectroForBonus;

	public static int countElectroAttack = 3;

	public static int countAttackPuddle;
	
	public static int countStars = 3;

	public static StarManager starManager;

	public static int firstStar;

	public static int secondStar;

	public static int thirdStar;

	public static int currentMinStarPoint = 0;

	public static bool startBunner = false;

	public static BuyManager buyManager = new BuyManager();
    /// <summary>
    /// Общее число уровней
    /// </summary>
	public static int allLevels = 100;
    /// <summary>
    /// Число локаций
    /// </summary>
    public static int locationsCount = 5;

	public static int multiplyBonusTimePoints = 2;

	public static int startCountLife = 10;
    /// <summary>
    /// Price for 3 moves
    /// </summary>
	public static int buyMoves = 30;
    /// <summary>
    /// Price for 1 life
    /// </summary>
	public static int buyLife = 40;
    /// <summary>
    /// Price to unlock game
    /// </summary>
    public static int buyGateBase = 70;
    public static int buyGateUp = 30;


    public static int _countCoins;
    public static int countCoins
    {
        get { return MySoomlaStore.GOLD_COINS.GetBalance(); }
    }
    /// <summary>
    /// Время для пополнения 1 жизни в секундах
    /// </summary>
	public static long updateLifeTime = 1200;

	public static int powerUpsAttack = 3;

	#region InAppGold
	public static int priceBomb = 55;
	public static int priceArrow = 130;
	public static int pricePrism = 210;
	#endregion

    /// <summary>
    /// Счётчик поражений для показа фулскринов
    /// </summary>
	public static int fullScreenCounterLose = 1;

    /// <summary>
    /// Счётчик побед для показа фулскринов
    /// </summary>
	public static int fullScreenCounterWin = 0;


	public static MapController2 mapController2;
	/// <summary>
	/// Reset this instance.
	/// </summary>
	public static void Reset()
	{
		parser = new Parser();
		GamePlay.isInput = true;
		score = 0;
		windowInterface = 0;
		GamePlay.selectedObjects.Clear ();
		colorChanses = new List<SChanceColor> ();
		taskLevel = new List<TaskLevel> ();
		diamondManager.ResetData ();
	}
}
