using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;
/// <summary>
/// Parser.
/// </summary>
public class Parser {
	#region Variables
	public TaskLevel typeLevel;
	public Task levelType;
	public int levelGoal;

	/// <summary>
	/// The minimum number diamonds in scene.
	/// </summary>
	public int minDiamondInScene;
	/// <summary>
	/// The maximum number diamonds in scene.
	/// </summary>
	public int maxDiamondInScene;
	/// <summary>
	/// The chance of a diamond;
	/// </summary>
	public int chanceDiamond;

	public Limit limitType;
	public int countLimit;

	//public int fStar;
	//public int sStar;
	//public int tStar;

	public List<int> pointsToStar = new List<int> ();

	public int colls;
	public int rows;
	public int vRows;
	public string pathToLevelXML = "Levels/lvl_";
	
	public List<SParseObject> parseObjects = new List<SParseObject>();
	public List<SChanceColor> chanceObjects = new List<SChanceColor> ();

	public bool isBonusTime = true;
	public bool isBombs = true;
	public bool isArrows = true;
	public bool isPrisms = true;


	#endregion

	public void ParseLevel(int number){
		//получаем хмл-файл
		TextAsset file = Resources.Load<TextAsset> (pathToLevelXML + number);
		//переводим его в строку
		string textFile = file.text;
		//через ридер работаем со строкой
		XmlReader xmlReader = XmlReader.Create(new StringReader (textFile));

		#region parse TaskLevel 
		xmlReader.ReadToFollowing("TaskLevel");
		xmlReader.MoveToFirstAttribute();

		for (int i=0; i<xmlReader.AttributeCount; i++){
			switch(xmlReader.Name){
			case "type":
				levelType = GetTask(XmlConvert.ToInt32(xmlReader.Value));
				break;
			case "goal":
				levelGoal = XmlConvert.ToInt32(xmlReader.Value);
				break;
			}
			xmlReader.MoveToNextAttribute();
		}
		#endregion

		#region DiamondSettings
		xmlReader.ReadToFollowing("DiamondSettings");
		xmlReader.MoveToFirstAttribute();
		
		for (int i = 0; i < xmlReader.AttributeCount; i++)
		{
			switch (xmlReader.Name)
			{
			case "min":
				minDiamondInScene = XmlConvert.ToInt32(xmlReader.Value);
				break;
			case "max":
				maxDiamondInScene = XmlConvert.ToInt32(xmlReader.Value);
				break;
			case "chance":
				chanceDiamond = XmlConvert.ToInt32(xmlReader.Value);
				break;
			}
			xmlReader.MoveToNextAttribute();
		}
		#endregion

		#region parse LimitLevel
		xmlReader.ReadToFollowing("LimitLevel");
		xmlReader.MoveToFirstAttribute();
		
		for (int i=0; i<xmlReader.AttributeCount; i++){
			switch(xmlReader.Name){
			case "type":
				limitType = GetTypeLimit(XmlConvert.ToInt32(xmlReader.Value));
				break;
			case "count":
				countLimit= XmlConvert.ToInt32(xmlReader.Value);
				break;
			}
			xmlReader.MoveToNextAttribute();
		}

		#endregion

		#region parse Stars
		xmlReader.ReadToFollowing("Stars");
		xmlReader.MoveToFirstAttribute();
		
		for (int i=0; i<xmlReader.AttributeCount; i++){
			switch(xmlReader.Name){
			case "first":
				pointsToStar.Add (XmlConvert.ToInt32(xmlReader.Value));
				//fStar = XmlConvert.ToInt32(xmlReader.Value);
				break;
			case "second":
				pointsToStar.Add (XmlConvert.ToInt32(xmlReader.Value));
				//sStar = XmlConvert.ToInt32(xmlReader.Value);
				break;
			case "third":
				pointsToStar.Add (XmlConvert.ToInt32(xmlReader.Value));
				//tStar = XmlConvert.ToInt32(xmlReader.Value);
				break;
			}
			xmlReader.MoveToNextAttribute();
		}
		
		#endregion

		#region parse PowerUps 
		xmlReader.ReadToFollowing("PowerUps");
		xmlReader.MoveToFirstAttribute();


		for (int i=0; i<xmlReader.AttributeCount; i++){
			int valuePUs = 1;
			switch(xmlReader.Name){
			case "bombs":
				valuePUs = XmlConvert.ToInt32(xmlReader.Value);
				if(valuePUs == 0)
				{
					isBombs = false;
				}
				break;
			case "arrows":
				valuePUs = XmlConvert.ToInt32(xmlReader.Value);
				if(valuePUs == 0)
				{
					isArrows = false;
				}
				break;
			case "prisms":
				valuePUs = XmlConvert.ToInt32(xmlReader.Value);
				if(valuePUs == 0)
				{
					isPrisms = false;
				}
				break;
			}
			xmlReader.MoveToNextAttribute();
		}
		
		#endregion

		#region parse BonusTime
		xmlReader.ReadToFollowing("BonusTime");
		xmlReader.MoveToFirstAttribute();
		
		
		for (int i=0; i<xmlReader.AttributeCount; i++){
			int valuePUs = 1;
			switch(xmlReader.Name){
			case "bonusTime":
				valuePUs = XmlConvert.ToInt32(xmlReader.Value);
				if(valuePUs == 0)
				{
					isBonusTime = false;
				}
				break;
			}
			xmlReader.MoveToNextAttribute();
		}
		
		#endregion

		#region ChansesColors
		//привязка к количеству цветов = 5 цветов
		for (int i=0; i<5; i++){
			SChanceColor chanceObject = new SChanceColor();
			xmlReader.ReadToFollowing("ChanceColor");
			xmlReader.MoveToFirstAttribute();
			
			for (int j=0; j<=xmlReader.AttributeCount; j++){
				switch (xmlReader.Name)
				{
				case "color":
					chanceObject.color = GetColor(XmlConvert.ToInt32(xmlReader.Value));
					break;
				case "chance":
					chanceObject.chance = XmlConvert.ToInt32(xmlReader.Value);
					break;
				}
				xmlReader.MoveToNextAttribute();
			}
			chanceObjects.Add(chanceObject);
		}
		#endregion

		#region parse Field
		xmlReader.ReadToFollowing("Field");
		xmlReader.MoveToFirstAttribute();	
		for (int i=0; i<xmlReader.AttributeCount; i++){
			switch(xmlReader.Name){
				case "colls":
					colls = XmlConvert.ToInt32(xmlReader.Value);
					break;
				case "rows":
					rows = XmlConvert.ToInt32(xmlReader.Value);
					break;
				case "vRows":
					vRows = XmlConvert.ToInt32(xmlReader.Value);
					break;
			}
			xmlReader.MoveToNextAttribute();
		}
		#endregion
		#region parse Cells
		for (int i=0; i<colls*rows; i++){
			SParseObject parseObject = new SParseObject();
			parseObject.background = ObjectTypes.Empty;
			parseObject.gObject = ObjectTypes.Empty;
			parseObject.needCreated = false;

			xmlReader.ReadToFollowing("Cell");
			xmlReader.MoveToFirstAttribute();

			for (int j=0; j<=xmlReader.AttributeCount; j++){
				switch (xmlReader.Name)
				{
					case "bg":
						parseObject.background = GetTypeObject(xmlReader.Value);
						parseObject.needCreated = true;
						break;
					case "type":
						parseObject.gObject = GetTypeObject(xmlReader.Value);
						parseObject.needCreated = true;
						break;
					case "color":
						parseObject.color = GetColor(XmlConvert.ToInt32(xmlReader.Value));
						break;
					case "hp":
						parseObject.hpObject = XmlConvert.ToInt32(xmlReader.Value);
						break;
					case "hpBg":
						//	parseObject.jamHp = XmlConvert.ToInt32(xmlReader.Value);
						parseObject.hpBackground = 1;
						break;
				}
				xmlReader.MoveToNextAttribute();
			}
			parseObjects.Add(parseObject);
		}
		#endregion
	}

	/// <summary>
	/// Gets the color.
	/// </summary>
	/// <returns>The color.</returns>
	/// <param name="color">Color.</param>
	private Colors GetColor(int color)
	{
		switch (color) 
		{
			case 0:
				return Colors.Fiolet;
			case 1:
				return Colors.Yellow;
			case 2:
				return Colors.Blue;
			case 3:
				return Colors.Green;
			case 4:
				return Colors.Red;
			default:
				return Colors.Empty;
		}
	}

	/// <summary>
	/// Gets the type object.
	/// </summary>
	/// <returns>The type object.</returns>
	/// <param name="value">Value.</param>
	private ObjectTypes GetTypeObject(string value)
	{
		switch(value)
		{
			case "jam":
				return ObjectTypes.Jam;
			case "jelly":
				return ObjectTypes.Jelly;
			case "diamond":
				return ObjectTypes.Diamond;
			case "plate":
				return ObjectTypes.Plate;
			case "dig":
				return ObjectTypes.BlackJelly;
			case "electro":
				return ObjectTypes.Electro;
			case "brick":
				return ObjectTypes.EmptyBlock;
			case "empty":
				return ObjectTypes.EmptyCell;
			case "stone":
				return ObjectTypes.StoneJelly;

			case "puddle":
				return ObjectTypes.Puddle;

			case "ice":
				return ObjectTypes.Ice; 
			case "snow":
				return ObjectTypes.Snow;
			case "bomb":
				return ObjectTypes.Bomb;
			case "jar":
				return ObjectTypes.Feed2;
			case "slime":
				return ObjectTypes.Slime;
			case "prism":
				return ObjectTypes.Prism;
			case "emptyPlace":
				return ObjectTypes.EmptyPlace;
			default:
				return ObjectTypes.Empty;
		}
	}

	/// <summary>
	/// Gets the type limit.
	/// </summary>
	/// <returns>The type limit.</returns>
	/// <param name="value">Value.</param>
	private Limit GetTypeLimit(int value)
	{
		return (Limit)value;
//		switch(value)
//		{
//			case 1: 
//				return Limit.NotLimit;
//			case 2:
//				return Limit.Time;
//			case 3:
//				return Limit.Moves;
//			default:
//				return Limit.Empty;
//		}
	}

	/// <summary>
	/// Gets the task.
	/// </summary>
	/// <returns>The task.</returns>
	/// <param name="value">Value.</param>
	private Task GetTask(int value)
	{
		return (Task)value;
//		switch(value)
//		{
//		case 1:
//			return Task.Points;
////		case 2:
////			return Task.Puddle;
//		case 3:
//			return Task.ClearJam;
//		case 4:
//			return Task.Diamond;
////		case 5:
////			return Task.ClearBHero;
//		default:
//			return Task.Empty;
//		}
	}
}
