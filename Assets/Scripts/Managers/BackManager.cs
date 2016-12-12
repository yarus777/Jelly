using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Создает задний фон и сетку для элементов
/// </summary>
public class BackManager{
	/// <summary>
	/// Все объекты
	/// </summary>
	private List<Cube> allCreateObj = new List<Cube>();
	//Ссылка на оригинал фона
	private GameObject back;

	/// <summary>
	/// Ссылка на оригинал 1 кубик
	/// </summary>
	private Cube cube01;
	/// <summary>
	/// Ссылка на оригинал 2 кубик
	/// </summary>
	private Cube cube02;
	/// <summary>
	/// Ссылка на оригинал 3 кубик
	/// </summary>
	private Cube cube03;

	// Имена объектов
	private enum ObjectsBack
	{
		Back01,
		Cube01,
		Cube02,
		Cube03
	}

	/// <summary>
	/// Задний фон
	/// </summary>
	public void CreateBack()
	{
		int orderLayer = -5;
		string path = "Prefabs/Back/";
		back = Resources.Load<GameObject>(path+ObjectsBack.Back01);
		GameObject objCreate = MonoBehaviour.Instantiate (back) as GameObject;
		//objCreate.transform.parent = GameField.parentObject.transform;
		float x = 7.5f;
		float y = 10f;
		objCreate.transform.localPosition = new Vector3 (x, y, objCreate.transform.localPosition.z);
		objCreate.GetComponent<SpriteRenderer> ().sortingOrder = orderLayer;
		//allCreateObj.Add (objCreate);
	}
	/// <summary>
	/// Сетка
	/// </summary>
	public void CreateGrid()
	{
		Vector3 position;
		//int orderLayer = -4;
		for(int j=0; j<GameData.sizeY; j++)
		{
			for(int i=0; i<GameData.sizeX; i++)
			{
				position = GameField.startPos;
				//смещаем позицию создания объекта
				position += new Vector3(i*GameData.distanceBetwObject, j*GameData.distanceBetwObject, 0);
				if(!GameData.manager.IsObject(ObjectTypes.EmptyCell,i,j) && GetCreateCube(i,j)!=null)
				{
					Cube obj = MonoBehaviour.Instantiate(GetCreateCube(i,j)) as Cube;
					obj.transform.position = position;
					obj.transform.parent = GameField.parentObject.transform;
					allCreateObj.Add(obj);
				}
			}
		}
	}

	public void Clear()
	{
		back = null;
		cube01 = null;
		cube02 = null;
		cube03 = null;
		Resources.UnloadUnusedAssets ();
	}

	private Cube GetCreateCube(int i, int j)
	{
		if((i+j)%2 == 0)
			return GetCube(ObjectsBack.Cube01);
		else
			return GetCube(ObjectsBack.Cube02);
	}

	private Cube GetCube(ObjectsBack name)
	{
		string path = "Prefabs/Backgrounds/";
		switch(name)
		{
			case ObjectsBack.Cube01:
				if(cube01==null)
				{
					cube01 = Resources.Load<Cube>(path+name);
				}
				return cube01;
			case ObjectsBack.Cube02:
				if(cube02==null)
				{
					cube02 = Resources.Load<Cube>(path+name);
				}
				return cube02;
			case ObjectsBack.Cube03:
				if(cube03==null)
				{
					cube03 = Resources.Load<Cube>(path+name);
				}
				return cube03;
		}	

		return null;
	}

	public void Visible()
	{
		foreach(Cube cube in allCreateObj)
		{
			cube.Visible();
		}
	}
}
