using UnityEngine;
using System.Collections;
using Assets.Scripts.MyScripts.Lives;

public class StartNewLvl : MonoBehaviour {
    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < GameData.locationsCount - 1; i++)
        {
            int k = System.Convert.ToInt32(PlayerPrefs.GetInt("Gate_" + i + "_State", 0));
            if (k == (int)Gate.GateStates.LockToGame || k == (int)Gate.GateStates.Waiting)
            {
                GamePlay.interfaceMap = StateInterfaceMap.Start;
                Debug.Log("+");

            }
        }
        if (PlayerPrefs.GetInt("firstStart", 0) == 0)
        {
            if (ProgressController.instance.IsWindowActive)
            {
                return;
            }
            GamePlay.EnableButtonsMap(false);
            
                Invoke("StartFirstLevel", 3f);
            
        }
        else
        {

            if (GamePlay.maxCompleteLevel >= 1)
            {

                //Debug.Log(GamePlay.interfaceMap);
                if (GamePlay.interfaceMap == StateInterfaceMap.StartNextLvl)
                {
                    if (!ProgressController.instance.IsWindowActive)
                    {
                        GamePlay.EnableButtonsMap(false);
                        Invoke("StartNewLvlSC", 1f);
                    }
                }
            }
        }
    }

    private void StartLevel()
    {
        if (GamePlay.enableButtonInterface)
        {
            if (LivesManager.Instance.LivesCount > 0 && !ProgressController.instance.IsWindowActive)
            {
                GameData.numberLoadLevel = PlayerPrefs.GetInt("lastOpenLevel", 1);
                Debug.Log(GameData.numberLoadLevel);
                GamePlay.interfaceMap = StateInterfaceMap.Interface1;
                GameObject ob = Instantiate(Resources.Load("Prefabs/Interface/Loadout")) as GameObject;
                ob.transform.parent = Camera.main.transform;
                ob.transform.localPosition = new Vector3(0, 1, 6);
                //GamePlay.loadoutInterface = ob.GetComponent<Loadout>();

                string level = "starsLevel" + GameData.numberLoadLevel;
                //GamePlay.loadoutInterface.countStars = PlayerPrefs.GetInt(level);
                //GamePlay.loadoutInterface.InitStars();
                //ob.GetComponentInChildren<LevelLabel>().SwitchNumber();
                PlayerPrefs.SetInt("lastOpenLevel", GameData.numberLoadLevel);
                GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.WindowOpenLevel, false);
            }
            else
            {
                GamePlay.interfaceMap = StateInterfaceMap.Start;
                GamePlay.EnableButtonsMap(true);
            }
        }
    }

    private void StartFirstLevel()
    {
        if (GamePlay.enableButtonInterface)
        {
            if (LivesManager.Instance.LivesCount > 0 && !ProgressController.instance.IsWindowActive)
            {
                PlayerPrefs.SetInt("firstStart", 1);
                GameData.numberLoadLevel = PlayerPrefs.GetInt("lastOpenLevel", 1);
                GamePlay.interfaceMap = StateInterfaceMap.Interface1;
                GameObject ob = Instantiate(Resources.Load("Prefabs/Interface/Loadout")) as GameObject;
                ob.transform.parent = Camera.main.transform;
                ob.transform.localPosition = new Vector3(0, 1, 6);
                //GamePlay.loadoutInterface = ob.GetComponent<Loadout>();

                string level = "starsLevel" + GameData.numberLoadLevel;
                //GamePlay.loadoutInterface.countStars = PlayerPrefs.GetInt(level);
                //GamePlay.loadoutInterface.InitStars();
               // ob.GetComponentInChildren<LevelLabel>().SwitchNumber();
                PlayerPrefs.SetInt("lastOpenLevel", GameData.numberLoadLevel);
                GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.WindowOpenLevel, false);
            }
            else
            {
                GamePlay.interfaceMap = StateInterfaceMap.Start;
                GamePlay.EnableButtonsMap(true);
            }
        }
    }

    private void StartNewLvlSC(){
        if (GamePlay.enableButtonInterface)
        {
            if (LivesManager.Instance.LivesCount > 0 && !ProgressController.instance.IsWindowActive)
            {
                //Debug.Log("newLVL");
                Debug.Log(GamePlay.interfaceMap);
                GamePlay.interfaceMap = StateInterfaceMap.Interface1;
                GameObject ob = Instantiate(Resources.Load("Prefabs/Interface/Loadout")) as GameObject;
                ob.transform.parent = Camera.main.transform;
                ob.transform.localPosition = new Vector3(0, 1, 6);
                //GamePlay.loadoutInterface = ob.GetComponent<Loadout>();

                string level = "starsLevel" + GameData.numberLoadLevel;
               // GamePlay.loadoutInterface.countStars = PlayerPrefs.GetInt(level);
               // GamePlay.loadoutInterface.InitStars();
                //ob.GetComponentInChildren<LevelLabel>().SwitchNumber();
                PlayerPrefs.SetInt("lastOpenLevel", GameData.numberLoadLevel);
                GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.WindowOpenLevel, false);
            }
            else
            {
                Debug.Log("newLVL");
                GamePlay.interfaceMap = StateInterfaceMap.Start;
                GamePlay.EnableButtonsMap(true);
            }
        }
    }
}
