using UnityEngine;
using System.Collections;

public class StartScene : MonoBehaviour {

	void Awake()
	{
        //Debug.Log (PlayerPrefs.GetString ("lastTimeLife"));
        //PlayerPrefs.DeleteAll();
        //PlayerPrefs.SetInt ("countLife", 0);
        //GamePlay.ChangeCountLife(5);
        //PlayerPrefs.SetString("Gate_2_LockTime", "-1");
        //PlayerPrefs.SetInt("Gate_2_State", 0);

        if (PlayerPrefs.GetInt("CLOSE_LVL",0) == 0)
        for (int i = 1; i <= GameData.allLevels; i++)
        {
            string level = "starsLevel" + i;
            
            int stars = PlayerPrefs.GetInt(level);
            Debug.Log("starsLevel" + i + ": " + stars);
            if (stars == 0)
            {
                
                GamePlay.maxCompleteLevel = i;
                    PlayerPrefs.SetInt("CLOSE_LVL", 1);
                    //Debug.Log("Dfs");
                return;
            }
        }

        var _isTutorialShown = PlayerPrefs.GetInt("tutorial_map", 0) == 1;
        if (GamePlay.maxCompleteLevel == 6 && !_isTutorialShown)
        {
            GamePlay.interfaceMap = StateInterfaceMap.Start;
            Debug.Log("Set Start scene");
        }
        else
        {
            GamePlay.interfaceMap = StateInterfaceMap.StartNextLvl;
            Debug.Log("Set StartNextLvl scene");
        }

        GameData.numberLoadLevel = PlayerPrefs.GetInt("lastOpenLevel", 1);
        PlayerPrefs.SetInt("updateAppScreen", PlayerPrefs.GetInt("updateAppScreen",0)+1);
        
        /*int conv = PlayerPrefs.GetInt("conversionCount", 1);
        if (conv < 3)
        {
            Debug.Log("conversionCount " + conv);
            conv++;
            PlayerPrefs.SetInt("conversionCount", conv);
        }
        else
        {
            Debug.Log("AdSDK Send Event - SendConversion");
            AdSDK.SendConversion();
            PlayerPrefs.SetInt("conversionCount", 1);

        }
         */

       // PlayerPrefs.SetInt("maxCompleteLevel", 10);

    }

    // Use this for initialization
    IEnumerator Start () {
		yield return new WaitForSeconds (0.5f);
        AsyncOperation loadLevel;
        /*if (PlayerPrefs.GetInt("firstStart", 0) == 0)
        {
            GameData.numberLoadLevel = 1;
            loadLevel = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("GameField");
            PlayerPrefs.SetInt("firstStart", 1);
        }
        else
        {
            //loadLevel = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Map");
            loadLevel = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("TestMap");
        }*/
        loadLevel = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("TestMap");
		yield return loadLevel;
	}
}
