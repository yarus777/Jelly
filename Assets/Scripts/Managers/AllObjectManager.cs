using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AllObjectManager : CacheTransform {
	private Vector3 toPos;
	private float speedMove;
	private List<Properties> objects;
	public bool moving = false;
	public MoveOffset offset;
	public int countOffset;

	public AllObjectManager()
	{
		GamePlay.allObjectManager = this;
	}

	public void StartMove(MoveOffset offset, float countOffset)
	{
		this.offset = offset;
		objects = GameData.manager.GetAllObects (true);
		GamePlay.SetInput (false);

		moving = true;
		toPos = transform.position;
		switch(offset)
		{
			case MoveOffset.Up:
				speedMove = GameData.speedMoveAllUp;
				toPos.y+=countOffset;
				break;
			case MoveOffset.Down: 
				speedMove = GameData.speedMoveAllDown;
				toPos.y-=countOffset;
				break;
		}
		Move();
	}

	public void Move()
	{
		if(needMove())
		{
			transform.position = Vector3.MoveTowards(transform.position, toPos, speedMove);
			Invoke("Move", GamePlay.timePhysics);
		}
		else
		{
			//GameData.manager.DestroyObjects(countOffset);
			transform.position = toPos;
			ActiveObjects();
			//GamePlay.SetInput(true);
			moving = false;
		}
		VisibleObjects ();
	}

	private bool needMove()
	{
		switch(offset)
		{
			case MoveOffset.Up:
				return transform.position.y < toPos.y;
			case MoveOffset.Down:
				return transform.position.y > toPos.y;
		}
		return false;
	}

	public void VisibleObjects()
	{
		foreach(Properties property in objects)
		{
			property.Visible();
		}

		GamePlay.backManager.Visible ();
	}

	public void ActiveObjects()
	{
		foreach(Properties property in objects)
		{
			property.ActiveObject(property.InField());
		}
	}

	public bool IsMoving()
	{
		return moving;
	}

	public bool NeedOffset()
	{

//		float offset;
//		int countStrings;
//		//work with black Hero
//		int[] position = GameData.manager.PositionObstacle(ObjectTypes.BlackJelly);
//		if(position != null)
//		{
//			countOffset  = (GameData.sizeY - GameData.sizeYVisible) - position[1];
//
//			//есть ли под этим элементом строка с еще таким же элементом
//			countStrings = GameData.manager.IsObjectOfString(ObjectTypes.BlackJelly, position[1]);
//			if(countStrings > 0)
//			{
//				// -1 строка, что бы сместиться на 2 вниз
//				countOffset++;
//			}
//			if( countOffset > 0)
//			{
//				offset = countOffset*GameData.distanceBetwObject;
//				StartMove(MoveOffset.Up, offset);
//				return true;
//			}
//			else 
//			{
//				return false;
//			}
//		}

		countOffset = 0;
		int countStringHero = 0;
		List<int> counts = new List<int> ();
		//black hero
		if(isBlackHero()&&!isCrystall())
		{
			int[] posLast = GameData.manager.PositionLastObjectOfType (ObjectTypes.BlackJelly);
			int offsetJ = posLast[1]-(GameData.sizeY-GameData.sizeYVisible);
			//если он на поле и не в первой строчке видимого поля
			if(offsetJ>0)
			{
				return false;
			}
			else
			{
				//если под последним есть blackhero можно сместиться на 2 строки
				if(posLast[1]-1>=0)
				{
					if(GameData.manager.IsObjectOfString(posLast[1]-1,ObjectTypes.BlackJelly))
					{
						countStringHero = 1;
					}
				}
				//иначе смещение на 1 строку
				countOffset = GameData.sizeY-GameData.sizeYVisible-posLast[1]+countStringHero;
				// если смещения нет
				if(countOffset==0)
				{
					return false;
				}
			}
			counts.Add(countOffset);
		}
		//crystall
		else if(isCrystall())
		{
//			int[] posLast = GameData.manager.PositionLastObjectOfType (ObjectTypes.Diamond);
//			int offsetJ = posLast[1]-(GameData.sizeY-GameData.sizeYVisible);
//			//если он на поле и не в первой строчке видимого поля
//			if(offsetJ>0)
//			{
//				return false;
//			}
//			else
//			{
//				//если под последним есть blackhero можно сместиться на 2 строки
//				if(posLast[1]-1>=0)
//				{
//					if(GameData.manager.IsObjectOfString(posLast[1]-1,ObjectTypes.Diamond))
//					{
//						countStringHero = 1;
//					}
//				}
//				//иначе смещение на 1 строку
//				countOffset = GameData.sizeY-GameData.sizeYVisible-posLast[1]+countStringHero;
//				// если смещения нет
//				if(countOffset==0)
//				{
//					return false;
//				}
//			}
//			counts.Add(countOffset);
		}
		//если не осталось black hero - смещаемся до конца поля
		else
		{
			countOffset = GameData.sizeY-GameData.sizeYVisible; 
			// если размер поля равен видимому размеру  - не смещаться
			if(countOffset<1)
			{
				return false;
			}
			counts.Add(countOffset);
		}

		//выбор минимального смещения
		countOffset = counts [0];
		for(int i=1; i<counts.Count; i++)
		{
			countOffset = Mathf.Min(countOffset, counts[i]);
		}

		//если набранное смещение превышает размер возможного смещение, смещаться на остаток(заглушка для кристалла)
		if(countOffset>GameData.sizeY-GameData.sizeYVisible)
		{
			countOffset = GameData.sizeY-GameData.sizeYVisible; 
		}

		StartMove(MoveOffset.Up, countOffset*GameData.distanceBetwObject);
		return true;
	}

	/// <summary>
	/// Есть ли BlackHero на поле
	/// </summary>
	/// <returns><c>true</c>, if black hero was ised, <c>false</c> otherwise.</returns>
	public bool isBlackHero()
	{
		int[] posLast = GameData.manager.PositionLastObjectOfType (ObjectTypes.BlackJelly);
		return posLast!=null;
	}

	public bool isCrystall()
	{
		return false;
	}
}
