//#define TestGA
using UnityEngine;
using System.Collections;
using Assets.Scripts.MyScripts.Lives;
using Soomla.Store;

public static class InAppManager {


    public static void Buy(EInApp inApp)
    {
        switch (inApp)
        {
            case EInApp.GOLDS_50:
                StoreInventory.BuyItem(MySoomlaStore.COINS_50);
                break;
            case EInApp.GOLDS_100:
                StoreInventory.BuyItem(MySoomlaStore.COINS_100);
                break;
            case EInApp.GOLDS_300:
                StoreInventory.BuyItem(MySoomlaStore.COINS_300);
                break;
            case EInApp.GOLDS_500:
                StoreInventory.BuyItem(MySoomlaStore.COINS_500);
                break;
            case EInApp.GOLDS_1000:
                StoreInventory.BuyItem(MySoomlaStore.COINS_1000);
                break;
            case EInApp.GOLDS_7000:
                StoreInventory.BuyItem(MySoomlaStore.COINS_7000);
                break;
        }
    }



	public static void CompleteMoves()
	{
        Debug.Log(GameData.buyManager);
        Debug.Log(GameData.limit);
		GameData.buyManager.BuyLimit(GameData.limit.GetTypeLimit());
		GamePlay.interfaceGame = StateInterfaceGame.Game;
		GamePlay.blockPauseButton = false;
		/*if(GamePlay.notMovesUI!=null)
		{
			MonoBehaviour.Destroy(GamePlay.notMovesUI.gameObject);
		}*/

	}
    public static void CompleteLife()
	{
        LivesManager.Instance.AddLife(1);
        PlayerPrefs.SetInt("countLife", LivesManager.Instance.LivesCount);
		if (GamePlay.notLifeUI != null) {
				GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.WindowClose, false);
				GamePlay.interfaceMap = StateInterfaceMap.Start;
				if(GamePlay.interfaceGame == StateInterfaceGame.NotLife)
				{
					GamePlay.interfaceGame = StateInterfaceGame.Pause;
				}
				else
				{
					GamePlay.EnableButtonsMap (true);

				}
				MonoBehaviour.Destroy(GamePlay.notLifeUI.gameObject);
		}
	}

    public static void CompleteTimer()
    {
        long locktime = System.Convert.ToInt64(PlayerPrefs.GetString("Gate_" + GamePlay.mapLocker.activeGate.numberGate + "_LockTime", "-1"));
        Debug.Log("Timer: " + locktime);
        locktime -= 30 * 60;
        Debug.Log("Timer: " + locktime);
        PlayerPrefs.SetString("Gate_" + GamePlay.mapLocker.activeGate.numberGate + "_LockTime", locktime.ToString());
        PlayerPrefs.Save();
        Debug.Log("Timer: " + PlayerPrefs.GetString("Gate_" + GamePlay.mapLocker.activeGate.numberGate + "_LockTime", "-1"));
        GamePlay.mapLocker.UpdateGate();

       /* if (GamePlay.unlockInterface != null)
        {
            GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.WindowClose, false);
            GamePlay.interfaceMap = StateInterfaceMap.Start;
            GamePlay.EnableButtonsMap(true);
            MonoBehaviour.Destroy(GamePlay.unlockInterface.gameObject);
        }*/
    }


}
