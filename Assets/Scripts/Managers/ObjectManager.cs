using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
/// <summary>
/// Object manager.
/// </summary>
public class ObjectManager {
	private SCell[,] objects;
	private Color colorRGB;
	private TextMesh textMesh;

	private ObjectTypes[] objectsSlip = {ObjectTypes.Diamond};

	public ObjectManager(int sizeX, int sizeY)
	{
		objects = new SCell[sizeX,sizeY];
	}

	public void AddObject(int i, int j, Properties property)
	{
		switch(property.type)
		{
			case ObjectTypes.Jam:
				objects [i,j].backObject = property;
				break;
			default :
				objects [i,j].mainObject = property;
				break;
		}
	}

	public void DeleteObject(Properties property)
	{
		switch(property.GetTypeObject())
		{
			case ObjectTypes.Jam:
				DeleteJam(property);
				break;
			default:
				DeleteMoveObject(property);
				break;
		}
	}

	private void DeleteJam(Properties property)
	{
		for(int j=0; j < GameData.sizeY; j++)
		{
			for(int i=0; i < GameData.sizeX; i++)
			{
				if(IsObject(property.GetTypeObject(),i, j) 
				   && IsEquals(objects[i,j].mainObject, property))
				{
					objects[i,j].backObject = null;
				}
			}
		}
	}

	private void DeleteMoveObject(Properties property)
	{
		for(int j=0; j < GameData.sizeY; j++)
		{
			for(int i=0; i < GameData.sizeX; i++)
			{
				if(IsObject(property.GetTypeObject(),i, j) 
				   && IsEquals(objects[i,j].mainObject, property))

				{
					objects[i,j].mainObject = null;
				}
			}
		}
	}

	private bool IsEquals(Properties prop1, Properties prop2)
	{  
		if (prop1 != null && prop2 != null)
		{
			return	prop1.GetInstanceID () == prop2.GetInstanceID ();
		}
		else
			return false;
	}

	public bool IsObject(ObjectTypes type, int i, int j)
	{
		switch(type)
		{
			case ObjectTypes.Jelly:
				if(objects [i, j].mainObject != null)
				{
					return objects [i, j].mainObject.iJelly != null;
				}
				return false;
			case ObjectTypes.BlackJelly:
				if(objects [i, j].mainObject != null)
				{
					return objects [i, j].mainObject.iBlackHero != null;
				}
				return false;
			case ObjectTypes.Jam:
					return objects [i, j].backObject != null;

			case ObjectTypes.Diamond:
				if(objects [i, j].mainObject != null)
				{
					return objects [i, j].mainObject.iDiamond != null;
				}
				return false;
			case ObjectTypes.Electro:
				if(objects [i, j].mainObject != null)
				{
					return objects [i, j].mainObject.iElectro != null;
				}
				return false;
			case ObjectTypes.StoneJelly: 
				if(objects [i, j].mainObject != null)
				{
					return objects [i, j].mainObject.iStone != null;
				}
				return false;
			case ObjectTypes.Ice:
				if(objects [i, j].mainObject != null)
				{
					return objects [i, j].mainObject.iIce != null;
				}
				return false;
			case ObjectTypes.Snow:
				if(objects [i, j].mainObject != null)
				{
					return objects [i, j].mainObject.iSnow != null;
				}
				return false;
			case ObjectTypes.Puddle: 
				if(objects [i, j].mainObject != null)
				{
					return objects [i, j].mainObject.iPuddle != null;
				}
				return false;
			case ObjectTypes.EmptyBlock:
				if(objects [i, j].mainObject != null)
				{
					return objects [i, j].mainObject.GetTypeObject() == ObjectTypes.EmptyBlock;
				}
				return false;
			case ObjectTypes.EmptyCell:
				if(objects [i, j].mainObject != null)
				{
					return objects [i, j].mainObject.GetTypeObject() == ObjectTypes.EmptyCell;
				}
				return false;
			case ObjectTypes.Bomb:
				if(objects [i, j].mainObject != null)
				{
					return objects [i, j].mainObject.GetTypeObject() == ObjectTypes.Bomb;
				}
				return false;
			case ObjectTypes.Feed2:
				if(objects [i, j].mainObject != null)
				{
					return objects [i, j].mainObject.GetTypeObject() == ObjectTypes.Feed2;
				}
				return false;
			case ObjectTypes.Slime:
				if(objects [i, j].mainObject != null)
				{
					return objects [i, j].mainObject.GetTypeObject() == ObjectTypes.Slime;
				}
				return false;
			case ObjectTypes.Prism:
				if(objects [i, j].mainObject != null)
				{
					return objects [i, j].mainObject.GetTypeObject() == ObjectTypes.Prism;
				}
				return false;
			default:
				return false;
		}
	}
	

	public bool OffsetObject()
	{
		List <Properties> createdObjects = new List<Properties> ();
		GamePlay.SetInput (false);
		GameField.countAutoCreate = new int[GameData.sizeX];
		bool needOffset = true;
		bool isOffsetObject = false;
        Debug.LogWarning("OffsetObject");
		while(needOffset)
		{
			bool isOffset = false;
			for (int j=0; j < GameData.sizeY; j++) 
			{
				for (int i=0; i < GameData.sizeX; i++) 
				{
					if(IsObjectIJMainObj(i, j)&& !objects[i, j].mainObject.isPassable)
					{
						if(objects[i, j].mainObject.canMove)
						{
							if(MoveDown(i, j))
							{
								isOffsetObject = true;
								isOffset = true;
							}
							else{
								int random = UnityEngine.Random.Range(0,2);
								switch(random){
									case 0:
										if(MoveLeft(i, j))
										{
											isOffsetObject = true;
											isOffset = true;
										}
										else if(MoveRight(i, j))
										{
											isOffsetObject = true;
											isOffset = true;
										}
										break;
									case 1:
										if(MoveRight(i, j))
										{
											isOffsetObject = true;
											isOffset = true;
										}
										else if(MoveLeft(i, j))
										{
											isOffsetObject = true;
											isOffset = true;
										}
										break;
								}
							}

						}
					}
					else if(j == GameData.sizeY-1 )
					{
						if(!IsObjectIJMainObj(i,j))
						{
							Offsed(i, j, createdObjects);
							isOffsetObject = true;
							isOffset = true;
						}
						else if(IsObjectIJMainObj(i,j) && objects[i, j].mainObject.isPassable)
						{
							int countMove = 1;
							
							for(int k = j-1; k >= 0; k--)
							{
								if(objects[i,k].mainObject == null)
								{	
									Offsed(i, k, createdObjects);
									AddPointTrajectory(objects[i, k].mainObject, MoveOffset.Down,countMove);
									isOffsetObject = true;
									isOffset = true;
									break;
								}
								else if(objects[i,k].mainObject.isPassable)
								{
									countMove++;
								}
								else
								{
									break;
								}
							}
						}
					}
				}
			}
			if(!isOffset)
			{
				needOffset = false;
			}
		}
		ChanceForCreateDiamond (createdObjects);
		StartMoveObject ();
		ResetOneShot ();
		return isOffsetObject;
	}

	private void ResetOneShot()
	{
		GamePlay.oneShotElectro = false;
		GamePlay.oneShotBomb = false;
		GamePlay.oneShotPot = false;
		GamePlay.oneShotIce = false;
		GamePlay.oneShotJam = false;
		GamePlay.oneShotFillTheBucket = false;
		GamePlay.oneShotFilledBucket = false;
		GamePlay.oneShotFeed = false;
		GamePlay.oneShotPU = false;
		GamePlay.oneShotSlimeOpen = false;
		GamePlay.oneShotSlimeDestroy = false;
		GamePlay.oneShotDigAttack = false;
		GamePlay.oneShotDigDrop = false;
		GamePlay.oneShotPrisBoom = false;
	}

	private void Offsed(int i, int j, List <Properties> createdObjects)
	{
		objects[i, j].mainObject = GameField.AutoFillField(i);
		createdObjects.Add(objects[i, j].mainObject);
		objects[i, j].mainObject.isOffseted = true;
		for(int k = 0; k < GameField.countAutoCreate[i]; k++)
		{
			AddPointTrajectory(objects[i, j].mainObject, MoveOffset.Down,1);
		}
	}


	private void ChanceForCreateDiamond(List <Properties> createdObjects)
	{
		if(createdObjects.Count > 0)
		{
			if(GameData.diamondManager.NeedCreateDiamond())
			{
				int random = UnityEngine.Random.Range(0, createdObjects.Count);
				Properties tempObject = createdObjects[random];
				createdObjects.Remove(tempObject);
				GameField.ReplaceObject(tempObject, ObjectTypes.Diamond, Colors.Empty);
				ChanceForCreateDiamond(createdObjects);
				return;
			}
		}
	}
	#region Move object
	/// <summary>
	/// Moves down.
	/// </summary>
	/// <returns><c>true</c>, if down was moved, <c>false</c> otherwise.</returns>
	/// <param name="i">The index.</param>
	/// <param name="j">J.</param>
	private bool MoveDown(int i, int j)
	{
		if(j >0)
		{
			if(objects[i,j-1].mainObject == null)
			{
				Move (i, j, 1);
				return true; 
			}
			else if(objects[i,j-1].mainObject.isPassable)
			{
				int countMove = 2;
				bool isEmptyObject = false;

				for(int k = j-2; k >= 0; k--)
				{
					if(objects[i,k].mainObject == null)
					{	
						isEmptyObject = true;
						break;
					}
					else if(objects[i,k].mainObject.isPassable)
					{
						countMove++;
					}
					else
					{
						return false;
					}
				}

				if(isEmptyObject)
				{
					Move (i, j, countMove);
					return true;
				}
			}
		}
		return false;
	}

	private void Move(int i, int j, int countMove)
	{
		objects[i, j].mainObject.isOffseted = true;
		AddPointTrajectory(objects[i, j].mainObject, MoveOffset.Down,countMove);
		objects[i,j-countMove].mainObject = objects[i, j].mainObject;
		objects[i, j].mainObject = null;
	}

	private bool MoveLeft(int i,int j)
	{
		if(j >0 && i > 0 && objects[i-1, j-1].mainObject == null)
		{
			if (IsOffsetLeft(i, j)
			    || IsMoveSlip(objects[i,j].mainObject, objects[i,j-1].mainObject))
			{
				objects[i, j].mainObject.isOffseted = true;
				AddPointTrajectory(objects[i, j].mainObject, MoveOffset.Left,1);
				objects[i-1,j-1].mainObject = objects[i, j].mainObject;
				objects[i, j].mainObject = null;
				return true; 
			}
		}
		return false;
	}

	private bool MoveRight(int i,int j)
	{
		if(j > 0 && i < GameData.sizeX - 1 && objects[i+1, j-1].mainObject == null)
		{
			if (IsOffsetRight(i, j)
			    || IsMoveSlip(objects[i,j].mainObject, objects[i,j-1].mainObject))
			{
				objects[i, j].mainObject.isOffseted = true;
				AddPointTrajectory(objects[i, j].mainObject, MoveOffset.Right,1);
				objects[i+1,j-1].mainObject = objects[i, j].mainObject;
				objects[i, j].mainObject = null;
				return true; 
			}
		}
		return false;
	}

	private bool IsMoveSlip(Properties property, Properties propertyDown)
	{
		if(propertyDown.isBypass)
		{
			foreach(ObjectTypes type in objectsSlip)
			{
				if(property.GetTypeObject () == type)
				{
					return true;
				}
			}
		}
		return false;
	}
	

	#region IsOffsetLeft
	private bool IsOffsetLeft(int i, int j)
	{
			int countToObstacle = 0; // расстояние до obstacle  не включая obstacle
			#region CountToObstacle
			bool isObstacle = false;
			for(int k = j; k<GameData.sizeY; k++){
				if(IsObjectIJMainObj(i-1,k)){
					if(!objects[i-1,k].mainObject.isBypass)
					{
						return false;
					}
					else
					{
						isObstacle = true;
						break;
					}
				}
				else
				{
					countToObstacle++;
				}
			}
			
			if(!isObstacle){
				return false;
			}
			#endregion

			int countLeft = 0;
			bool fullLeft = false;
			#region CountLeft
			for(int k = i-2; k>=0; k--){
				if(IsObjectIJMainObj(k, j+countToObstacle)
			   		&& objects[k, j+countToObstacle].mainObject.isBypass){
					countLeft++;
					if(k==0){
						fullLeft = true;
					}
				}
				else{
					break;
				}
			}
			if(i-2<0){
				fullLeft = true;
			}
			#endregion

			int countRight = 0;
			#region CountRight
			for(int k = i; k<GameData.sizeX; k++){
				if(IsObjectIJMainObj(k, j+countToObstacle)
			 		  && objects[k, j+countToObstacle].mainObject.isBypass){
					countRight++;
				}
				else{
					break;
				}
			}
			#endregion

			if(fullLeft){
				if(countToObstacle == countRight){
					return true;
				}
			}
			else{
				int half = (countLeft+countRight+1)/2;
				if(countRight<=half && countToObstacle == countRight){
					return true;
				}
			}

		return false;
	}
	#endregion

	private bool IsOffsetRight(int i, int j)
	{
		int countToObstacle = 0; // расстояние до obstacle  не включая obstacle
		#region CountToObstacle
		bool isObstacle = false;
		for(int k = j; k<GameData.sizeY; k++){
			if(IsObjectIJMainObj(i+1,k)){
				if(!objects[i+1,k].mainObject.isBypass){
					return false;
				}
				else{
					isObstacle = true;
					break;
				}
			}
			else{
				countToObstacle++;
			}
		}
		
		if(!isObstacle){
			return false;
		}
		

		#endregion
		
		int countRight = 0;
		bool fullRight = false;
		#region CountRight
		for(int k = i+2; k<GameData.sizeX; k++){
			if(IsObjectIJMainObj(k, j+countToObstacle)
			   && objects[k, j+countToObstacle].mainObject.isBypass){
				countRight++;
				if(k==GameData.sizeX-1){
					fullRight = true;
				}
			}
			else{
				break;
			}
		}
		if(i+2>GameData.sizeX-1){
			fullRight = true;
		}
		#endregion
		
		int countLeft = 0;
		#region CountLeft
		for(int k = i; k>=0; k--){
			if(IsObjectIJMainObj(k, j+countToObstacle)
			   && objects[k, j+countToObstacle].mainObject.isBypass){
				countLeft++;
			}
			else{
				break;
			}
		}
		#endregion
		
		if(fullRight){
			if(countToObstacle == countLeft){
				return true;
			}
		}
		else{
			int half = (countLeft+countRight+1)/2;
			if(countLeft<=half && countToObstacle == countLeft){
				return true;
			}
		}
		
		return false;
	}
	#endregion


	private void AddPointTrajectory(Properties property, MoveOffset side, int countMove)
	{
		switch(side)
		{
			case MoveOffset.Down:
				property.toPos.y -= GameData.distanceBetwObject*countMove;
				break;
			case MoveOffset.Right:
				property.toPos.x += GameData.distanceBetwObject*countMove;
				property.toPos.y -= GameData.distanceBetwObject*countMove;
				break;
			case MoveOffset.Left:
				property.toPos.x -= GameData.distanceBetwObject*countMove;
				property.toPos.y -= GameData.distanceBetwObject*countMove;
				break;
		}

		property.pointsTrajectory.Add (property.toPos);
	}

	private void StartMoveObject()
	{
		for (int j=0; j < GameData.sizeY; j++)  
		{
			for (int i=0; i < GameData.sizeX; i++) 
			{
				if(IsObjectIJMainObj(i, j) && objects[i, j].mainObject.isOffseted)
				{
					objects[i, j].mainObject.speedMove = GameData.speedMoveObject*objects[i,j].mainObject.pointsTrajectory.Count;
				//	objects[i, j].gObject.acceleration = GameData.acceleration*objects[i,j].gObject.pointsTrajectory.Count;
					objects[i, j].mainObject.isMoving = true;
					objects[i, j].mainObject.Move();
				}
			}
		}
	}

	public bool IsMoveObject()
	{
		for (int j=0; j < GameData.sizeY; j++) 
		{
			for (int i=0; i < GameData.sizeX; i++) 
			{
				if(IsObjectIJMainObj(i, j) && objects[i, j].mainObject.isMoving)
				{
					return true;
				}
			}
		}
		return false;
	}
	/// <summary>
	/// получить джем по желе
	/// </summary>
	/// <returns>The jam.</returns>
	/// <param name="property">Property.</param>
	public Properties GetJam(Properties property)
	{
		for (int j=0; j < GameData.sizeY; j++) 
		{
			for (int i=0; i < GameData.sizeX; i++) 
			{
				if(IsObject(property.GetTypeObject(), i, j) 
				   && IsEquals(objects[i,j].mainObject, property))
				{
					return objects[i,j].backObject;
				}
			}
		}
		return null;
	}

	public bool IsObjectIJMainObj(int i, int j)
	{
		return objects [i, j].mainObject != null;
	}

	public bool IsObjectIJBackObj(int i, int j)
	{
		return objects[i, j].backObject != null;
	}

	public void ChangeImageJam(Properties property)
	{
		for (int j=0; j < GameData.sizeY; j++) 
		{
			for (int i=0; i < GameData.sizeX; i++) 
			{
				if(IsObject(property.GetTypeObject(), i, j) 
				   && !IsEquals(objects[i,j].mainObject, property))
				{
					CreateNumber(objects[i,j].backObject, i, j);
				}
			}
		}
	}

	private void CreateNumber(Properties jam, int i, int j)
	{
		string numberString = "";
		if(i-1 >=0 && IsObject(ObjectTypes.Jam, i-1, j) )
		{
			numberString += "1";
		}
		else
		{
			numberString += "0";
		}

		if(j+1 < GameData.sizeY && IsObject(ObjectTypes.Jam, i, j+1) )
		{
			numberString += "1";
		}
		else
		{
			numberString += "0";
		}

		if(i+1 < GameData.sizeX && IsObject(ObjectTypes.Jam, i+1, j))
		{
			numberString += "1";
		}
		else
		{
			numberString += "0";
		}

		if(j-1 >=0 && IsObject(ObjectTypes.Jam, i, j-1))
		{
			numberString += "1";
		}
		else
		{
			numberString += "0";
		}

		int number = Convert.ToInt32(numberString, 2);

		Jams typeImg = (Jams)number;
		//Debug.Log (typeImg);
		jam.iJam.SetTypeImage(typeImg);
	}

	public List<Properties> GetAllObects(bool needJam)
	{
		List<Properties> properties = new List<Properties>();
		for (int j=0; j < GameData.sizeY; j++) 
		{
			for (int i=0; i < GameData.sizeX; i++) 
			{
				if(needJam)
				{
					if(IsObject(ObjectTypes.Jam, i, j))
					{
						properties.Add(objects[i, j].backObject);
					}
				}
				if(IsObjectIJMainObj(i, j))
				{
					properties.Add(objects[i, j].mainObject);
				}
			}
		}
		return properties;
	}

	public List<Properties> GetAllObjectsOfType(ObjectTypes type)
	{
		List<Properties> properties = new List<Properties>();
		for (int j=0; j < GameData.sizeY; j++) 
		{
			for (int i=0; i < GameData.sizeX; i++) 
			{
				switch(type)
				{
					case ObjectTypes.Jam:
						if(IsObject(ObjectTypes.Jam, i, j))
						{
							properties.Add(objects[i, j].backObject);
						}
						break;

					default:
						if(IsObject(type, i, j))
						{
							properties.Add(objects[i, j].mainObject);
						}
						break;
				}
			}
		}
		return properties;
	}

	public List<Properties> GetAllObjectsOfTypeWithNull(ObjectTypes type)
	{
		List<Properties> properties = new List<Properties>();
		for (int j=0; j < GameData.sizeY; j++) 
		{
			for (int i=0; i < GameData.sizeX; i++) 
			{
				switch(type)
				{
				case ObjectTypes.Jam:
//					if(IsObject(ObjectTypes.Jam, i, j))
//					{
						properties.Add(objects[i, j].backObject);
//					}
					break;
					
				default:
					if(IsObject(type, i, j))
					{
						properties.Add(objects[i, j].mainObject);
					}
					else
					{
						properties.Add(null);
					}
					break;
				}
			}
		}
		return properties;
	}

	public List<Properties> GetAllObjectsOfTypeInField(ObjectTypes type)
	{
		List<Properties> properties = new List<Properties>();
		for (int j=0; j < GameData.sizeY; j++) 
		{
			for (int i=0; i < GameData.sizeX; i++) 
			{
				switch(type)
				{
					case ObjectTypes.Jam:
						if(IsObject(ObjectTypes.Jam, i, j))
						{
							if(objects[i,j].backObject.InField())
							{
								properties.Add(objects[i, j].backObject);
							}
						}
						break;
						
					default:
						if(IsObject(type, i, j))
						{
							if(objects[i,j].mainObject.InField())
							{
								properties.Add(objects[i, j].mainObject);
							}
						}
						break;
				}
			}
		}
		return properties;
	}

	public int[] ReturnIJPosObject(Properties property){
		for(int j=0; j < GameData.sizeY; j++)
		{
			for(int i=0; i < GameData.sizeX; i++)
			{
				if(IsObject(property.GetTypeObject(), i, j) && 
				IsEquals(objects[i,j].mainObject, property))
				{
					int[] posIJ = {i,j};
					return posIJ;
				}
			}
		}
		return null;
	}

	public List<Properties> ArroundObject(ObjectTypes type, Properties property)
	{
		int[] pos = ReturnIJPosObject (property);
		int i = pos [0];
		int j = pos [1];

		List<Properties> findObjects = new List<Properties> ();

		List<SIJPos> positions = new List<SIJPos>();
		SIJPos posIJ  = new SIJPos();
		posIJ.i = i;
		posIJ.j = j+1;
		positions.Add (posIJ);

		posIJ  = new SIJPos();
		posIJ.i = i+1;
		posIJ.j = j;
		positions.Add (posIJ);

		posIJ  = new SIJPos();
		posIJ.i = i;
		posIJ.j = j-1;
		positions.Add (posIJ);

		posIJ  = new SIJPos();
		posIJ.i = i-1;
		posIJ.j = j;
		positions.Add (posIJ);

		for(int k=0; k<positions.Count; k++)
		{
			if(ValidateIPosition(positions[k].i) && ValidateJPosition(positions[k].j))
			{
				if(IsObjectIJMainObj(positions[k].i, positions[k].j))
				{
					if(objects[positions[k].i, positions[k].j].mainObject.GetTypeObject() == type)
					{
						if(objects[positions[k].i, positions[k].j].mainObject.InField())
						{
							findObjects.Add(objects[positions[k].i, positions[k].j].mainObject);
						}
					}

				}
			}
		}
		return findObjects;
	}

	public bool ValidateIPosition(int i)
	{
		return (i >= 0 && i < GameData.sizeX);
	}

	public bool ValidateJPosition(int j)
	{
		return (j >= 0 && j < GameData.sizeY);
	}

	public List<Properties> ReturnObjectAroundSquare(int[] posIJ, int distance)
	{
		List<Properties> property = new List<Properties> ();
		int i = posIJ [0] + distance;
		int j = posIJ[1] + distance;

		for(int z=0; z<3; z++)
		{
			for(int k=0; k<3; k++)
			{
				if(ValidateIPosition(i)
				   &&ValidateJPosition(j)
				   && IsObjectIJMainObj(i, j) 
				   && !objects[i, j].mainObject.isDelete
				   )
				{
					property.Add(objects[i, j].mainObject);
				}
				i-= distance;
			}
			j-= distance;
			i = posIJ [0] + distance;
		}
		return property;
	}

	public List<Properties> ReturnObjectOfColor(Colors color)
	{
		List<Properties> property = new List<Properties> ();
		
		for(int j=0; j < GameData.sizeY; j++)
		{
			for(int i=0; i < GameData.sizeX; i++)
			{
				if(IsObjectIJMainObj(i, j) 
				   && objects[i,j].mainObject.InField()
				   && !objects[i, j].mainObject.isDelete
				   && objects[i,j].mainObject.iColor.GetColor() == color
				   )
				{
					property.Add(objects[i, j].mainObject);
				}
			}
		}
		return property;
	}
	
	public List<Properties> ReturnObjectAroundLine(int[] posIJ, int distance, RemovingDirection direction)
	{
		switch(direction)
		{
			case RemovingDirection.Horizontal:
				return ReturnHorizontal(posIJ,distance);
			case RemovingDirection.Vertical:
				return ReturnVertical(posIJ,distance);
			default:
				return null;
		}
	}

	private List<Properties> ReturnHorizontal(int[] posIJ, int distance)
	{
		List<Properties> property = new List<Properties> ();
		int i = posIJ [0] + distance;
		int j = posIJ[1];

		for(int k=0; k<2; k++)
		{
			if(ValidateIPosition(i) 
			   && IsObjectIJMainObj(i, j) 
			   && !objects[i, j].mainObject.isDelete
			   )
			{
				property.Add(objects[i, j].mainObject);
			}
			i = posIJ [0] - distance;
		}
		return property;
	}

	private List<Properties> ReturnVertical(int[] posIJ, int distance)
	{
		List<Properties> property = new List<Properties> ();
		int i = posIJ [0];
		int j = posIJ[1] + distance;
		
		for(int k=0; k<2; k++)
		{
			if(ValidateJPosition(j) 
			   && IsObjectIJMainObj(i, j) 
			   && !objects[i, j].mainObject.isDelete
			   )
			{
				property.Add(objects[i, j].mainObject);
			}
			j = posIJ [1] - distance;
		}
		return property;
	}


	#region Not Moves
	public void NotMoves()
	{	
		List<Properties> properties;
		List<SListArroundObjects> listArroundObjects = new List<SListArroundObjects> ();
		int randomPair;
		int[] posIJ;
		int countPasses = 0;
		for (int j=0; j < GameData.sizeY; j++) 
		{
			for (int i=0; i < GameData.sizeX; i++) 
			{
				if(IsObject(ObjectTypes.Jelly, i, j) || IsObject(ObjectTypes.Electro, i, j)|| IsObject(ObjectTypes.Puddle, i, j))
				{
					if(objects[i,j].mainObject.InField())
					{
						countPasses++;
						posIJ = ReturnIJPosObject(objects[i,j].mainObject);
						properties = AllArroundObjectsInField	(posIJ, objects[i,j].mainObject);
						if(properties != null)
						{
							listArroundObjects.Add(new SListArroundObjects(objects[i,j].mainObject.iColor.GetColor(), properties));
						}
						else
						{
						//	Debug.Log("Is moves");
						//	return;
						}
					}
				}
			}
		}

	//	Debug.Log ("Count: "+listArroundObjects.Count);
		if(listArroundObjects.Count == countPasses)
		{
			for(int i=0; i < listArroundObjects.Count; i++)
			{
				if(listArroundObjects[i].list.Count < 2)
				{
					listArroundObjects.RemoveAt(i);
					i--;
				}
			}
			
			if(listArroundObjects.Count > 0)
			{
				randomPair = UnityEngine.Random.Range(0, listArroundObjects.Count);
				ChangeColorObject(listArroundObjects[randomPair]);
			}
			else
			{
				Debug.Log("Game over");
			}
		}


	}

	private List<Properties> AllArroundObjects(int[] posIJ, Properties property)
	{
		int i = posIJ [0];
		int j = posIJ [1];

		int countSameColors = 0;

		List<Properties> propertiesArround = new List<Properties> ();

		for(int t = i-1; t <= i+1; t++)
		{
			for(int r = j-1; r <= j+1; r++)
			{
				if(t >=0 && t <GameData.sizeX && r < GameData.sizeY && r >=0)
				{
					if((IsObject(ObjectTypes.Jelly, t, r) || IsObject(ObjectTypes.Electro, t, r)|| IsObject(ObjectTypes.Puddle, t, r)) 
					   && !IsEquals(objects[t,r].mainObject, property))
					{
						if(objects[t, r].mainObject.iColor.GetColor() == property.iColor.GetColor())
						{
							countSameColors++;
						}
						propertiesArround.Add (objects[t, r].mainObject);
					}
				}
			}
		}
		if(countSameColors < 2)
		{
			return propertiesArround;
		}
		else
		{
			return null;
		}
	}

	private List<Properties> AllArroundObjectsInField(int[] posIJ, Properties property)
	{
		int i = posIJ [0];
		int j = posIJ [1];
		
		int countSameColors = 0;
		
		List<Properties> propertiesArround = new List<Properties> ();
		
		for(int t = i-1; t <= i+1; t++)
		{
			for(int r = j-1; r <= j+1; r++)
			{
				if(t >=0 && t <GameData.sizeX && r < GameData.sizeY && r >=0)
				{
					if((IsObject(ObjectTypes.Jelly, t, r) || IsObject(ObjectTypes.Electro, t, r)) 
					   && !IsEquals(objects[t,r].mainObject, property))
					{
						if(objects[t,r].mainObject.InField())
						{
							if(objects[t, r].mainObject.iColor.GetColor() == property.iColor.GetColor())
							{
								countSameColors++;
							}
							propertiesArround.Add (objects[t, r].mainObject);
						}
					}
				}
			}
		}
		if(countSameColors < 2)
		{
			return propertiesArround;
		}
		else
		{
			return null;
		}
	}

	private void ChangeColorObject(SListArroundObjects arroundObj)
	{
		//Debug.Log ("Not moves");
		int randomElement;

		for(int i=0; i<2; i++)
		{
			randomElement = UnityEngine.Random.Range (0, arroundObj.list.Count);
			GameField.ReplaceObject (arroundObj.list [randomElement], ObjectTypes.Jelly, arroundObj.color);
			arroundObj.list.RemoveAt(randomElement);
		}
	}
	#endregion
	

	#region Shadow
	public void SetShadows(StatementShadow state, Colors color)
	{
//		SpriteRenderer sRenderer;
		/*switch(state)
		{
			case StatementShadow.On:
				colorRGB = new Color(0.4f,0.4f,0.4f,1f);
				break;
			case StatementShadow.Off:
				colorRGB = new Color(1f, 1f, 1f);
				break;
		}*/

//		for(int j=0; j < GameData.sizeY; j++)
//		{
//			for(int i=0; i < GameData.sizeX; i++)
//			{
//				if(	  IsObject(ObjectTypes.Jelly, i, j) 
//				   || IsObject(ObjectTypes.Electro, i, j) 
//				   || IsObject(ObjectTypes.Puddle, i, j)
//				   || IsObject(ObjectTypes.Bomb, i, j)
//				   || IsObject(ObjectTypes.Feed2, i, j)
//				   )
//				{
//					if(objects[i,j].mainObject.iColor.GetColor() != color)
//					{
//						switch(objects[i,j].mainObject.GetTypeObject())
//						{
//							case ObjectTypes.Jelly:
//								sRenderer = objects[i,j].mainObject.GetComponentInChildren<SpriteRenderer>();
////								sRenderer.renderer.sharedMaterial.color = colorRGB;
//								sRenderer.color = colorRGB;
//								break;
//							case ObjectTypes.Electro:
//								sRenderer = objects[i,j].mainObject.GetComponent<Electro>().electroSR;
//								sRenderer.color = colorRGB;
////								sRenderer.renderer.sharedMaterial.color = colorRGB;
//								break;
//							case ObjectTypes.Bomb:
//								sRenderer = objects[i,j].mainObject.GetComponentInChildren<SpriteRenderer>();
//								sRenderer.color = colorRGB;
////								sRenderer.renderer.sharedMaterial.color = colorRGB;
//								break;
//							case ObjectTypes.Puddle:
//								sRenderer = objects[i,j].mainObject.GetComponentInChildren<SpriteRenderer>();
//								sRenderer.color = colorRGB;
//
//								textMesh = objects[i,j].mainObject.GetComponentInChildren<TextMesh>();
//								textMesh.color = colorRGB;
//								break;
//							case ObjectTypes.Feed2:
//								foreach(SpriteRenderer spriteRenderer in objects[i,j].mainObject.GetComponentsInChildren<SpriteRenderer>())
//								{
//									spriteRenderer.color = colorRGB;
//								}
//								
//								textMesh = objects[i,j].mainObject.GetComponentInChildren<TextMesh>();
//								textMesh.color = colorRGB;
//								break;
//							default:
//								sRenderer = null;
//								break;
//						}
//					}
//				}
//			}
//		}
	}
	#endregion

	public void DestroyYToSizeYField(int posJ)
	{
		for (int j=posJ; j < GameData.sizeY; j++)  
		{
			for (int i=0; i < GameData.sizeX; i++) 
			{
				if(objects[i,j].backObject!=null)
				{
					MonoBehaviour.DestroyImmediate(objects[i,j].backObject.gameObject);
				}
				if(objects[i,j].mainObject!=null)
				{
					MonoBehaviour.DestroyImmediate(objects[i,j].mainObject.gameObject);
				}
			}
		}
	}
	
	/// <summary>
	/// Возвращает позицию последнего объекта в массиве по типу
	/// </summary>
	/// <returns>The last object of type.</returns>
	/// <param name="type">Type.</param>
	public int[] PositionLastObjectOfType(ObjectTypes type)
	{
		int[] posIJ = new int[2];
		for(int j=GameData.sizeY-1; j >= 0; j--)
		{
			for(int i=GameData.sizeX-1; i >=0; i--)
			{
				if(IsObject(type, i, j))
				{
					posIJ[0] = i;
					posIJ[1] = j;
					return posIJ;
				}
			}
		}
		return null;
	}

//	public int IsObjectOfString(ObjectTypes type, int currentJ)
//	{
//		int countStrings = 0;
//		for(int j = currentJ -1 ; j>=0; j--)
//		{
//			if(countStrings != currentJ-j-1)
//			{
//				break;
//			}
//			for(int i =0; i< GameData.sizeX; i++)
//			{
//				if(IsObject(type, i, j))
//				{
//					countStrings++;
//					break;
//				}
//			}
//		}
//		return countStrings;
//	}
//
//	/// <summary>
//	/// Поиск позиции последнего объекта.
//	/// </summary>
//	/// <returns>The obstacle.</returns>
//	/// <param name="type">Type.</param>
//	public int[] PositionObstacle(ObjectTypes type)
//	{
//		for(int j=GameData.sizeY-1; j >=0; j--)
//		{
//			for(int i=0; i < GameData.sizeX; i++)
//			{
//				if(IsObject(type, i, j))
//				{
//					return new int[2]{i,j};	
//				}
//			}
//		}
//		return null;
//	}
//	/// <summary>
//	/// Есть ли объект в поле (по типу).
//	/// </summary>
//	/// <returns><c>true</c> if this instance is object of type; otherwise, <c>false</c>.</returns>
//	public bool IsObjectOfType(ObjectTypes type)
//	{
//		return PositionObstacle (type) != null;
//	}
//
//	public void DestroyObjects(int offset)
//	{
//		for(int j = GameData.sizeY-1; j >= GameData.sizeY - offset; j--)
//		{
//			for(int i = 0; i < GameData.sizeX; i++)
//			{
//				if(IsObjectIJMainObj(i,j))
//				{
//					MonoBehaviour.DestroyImmediate(objects[i, j].mainObject.gameObject);	
//				}
//				if(IsObjectIJBackObj(i,j))
//				{
//					MonoBehaviour.DestroyImmediate(objects[i, j].backObject.gameObject);
//				}
//			}
//		}
//		GameData.sizeY -= offset;
//	}
//
//
//

	/// <summary>
	/// Проверяет есть ли объект в указанной строке по типу
	/// </summary>
	/// <returns><c>true</c> if this instance is object of string the specified posJ; otherwise, <c>false</c>.</returns>
	/// <param name="posJ">Position j.</param>
	public bool IsObjectOfString(int posJ, ObjectTypes type)
	{
		for(int i=0; i < GameData.sizeX; i++)
		{
			if(IsObject(type, i, posJ))
			{
				return true;
			}
		}
		return false;
	}
	
	public Properties GetLastPropOfType(List<Properties> objs, ObjectTypes[] types)
	{
		Properties lastProp = null;
		foreach(Properties property in objs)
		{
			foreach(ObjectTypes type in types)
			{
				if(property.GetTypeObject() == type)
				{
					lastProp = property;
					break;
				}
			}
		}
		return lastProp;
	}

	/// <summary>
	/// Возвращает объекты в строке по типу
	/// </summary>
	/// <returns>The object in string.</returns>
	/// <param name="j">J.</param>
	public List<Properties> GetObjectInString(ObjectTypes type, int posJ)
	{
		List<Properties> returnObjs = new List<Properties> ();
		for(int i=0; i<GameData.sizeX; i++)
		{
			if(IsObject(type, i, posJ))
			{
				returnObjs.Add(objects[i, posJ].mainObject);
			}
		}
		return returnObjs;
	}

	public Properties ReturnObjectOfIJPos(int i, int j)
	{
		if(IsObjectIJMainObj(i,j))
		{
			return objects[i,j].mainObject;
		}
		return null;
	}

	public List<Properties> ReturnTypeAround90InField(ObjectTypes type, int[] posIJ)
	{
		List<Properties> aroundObjects = new List<Properties> ();
		int []i = {posIJ [0],posIJ [0],posIJ [0]-1,posIJ [0]+1};
		int []j = {posIJ [1]+1,posIJ [1]-1,posIJ [1],posIJ [1]};
		for(int k = 0; k<4; k++)
		{
			if(ValidateIPosition(i[k])&&ValidateJPosition(j[k]))
			{
				if (IsObject(type, i[k], j[k]))
				{
					aroundObjects.Add(objects[i[k], j[k]].mainObject);
				}
			}
		}
		return aroundObjects;
	}
}
