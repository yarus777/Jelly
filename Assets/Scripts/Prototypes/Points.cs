using UnityEngine;
using System.Collections;

public class Points : IPoints{
	private int point;
	private int bonusPoint;

	public Points(int point)
	{
		this.point = point;
		this.bonusPoint = 0;
	}

	#region IPoints implementation
	
	public int GetPoint ()
	{
		return point + bonusPoint;
	}

	public void SetBonusPoint (int bonusPoint)
	{
		this.bonusPoint = bonusPoint;
	}
	
	#endregion
}
