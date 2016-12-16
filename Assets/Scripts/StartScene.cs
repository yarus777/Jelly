using System.Collections;
using UnityEngine;

public class StartScene : MonoBehaviour {
    void Awake() {
        if (PlayerPrefs.GetInt("CLOSE_LVL", 0) == 0) {
            for (var i = 1; i <= GameData.allLevels; i++) {
                var level = "starsLevel" + i;

                var stars = PlayerPrefs.GetInt(level);
                Debug.Log("starsLevel" + i + ": " + stars);
                if (stars == 0) {
                    GamePlay.maxCompleteLevel = i;
                    PlayerPrefs.SetInt("CLOSE_LVL", 1);
                    return;
                }
            }
        }

        GameData.numberLoadLevel = GamePlay.LastOpenedLvl;
        PlayerPrefs.SetInt("updateAppScreen", PlayerPrefs.GetInt("updateAppScreen", 0) + 1);

        //PlayerPrefs.SetInt("maxCompleteLevel", 5);
    }


    IEnumerator Start() {
        yield return new WaitForSeconds(0.5f);
        GamePlay.LoadSoundSettings();
        AsyncOperation loadLevel;
        if (PlayerPrefs.GetInt("firstStart", 0) == 0) {
            GameData.numberLoadLevel = 1;
            loadLevel = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("GameField");
            PlayerPrefs.SetInt("firstStart", 1);
        } else {
            loadLevel = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("TestMap");
        }
        yield return loadLevel;
    }
}