using UnityEngine;
using System.Collections;

public class Limitation {
	private Limit type;
	private int currentValueLimit;
	private int maxValueLimit;

	public Limitation(Limit type, int valueLimit)
	{
		this.type = type;
		this.currentValueLimit = this.maxValueLimit = valueLimit;
	}

	public Limitation(Limit type)
	{
		this.type = type;
	}

	public bool EndLimit()
	{
		switch(type)
		{
			case Limit.NotLimit:
				return false;
			case Limit.Time:
				return (currentValueLimit <= 0);
			case Limit.Moves:
				return (currentValueLimit <= 0);
			default:
				return true;
		}
	}

	public void ChangeLimitType(Limit type)
	{
		this.type = type;
	}

	public void ChangeLimit(Limit type, int value)
	{
		if(this.type == type)
		{
			currentValueLimit += value;
		}
	}

	public string GetLimitName()
	{
		if(type != Limit.NotLimit)
		{
			return type.ToString();
		}
		return "";
	}

	public string GetLimitCount()
	{
		if(type != Limit.NotLimit)
		{
			return currentValueLimit.ToString();
		}
		return "";
	}


	public Limit GetTypeLimit()
	{
		return type;
	}

	public int GetValueLimit()
	{
		return currentValueLimit;
	}

	public void AddLimitValue(int value)
	{
		currentValueLimit += value;
	}

	public int GetPassedLimit()
	{
		return maxValueLimit - currentValueLimit;
	}
}
