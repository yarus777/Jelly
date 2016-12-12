using UnityEngine;
using System.Collections;

public class BuyManager{
	private int buyMoves = 3;
	private int buySeconds = 5;


	public void BuyLimit(Limit type)
	{
		LevelManager level  = Camera.main.GetComponent<LevelManager>();
		switch(type)
		{
			case Limit.Moves:
				GameData.limit.AddLimitValue (buyMoves);
				break;
			case Limit.Time:
				GameData.limit.AddLimitValue(buySeconds);
				GameData.timer.Continue();
				break;
		}
		level.Stroke();
	}
}
