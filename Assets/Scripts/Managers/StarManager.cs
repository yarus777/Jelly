using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StarManager : CacheTransform{
	private List<Star> starsList = new List<Star>();
	public Star[] stars;
	
	void Start()
	{
		GameData.starManager = this;

		for(int i=0; i< stars.Length; i++)
		{
			starsList.Add (stars[i].GetComponentInChildren<Star>());
			starsList[i].SetPoint(GameData.parser.pointsToStar[i]);
			starsList[i].IsFull();
			if(i==0)
			{
				starsList[i].prewValueStar = 0;	
			}
			else
			{
				starsList[i].prewValueStar = starsList[i-1].point;
			}
		}
	}

	public void SetFull()
	{
		for(int i=0; i< starsList.Count; i++)
		{
			if(!starsList[i].isFull)
			{
				starsList[i].IsFull();
			}

		}
	}
}
