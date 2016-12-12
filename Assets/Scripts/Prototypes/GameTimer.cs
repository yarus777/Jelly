using UnityEngine;
using System.Collections;

public class GameTimer : CacheTransform {

	public GameTimer()
	{
		GameData.timer = this;
	}

	private void OnTimer()
	{
		if(GameData.limit.GetTypeLimit() == Limit.Time)
		{
			Invoke ("OnTimer", 1f);
			GameData.limit.ChangeLimit (Limit.Time, -1);
			if(GamePlay.LoseLevel())
			{
				if(IsInvoking("OnTimer"))
				{
					CancelInvoke ("OnTimer");
				}
				GameData.windowInterface = 2;
			}
		}
	}

	public void Run()
	{
		OnTimer ();
	}

	public void Pause()
	{
		if(IsInvoking("OnTimer"))
		{
			CancelInvoke ("OnTimer");
		}
	}

	public void Continue()
	{
		if(IsInvoking("OnTimer"))
		{
			CancelInvoke ("OnTimer");
		}
		Run ();
	}
}
