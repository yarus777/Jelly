using UnityEngine;
using System.Collections;
using Assets.Scripts.MyScripts.Popups;

public class LoadWinLose : MonoBehaviour {

	void Start () {

	    if (GamePlay.maxCompleteLevel >= 5)
	    {
	        if (GameData.fullScreenCounterLose%2 == 0)
	        {
	            Debug.Log("AdSDK: Show FullScreen Lose");
                #if !UNITY_EDITOR
	            AdSDK.ShowIntersisialAd();
                #endif
            }
	        GameData.fullScreenCounterLose++;
	    }

	    if (GamePlay.maxCompleteLevel >= 3)
	    {
	        AdSDK.SetBannerVisible(true);
	    }

        if (GamePlay.WinLevel())
        {
            string level = "starsLevel" + GameData.numberLoadLevel;
            int countStars = PlayerPrefs.GetInt(level);

            Debug.Log("GamePlay.countStarsLevel " + GamePlay.countStarsLevel);

            if (GamePlay.countStarsLevel == 3 && countStars!=3)
            {               
                GamePlay.FullStarsLevelCount++;
                Debug.Log("GamePlay.FullStarsLevelCount " + GamePlay.FullStarsLevelCount);
                ProgressController.instance.SetProgress("Conqueror", GamePlay.FullStarsLevelCount);
            }

            if (countStars < GamePlay.countStarsLevel)
            {
                PlayerPrefs.SetInt(level, GamePlay.countStarsLevel);
                PlayerPrefs.Save();
            }

            PopupsController.Instance.Show(PopupType.Win);
            //Instantiate(Resources.Load("Prefabs/Interface/Win"));
            GameData.fullScreenCounterWin++;

#if UNITY_ANDROID
            if (GamePlay.maxCompleteLevel == GameData.numberLoadLevel)
            {
                //Debug.Log("AdSDK: Send Event - UNIQUE_FINISHED_LEVEL_" + GameData.numberLoadLevel.ToString("000"));
                AdSDK.SendEvent("UNIQUE_FINISHED_LEVEL_" + GameData.numberLoadLevel.ToString("000"));
            }
            //Debug.Log("AdSDK: Send Event - FINISHED_LEVEL_" + GameData.numberLoadLevel.ToString("000") + "_STARS_" + GamePlay.countStarsLevel);
            AdSDK.SendEvent("FINISHED_LEVEL_" + GameData.numberLoadLevel.ToString("000") + "_STARS_" + GamePlay.countStarsLevel);
#endif
            if (GameData.numberLoadLevel != GameData.allLevels)
            {               
                    GameData.numberLoadLevel++;
                    Debug.Log("maxCompleteLevel: " + GamePlay.maxCompleteLevel + "\n numberLoadLevel: " + GameData.numberLoadLevel);
                    if (GamePlay.maxCompleteLevel < GameData.numberLoadLevel)
                        GamePlay.maxCompleteLevel = GameData.numberLoadLevel;

            }
        }
		else
		{
            string level = "starsLevel" + GameData.numberLoadLevel;
            int countStars = PlayerPrefs.GetInt(level);
            if (countStars == -1)
            {
                PlayerPrefs.SetInt(level, 0);
                PlayerPrefs.Save();
            }
            //Instantiate(Resources.Load("Prefabs/Interface/Lose"));
		    PopupsController.Instance.Show(PopupType.Lose);
#if UNITY_ANDROID
            Debug.Log("AdSDK: SEND EVENT - FAILED_LEVEL_" + GameData.numberLoadLevel.ToString("000"));
            AdSDK.SendEvent("FAILED_LEVEL_" + GameData.numberLoadLevel.ToString("000"));
#endif
        }
		PlayerPrefs.SetInt ("countRateUs", PlayerPrefs.GetInt("countRateUs")+1);
	}



}
