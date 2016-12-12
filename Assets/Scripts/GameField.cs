//#define ONE_COLOR

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// Game field.
/// </summary>
public static class GameField {
	#region Variables
	#if ONE_COLOR
	/// <summary>
	/// Для настройки одного цвета
	/// </summary>
	public static Colors oneColor = Colors.Red;
	#endif
	/// <summary>
	/// Хранит количество объектов созданных в позиции х.
	/// </summary>
	public static int[] countAutoCreate;
	/// <summary>
	/// Позиция автосоздания объекта.
	/// </summary>
	private static Vector3 positionAutoCreate;
	/// <summary>
	/// Начальная позиция создания объектов.
	/// </summary>
	public static Vector3 startPos;
	/// <summary>
	/// Массив спрайтов для изменения картинки на джеме.
	/// </summary>
	public static Sprite[] jamSprites;
	/// <summary>
	/// Родительский объект, в котором будут создаваться другие объекты на сцене.
	/// </summary>
	public static ParentObject parentObject;
	#endregion

	/// <summary>
	/// Заполняет игровое поле рандомными объектами.
	/// </summary>
	/// <param name="type">Тип создаваемого объекта.</param>
	/// <param name="sizeX">Size x.</param>
	/// <param name="sizeY">Size y.</param>
	public static void CreateField(ObjectTypes type, int sizeX, int sizeY)
	{
		Vector3 position;
		Colors color;
		//startPos = new Vector3 ((GameData.maxX-sizeX)/2f*GameData.distanceBetwObject, (GameData.maxY - sizeY)/2f*GameData.distanceBetwObject, 0);

		for(int j = 0; j<sizeY; j++)
		{
			for(int i = 0; i<sizeX; i++)
			{
				//Если в данной позиции есть объект, то не заполняет рандомным объектом
				SParseObject objParce = GameData.parser.parseObjects[j*GameData.sizeX + i];
				if(GameData.manager.IsObjectIJMainObj(i, j)||objParce.gObject == ObjectTypes.EmptyPlace)
				{
					continue;
				}
				//задаем позицию создания объекта
				position = startPos;
				//смещаем позицию создания объекта
				position += new Vector3(i*GameData.distanceBetwObject, j*GameData.distanceBetwObject, 0);
				#if ONE_COLOR
				color = oneColor;
				#else
				color = ReturnRandomColor();
				#endif


				GameData.manager.AddObject (i, j, CreateGameObject(position, GameData.pool.GetObject(type, color), color, type,0));
			}
		}
	}

	/// <summary>
	/// Creates the game object.
	/// </summary>
	/// <returns>The game object.</returns>
	/// <param name="position">Position.</param>
	/// <param name="gObject">Game object.</param>
	/// <param name="color">Color.</param>
	/// <param name="type">Тип объекта.</param>
	public static Properties CreateGameObject(Vector3 position, GameObject gObject, Colors color, ObjectTypes type, int hpValue)
	{
//		if (type == ObjectTypes.EmptyPlace) 
//		{
//			return null;		
//		}
		Properties property;
		GameObject obj;
		obj = MonoBehaviour.Instantiate(gObject) as GameObject;
		property = obj.GetComponent<Properties>();
		property.transform.parent = parentObject.transform;
		obj.transform.localPosition = position;
		property.toPos = position;
		property.SetTypeObject(type);
		property.Visible ();
		switch(type)
		{
			case ObjectTypes.Jelly:
				property.iColor.SetColor(color);
				break;
			case ObjectTypes.Electro:
				property.iColor.SetColor(color);
				if(!GamePlay.oneShotPU)
				{
					GamePlay.soundManager.CreateSoundType(SoundsManager.SoundType.PowerUp);
					GamePlay.oneShotPU = true;
				}
				break;
			case ObjectTypes.Diamond:
				GameData.diamondManager.AddCurrentCountDiamond();
				break;
			case ObjectTypes.BlackJelly:
				property.iColor.SetColor(color);
				property.iBlackHero.SetHp(hpValue);
				break;
			case ObjectTypes.Puddle:
				property.iColor.SetColor(color);
				property.iPuddle.SetCurHp(hpValue);
				property.iPuddle.SetMaxHp(hpValue);
				break;	
			case ObjectTypes.Ice:
				property.iColor.SetColor(color);
				break;
			case ObjectTypes.Snow:
				property.iColor.SetColor(color);
				break;
			case ObjectTypes.Bomb:
				property.iColor.SetColor(color);
				if(!GamePlay.oneShotPU)
				{
					GamePlay.soundManager.CreateSoundType(SoundsManager.SoundType.PowerUp);
					GamePlay.oneShotPU = true;
				}
				break;
			case ObjectTypes.Feed2:
				property.iColor.SetColor(color);
				property.iFeed2.SetCurHp(hpValue);
				property.iFeed2.SetMaxHp(hpValue);
				break;
			case ObjectTypes.Slime:
				if(!GamePlay.oneShotSlimeOpen)
				{
					GamePlay.soundManager.CreateSoundType(SoundsManager.SoundType.SlimeOpen);
					GamePlay.oneShotSlimeOpen = true;
				}
				break;
			case ObjectTypes.Prism:
				property.iColor.SetColor(color);
				break;
			case ObjectTypes.Empty:
				break;
		}
		return property;
	}

	/// <summary>
	/// Заполняет игровое поле распарсенными объектами.
	/// </summary>
	public static void EditorLevel()
	{
		SParseObject obj;
		for(int j = 0; j<GameData.sizeY; j++)
		{
			for(int i = 0; i<GameData.sizeX; i++)
			{
				obj = GameData.parser.parseObjects[j*GameData.sizeX + i];
				if(obj.needCreated)
				{
					if(obj.background != ObjectTypes.Empty)
					{
						CreateGOFromEditor(obj.background, i, j, 1, Colors.Empty);
					}
					if(obj.gObject != ObjectTypes.Empty)
					{
						switch(obj.gObject)
						{
							case ObjectTypes.Jelly:
								CreateGOFromEditor(ObjectTypes.Jelly, i, j, 0, obj.color);
								break;
							case ObjectTypes.Diamond:
								CreateGOFromEditor(ObjectTypes.Diamond, i, j, 0, Colors.Empty);
								break;
							case ObjectTypes.Plate:
								CreateGOFromEditor(ObjectTypes.Plate, i, j, 0, Colors.Empty);
								break;
							case ObjectTypes.BlackJelly:
								CreateGOFromEditor(ObjectTypes.BlackJelly, i, j, obj.hpObject, Colors.Black);
								break;
							case ObjectTypes.Electro:
								CreateGOFromEditor(ObjectTypes.Electro, i, j, 0, obj.color);
								break;
							case ObjectTypes.EmptyBlock:
								CreateGOFromEditor(ObjectTypes.EmptyBlock, i, j, 0, Colors.Empty);
								break;
							case ObjectTypes.EmptyCell:
								CreateGOFromEditor(ObjectTypes.EmptyCell, i, j, 0, Colors.Empty);
								break;
							case ObjectTypes.StoneJelly:
								CreateGOFromEditor(ObjectTypes.StoneJelly, i, j, 0, Colors.Empty);
								break;
							case ObjectTypes.Puddle:
								CreateGOFromEditor(ObjectTypes.Puddle, i, j, obj.hpObject, obj.color);
								break;
							case ObjectTypes.Ice:
								CreateGOFromEditor(ObjectTypes.Ice, i, j, 0, obj.color);
								break;
							case ObjectTypes.Snow:
								CreateGOFromEditor(ObjectTypes.Snow, i, j, 0, obj.color);
								break;
							case ObjectTypes.Bomb:
								CreateGOFromEditor(ObjectTypes.Bomb, i, j, 0, obj.color);
								break;
							case ObjectTypes.Feed2:
								CreateGOFromEditor(ObjectTypes.Feed2, i, j, obj.hpObject, obj.color);
								break;
							case ObjectTypes.Slime:
								CreateGOFromEditor(ObjectTypes.Slime, i, j, 0, Colors.Empty);
								break;
							case ObjectTypes.Prism:
								CreateGOFromEditor(ObjectTypes.Prism, i, j, 0, Colors.Prism);
								break;
							case ObjectTypes.EmptyPlace:
								//CreateGOFromEditor(ObjectTypes.EmptyPlace, i, j, 0, Colors.Empty);
								break;
							default:
								break;
						}
					}
				}
			}
		}
		CreateField (ObjectTypes.Jelly, GameData.sizeX, GameData.sizeY);
	}

	/// <summary>
	/// Создает объект из распарсенного файла.
	/// </summary>
	/// <param name="type">Тип объекта.</param>
	/// <param name="i">The index.</param>
	/// <param name="j">J.</param>
	/// <param name="hpValue">Hp value.</param>
	/// <param name="color">Color.</param>
	/// 
	public static void CreateGOFromEditor(ObjectTypes type, int i, int j, int hpValue, Colors color)
	{

		Vector3 position;
		//startPos = new Vector3 ((GameData.maxX-GameData.sizeX)/2f*GameData.distanceBetwObject, (GameData.maxY - GameData.sizeY)/2f*GameData.distanceBetwObject, 0);
		position = startPos;
		position += new Vector3(i*GameData.distanceBetwObject, j*GameData.distanceBetwObject, 0);
		//color = Colors.Green;
		GameData.manager.AddObject (i, j, CreateGameObject(position, GameData.pool.GetObject(type, color), color, type, hpValue));
	}

	/// <summary>
	/// Autofill the field.
	/// Дополняет поле элементами после удаления
	/// </summary>
	/// <returns>The fill field.</returns>
	/// <param name="posI">Position i.</param>
	public static Properties AutoFillField(int posI)
	{
		Colors color;
		ObjectTypes type;
		positionAutoCreate = startPos;
		positionAutoCreate.x += posI * GameData.distanceBetwObject;
		positionAutoCreate.y += (GameData.sizeY + countAutoCreate[posI])*GameData.distanceBetwObject;
		countAutoCreate [posI]++;
		#if ONE_COLOR
		color = oneColor;
		#else
		color = ReturnRandomColor();
		#endif
		type = ObjectTypes.Jelly;

		//color = Colors.Green;

		return CreateGameObject(positionAutoCreate,GameData.pool.GetObject(type, color), color, type, 0);
	}

	/// <summary>
	/// Returns the random color.
	/// </summary>
	/// <returns>The random color.</returns>
	public static Colors ReturnRandomColor()
	{
//		//one color
	//	return Colors.Green;
		int random = Random.Range (0, SumChanses());
		int currentChance = 0;
		int nextChance = 0;
		for(int i=0; i < GameData.colorChanses.Count; i++)
		{
			nextChance = currentChance+GameData.colorChanses[i].chance;
			if(random >= currentChance && random < nextChance)
			{
				return GameData.colorChanses[i].color;
			}
			currentChance = nextChance;
		}
		return Colors.Empty;
	}

	/// <summary>
	/// Сумма шансов выпадения нужного цвета.
	/// </summary>
	/// <returns>The chanses.</returns>
	private static int SumChanses()
	{
		int count = 0;
		for(int i=0; i<GameData.colorChanses.Count; i++)
		{
			count += GameData.colorChanses[i].chance;
		}
		return count;
	}

	public static Properties ReplaceObject(Properties property, ObjectTypes type, Colors color){
		Properties replaceObject;
		if (property.iColor != null)
		{
			if(color == Colors.Empty)
			{
				replaceObject = CreateGameObject (property.transform.localPosition, GameData.pool.GetObject (type, property.iColor.GetColor()), property.iColor.GetColor(), type, 0);
			}
			else
			{
				replaceObject = CreateGameObject (property.transform.localPosition, GameData.pool.GetObject (type, color), color, type, 0);
			}
		}
		else
		{
			replaceObject = CreateGameObject (property.transform.localPosition, GameData.pool.GetObject (type, 0), 0, type, 0);
		}

		replaceObject.toPos = property.toPos;
		replaceObject.pointsTrajectory = property.pointsTrajectory;
		replaceObject.isOffseted = property.isOffseted;
		
		int[] posIJ = GameData.manager.ReturnIJPosObject (property);
		GameData.manager.AddObject (posIJ [0], posIJ [1], replaceObject);
		MonoBehaviour.DestroyImmediate (property.gameObject);
		
		return replaceObject;
	}
}
