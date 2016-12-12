//#define Debug
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// Класс, в котором реализовано удаление объектов электролинией.
/// </summary>
public class ElectroLine{

	public RemovingDirection direction;
	/// <summary>
	/// Список списков объектов, которые находятся вокруг элементов, расположенных на электролинии.
	/// </summary>
	public List<List<Properties>> aroundListProperty;

	/// <summary>
	/// Устанавливет значение в aroundListProperty.
	/// </summary>
	/// <param name="prop">Property.</param>
	/// <param name="direction">Direction.</param>
	/// <param name="startDelay">Start delay.</param>
	public void SetListProperty(Properties prop, RemovingDirection direction, float startDelay)
	{
		aroundListProperty = new List<List<Properties>> ();
		int[] posIJ = GameData.manager.ReturnIJPosObject (prop);
		int count = 1;
		List<Properties> listProperty = GameData.manager.ReturnObjectAroundLine (posIJ, count, direction);
		int max = Mathf.Max (GameData.sizeX, GameData.sizeY);
		for (int i=0; i<max; i++)
		{
			aroundListProperty.Add(listProperty);
			count++;
			listProperty = GameData.manager.ReturnObjectAroundLine (posIJ, count, direction);
		}

		DeleteAll (startDelay);
	}

	public void DeleteAll(float startDelay)
	{
		float delay = startDelay;
		float stepDelay = 0.03f;
		foreach (List<Properties> list in aroundListProperty) 
		{
			delay+=stepDelay;
			foreach(Properties property in list)
			{
				if(property != null && !property.isDelete)
				{
					if(property.InField()){
						switch(property.GetTypeObject())
						{
							case ObjectTypes.Jelly:
								
								GamePlay.DeleteJelly(property, delay, true);
								break;
							case ObjectTypes.BlackJelly:
//								delay+=stepDelay;
								GamePlay.DeleteBlackHero(property, delay);
								break;
							case ObjectTypes.Electro:
								if(!property.iElectro.stateActive)
								{
//									delay+=stepDelay;
									GamePlay.electroForDelete.Add(property);
									property.iElectro.stateActive = true;
									property.delayDelete = delay;
								}
								break;
							case ObjectTypes.Bomb:
								if(!property.iBomb.stateActive)
								{
//									delay+=stepDelay;
									GamePlay.bombForDelete.Add(property);
									property.iBomb.stateActive = true;
									property.delayDelete = delay;
								}
								break;
							case ObjectTypes.StoneJelly:
//								delay+=stepDelay;
								GamePlay.DeleteStone(property, delay);
								break;
							case ObjectTypes.Puddle:
								if(!property.iPuddle.stateActive)
								{
									property.delayDelete = delay;
									property.iPuddle.Attack(GameData.powerUpsAttack);
									property.iPuddle.stateActive = true;
//									property.delayDelete = delay;
								}
//								delay+=stepDelay;
//								GamePlay.DeletePuddle(property, delay);
								break;
							case ObjectTypes.Snow:
//								delay+=stepDelay;
								GamePlay.DeleteSnow(property, delay);
								break;
							case ObjectTypes.Ice:
//								delay+=stepDelay;
								GamePlay.DeleteIce(property, delay);
								break;
							case ObjectTypes.Slime:
								property.iSlime.PrepareDelete(delay);
								property.isDelete = true;
								break;
							case ObjectTypes.Prism:
								if(!property.iPrism.stateActive)
								{
									//									delay+=stepDelay;
									GamePlay.prismForDelete.Add(property);
									property.iPrism.RandomColor();
									property.iPrism.SetSpeed(0.1f);
									property.iPrism.Resume();
									property.iPrism.stateActive = true;
									property.delayDelete = delay+0.5f;
								}
								break;
						}
					}
				}
			}
		}
		#if Debug
		Debug.Log("All");
		#endif
	}
}
