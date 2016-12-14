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



}
