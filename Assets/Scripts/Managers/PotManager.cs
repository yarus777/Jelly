using UnityEngine;
using System.Collections;

/// <summary>
/// Управление кристаллами в сцене
/// </summary>
public class PotManager{
	#region Variables
	/// <summary>
	/// Текущее количество бриллинтов в сцене
	/// </summary>
	private int currentCountPot;
	/// <summary>
	/// Минимальное количество бриллиантов в сцене
	/// </summary>
	private int minNumberPot;
	/// <summary>
	/// Максимальное количество брилллиантов в сцене
	/// </summary>
	private int maxNumberPot;
	/// <summary>
	/// Шанс появления бриллианта в сцене
	/// </summary>
	private int chancePot;

	/// <summary>
	/// Нужно ли создавать бриллиант
	/// </summary>
	private bool needPot;
	/// <summary>
	///Контролирует необходимость создания бриллиантов в NeedCreateDiamond() "подряд"
	/// </summary>
	private bool createdPotInScene;
	#endregion

	/// <summary>
	/// Устанавливает значения, необходимые для создания бриллиантов 
	/// </summary>
	/// <param name="min">Minimum.</param>
	/// <param name="max">Max.</param>
	/// <param name="chance">Chance.</param>
	/// <param name="needDiamond">If set to <c>true</c> need diamond.</param>
	public void SetData(int min, int max, int chance, bool needDiamond)
	{
		minNumberPot = min;
		maxNumberPot = max;
		chancePot = chance;
		this.needPot = needDiamond;
	}

	/// <summary>
	/// Сбрасывает данные 
	/// </summary>
	public void ResetData()
	{
		currentCountPot = 0;
		minNumberPot = 0;
		maxNumberPot = 0;
		chancePot = 0;
		needPot = false;
		createdPotInScene = false;
	}

	/// <summary>
	/// Sets the need diamond.
	/// </summary>
	/// <param name="state">If set to <c>true</c> state.</param>
	public void SetNeedDiamond(bool state)
	{
		needPot = state;
	}

	public int GetCurrentCountDiamond()
	{
		return currentCountPot;
	}

	public void AddCurrentCountDiamond()
	{
		currentCountPot++;
	}

	public void DeleteCurrentCountDiamond()
	{
		currentCountPot--;
	}

	public int GetMinNumberDiamond()
	{
		return minNumberPot;
	}

	public int GetMaxNumberDiamond()
	{
		return maxNumberPot;
	}

	public bool GetNeedDiamond()
	{
		return needPot;
	}

	public bool NeedCreateDiamond()
	{
		if(needPot)
		{

			if(!IsFullCreatedDiamond())
			{
				if(currentCountPot < minNumberPot)
				{
					createdPotInScene = false;
					return true;
				}
				else if(createdPotInScene && currentCountPot < maxNumberPot)
				{
					int randomChanse = Random.Range(0, 100);
					if(randomChanse < chancePot)
					{
						createdPotInScene = false;
						return true;
					}
				}
				createdPotInScene = true;
			}
		}
		return false;
	}

	public bool IsFullCreatedDiamond()
	{
		foreach(TaskLevel task  in GameData.taskLevel)
		{
			if(task.GetTaskType() == Task.Diamond)
			{
				if(currentCountPot == task.GetGoal() - task.GetCurrent())
				{
					return true;
				}
			}
		}

		return false;
	}
}
