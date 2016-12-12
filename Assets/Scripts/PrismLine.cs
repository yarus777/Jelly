//#define Debug
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// Класс, в котором реализовано удаление объектов электролинией.
/// </summary>
public class PrismLine{
	/// <summary>
	/// Список списков объектов, которые находятся вокруг элементов, расположенных на электролинии.
	/// </summary>
	public List<List<Properties>> aroundListProperty;

	private float animationBoom = 0.25f;
	
	/// <summary>
	/// Устанавливет значение в aroundListProperty.
	/// </summary>
	/// <param name="prop">Property.</param>
	/// <param name="direction">Direction.</param>
	/// <param name="startDelay">Start delay.</param>
	public void SetListProperty(Properties prop, float startDelay, Colors color)
	{
		aroundListProperty = new List<List<Properties>> ();
//		int[] posIJ = GameData.manager.ReturnIJPosObject (prop);
//		int count = 1;
//		List<Properties> listProperty = GameData.manager.ReturnObjectAroundSquare (posIJ, count);
		List<Properties> listProperty = GameData.manager.ReturnObjectOfColor (color);
		aroundListProperty.Add(listProperty);
		//		int max = 1;
		//
		//
		//		for (int i=0; i<max; i++)
		//		{
		//			aroundListProperty.Add(listProperty);
		//			count++;
		//			listProperty = GameData.manager.ReturnObjectAroundSquare (posIJ, count);
		//		}
		
		DeleteAll (prop, startDelay);
	}
	
	public void DeleteAll(Properties propStart,float startDelay)
	{
		float delay = startDelay;
		delay += animationBoom;
//		float stepDelay =	0.4f;
		foreach (List<Properties> list in aroundListProperty) 
		{
//			delay+=stepDelay;
			foreach(Properties property in list)
			{
				if(property != null && !property.isDelete)
				{
					if(property.InField()){

						switch(property.GetTypeObject())
						{
							case ObjectTypes.Jelly:
								propStart.iPrism.AddPosEffect(property);
//								StartEffect(propStart,property);
								//							delay+=stepDelay;
								GamePlay.DeleteJelly(property, delay, true);
								break;
							case ObjectTypes.BlackJelly:
								//							delay+=stepDelay;
//								GamePlay.DeleteBlackHero(property, delay);
								break;
							case ObjectTypes.Electro:
								if(!property.iElectro.stateActive)
								{
									propStart.iPrism.AddPosEffect(property);
//									StartEffect(propStart,property);
									//								delay+=stepDelay;
									GamePlay.electroForDelete.Add(property);
									property.iElectro.stateActive = true;
									property.delayDelete = delay;
								}
								break;
							case ObjectTypes.Bomb:
								if(!property.iBomb.stateActive)
								{
									propStart.iPrism.AddPosEffect(property);
//									StartEffect(propStart,property);
									//								delay+=stepDelay;
									GamePlay.bombForDelete.Add(property);
									property.iBomb.stateActive = true;
									property.delayDelete = delay;
								}
								break;
							case ObjectTypes.StoneJelly:
								//							delay+=stepDelay;
//								GamePlay.DeleteStone(property, delay);
								break;
							case ObjectTypes.Puddle:
								if(!property.iPuddle.stateActive)
								{
									propStart.iPrism.AddPosEffect(property);
									property.delayDelete = delay;
									property.iPuddle.Attack(GameData.powerUpsAttack);
									property.iPuddle.stateActive = true;
//									property.delayDelete = delay;
								}

								//							delay+=stepDelay;
//								GamePlay.DeletePuddle(property, delay);
								break;
							case ObjectTypes.Snow:
								//							delay+=stepDelay;
//								GamePlay.DeleteSnow(property, delay);
								break;
							case ObjectTypes.Ice:
								//							delay+=stepDelay;
//								GamePlay.DeleteIce(property, delay);
								break;
							case ObjectTypes.Slime:
//								property.iSlime.PrepareDelete(delay);
//								property.isDelete = true;
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

//	public void StartEffect(Properties start, Properties target)
//	{
//		GameObject effect = start.iPrism.GetEffect ();
//		GameObject ob = MonoBehaviour.Instantiate (effect) as GameObject;
//		ob.transform.position = start.transform.position;
//		ob.transform.position -= new Vector3 (0, 0, 1);
//		ob.GetComponent<MoveToPosition> ().PrepareMove (target.transform.position);
//	}
}
