using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.MyScripts.Lives;
using Assets.Scripts.Sounds;

public class ButtonsInterface : MonoBehaviour
{

    public enum Buttons
    {
        Settings,
        PlayGame,
        OKNotLife,
        WinMainMenu,
        WinRestart,
        WinNextLevel,
        CloseSettings,
        Sound,
        Music,
        Capitulate,
        Buy,
        LoseMainMenu,
        LoseRestart
    }

    public Buttons button;
    public List<Sprite> states;
    private SpriteRenderer spriteRenderer;
    private bool _enabled = true;

    void Awake()
    {

        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateState(StateButton.Normal);
        SoundMusicInit();
        spriteRenderer.sharedMaterial.color = new Color(1f, 1f, 1f, 1f);
    }

    void OnMouseUpAsButton()
    {
        if (!_enabled)
        {
            //Debug.LogWarning("buttons disabled");
            return;
        }
        Debug.Log("GamePlay.enableButtonInterface UP as button " + GamePlay.enableButtonInterface);
        if (UpdateApp.stopOther) return;
        if (GamePlay.enableButtonInterface)
        {
            switch (button)
            {
                case Buttons.Settings:
                    Settings(false);
                    break;
                case Buttons.PlayGame:
                    PreparePlayGame();
                    break;
                case Buttons.OKNotLife:
                    OkNotLife();
                    break;
                case Buttons.WinMainMenu:
                    PrepareWinMainMenu();
                    break;
                case Buttons.WinNextLevel:
                    PrepareWinNextLevel();
                    break;
                case Buttons.WinRestart:
                    PrepareWinRestart();
                    break;
                case Buttons.CloseSettings:
                    CloseSettings();
                    break;
                case Buttons.Music:
                    SwitchMusic();
                    break;
                case Buttons.Sound:
                    SwitchSound();
                    break;
                case Buttons.Capitulate:
                    Capitulate();
                    break;
                case Buttons.Buy:
                    Buy();
                    break;
                case Buttons.LoseMainMenu:
                    PrepareloseMainMenu();
                    break;
                case Buttons.LoseRestart:
                    LoseRestart();
                    break;

            }
            UpdateState(StateButton.Normal);
        }
    }


    void OnMouseDown()
    {
        Debug.Log("GamePlay.enableButtonInterface Down " + GamePlay.enableButtonInterface);
        if (UpdateApp.stopOther) return;
        if (GamePlay.enableButtonInterface)
        {
            UpdateState(StateButton.Highlight);
            if (button != Buttons.Music && button != Buttons.Sound)
            {
                GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.ButtonPush1, false);
            }
        }
    }

    void OnMouseUp()
    {
        Debug.Log("GamePlay.enableButtonInterface UP " + GamePlay.enableButtonInterface);
        if (UpdateApp.stopOther) return;
        if (GamePlay.enableButtonInterface)
        {
            UpdateState(StateButton.Normal);
            if (button != Buttons.Music && button != Buttons.Sound)
            {
                GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.ButtonPush2, false);
            }
        }
    }

    public void UpdateState(StateButton state)
    {
        if (button != Buttons.Music && button != Buttons.Sound)
        {
            spriteRenderer.sprite = states[(int)state];
        }
    }

    private void Settings(bool credits)
    {
        if (GamePlay.interfaceMap == StateInterfaceMap.Start)
        {
            GamePlay.stateUI = EUI.Settings;
            GamePlay.interfaceMap = StateInterfaceMap.Interface1;

            GameObject obj = Instantiate(Resources.Load("Prefabs/Popups/SettingsPopup")) as GameObject;
            obj.transform.parent = GameObject.Find("UICanvas").transform;
            obj.transform.localScale = Vector3.one;
            var tr = obj.GetComponent<RectTransform>();
            tr.anchoredPosition3D = Vector3.zero;
            tr.offsetMax = Vector2.zero;
            tr.offsetMin = Vector2.zero;

            if (!credits)
            {
                GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.WindowOpen, false);
            }
        }
    }


    private void PreparePlayGame()
    {
        if (GamePlay.interfaceMap != StateInterfaceMap.Start)
        {
//#if UNITY_ANDROID
            //Debug.Log("maxCompleteLevel: " + GamePlay.maxCompleteLevel + "\nnumberLoadLevel " + GameData.numberLoadLevel);
            Debug.Log("stars: " + PlayerPrefs.GetInt("starsLevel" + GameData.numberLoadLevel, -1));
            if (GamePlay.maxCompleteLevel == GameData.numberLoadLevel && PlayerPrefs.GetInt("starsLevel" + GameData.numberLoadLevel, -1) == -1)
            {
                //Debug.Log("AdSDK Send Event - UNIQUE_START_LEVEL_" + GameData.numberLoadLevel.ToString("000"));
                AdSDK.SendEvent("UNIQUE_START_LEVEL_" + GameData.numberLoadLevel.ToString("000"));
                PlayerPrefs.SetInt("starsLevel" + GameData.numberLoadLevel, 0);
            }
            else
            {
                //Debug.Log("AdSDK Send Event - REPLAY_LEVEL_" + GameData.numberLoadLevel.ToString("000"));
                AdSDK.SendEvent("REPLAY_LEVEL_" + GameData.numberLoadLevel.ToString("000"));
            }
//#endif
            GamePlay.enableButtonInterface = false;
            LivesManager.Instance.SpendLife(1);
            //GamePlay.ChangeCountLife(-1);
            //GamePlay.mapInterface.UpdateLife();
            GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.ButtonPlay, false);
            PlayerPrefs.SetInt("lastOpenLevel", GameData.numberLoadLevel);
            Invoke("PlayGame", 0.7f);
        }
    }

    public void PlayGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("SplashScreen");
    }

    public void OkNotLife()
    {
        GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.WindowClose, false);
        GamePlay.interfaceMap = StateInterfaceMap.Start;
        if (GamePlay.interfaceGame == StateInterfaceGame.NotLife)
        {
            GamePlay.interfaceGame = StateInterfaceGame.Pause;
        }
        else
        {
            GamePlay.EnableButtonsMap(true);
        }
        Destroy(this.transform.parent.gameObject);

    }


    private void PrepareWinMainMenu()
    {
        Debug.Log(GameData.numberLoadLevel);
        if (PlayerPrefs.GetInt("firstWin", 0) == 0 && GameData.numberLoadLevel - 1 == 1)
        {
            Debug.Log("AdSDK Send Event\n\tWIN_1LEVEL_CLICK_MENU");
            AdSDK.SendEvent("WIN_1LEVEL_CLICK_MENU");
            PlayerPrefs.SetInt("firstWin", 1);
        }
        GameData.numberLoadLevel++;
        GamePlay.enableButtonInterface = false;
        GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.ButtonToMap, false);
        Invoke("WinMainMenu", 0.7f);
    }

    private void PrepareloseMainMenu()
    {
        GamePlay.enableButtonInterface = false;
        GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.ButtonToMap, false);
        Invoke("WinMainMenu", 0.7f);
    }
    private void WinMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("ToMapScene");
    }

    private void PrepareWinNextLevel()
    {
        if (PlayerPrefs.GetInt("firstWin", 0) == 0 && GameData.numberLoadLevel - 1 == 1)
        {
            //Debug.Log("AdSDK Send Event\n\tWIN_1LEVEL_CLICK_NEXT");
            AdSDK.SendEvent("WIN_1LEVEL_CLICK_NEXT");
            PlayerPrefs.SetInt("firstWin", 1);
        }
        if (GameData.numberLoadLevel < 100)
        {
            var _isTutorialShown = PlayerPrefs.GetInt("tutorial_map", 0) == 1;
            if (GameData.numberLoadLevel == 6 && !_isTutorialShown)
            {
                Debug.Log("Set Start btn");
                GamePlay.interfaceMap = StateInterfaceMap.Start;
            }
            else
            {
                Debug.Log("Set StartNextLvl btn");
                GamePlay.interfaceMap = StateInterfaceMap.StartNextLvl;
            }
            GamePlay.enableButtonInterface = false;
            GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.ButtonPlay, false);
            Debug.Log("sheet is here");          
            Invoke("WinNextLevel", 0.7f);
        }
        else {
            GamePlay.enableButtonInterface = false;
            GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.ButtonPlay, false);
            Invoke("WinNextLevel", 0.7f);
        }

    }

    private void WinNextLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("ToMapScene");
    }

    private void PrepareWinRestart()
    {
        if (PlayerPrefs.GetInt("firstWin", 0) == 0 && GameData.numberLoadLevel - 1 == 1)
        {
            Debug.Log("AdSDK Send Event\n\tWIN_1LEVEL_CLICK_RESTART");
            AdSDK.SendEvent("WIN_1LEVEL_CLICK_RESTART");
            PlayerPrefs.SetInt("firstWin", 1);
        }
        if (LivesManager.Instance.LivesCount < 1)
        {
            CreateNotLife();
            return;
        }
        //GamePlay.ChangeCountLife(-1);
        LivesManager.Instance.SpendLife(1);
        GamePlay.enableButtonInterface = false;
        GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.ButtonReplay, false);

        Invoke("WinRestart", 0.7f);
    }

    private void CreateNotLife()
    {
        GamePlay.interfaceGame = StateInterfaceGame.NotLife;
        GameObject ob = Instantiate(Resources.Load("Prefabs/Interface/NotLife")) as GameObject;
        ob.transform.localPosition = new Vector3(0f, 0f, -5f);
        ob.transform.localScale = new Vector3(0.75f, 0.75f, 1);
        GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.WindowNotMoves, false);
    }
    private void WinRestart()
    {
        GameData.numberLoadLevel--;
//#if UNITY_ANDROID
        Debug.Log("AdSDK Send Event - REPLAY_LEVEL_" + GameData.numberLoadLevel.ToString("000"));
        AdSDK.SendEvent("REPLAY_LEVEL_" + GameData.numberLoadLevel.ToString("000"));
//#endif

        GamePlay.RestartLevel();
    }
    private void LoseRestart()
    {
        if (LivesManager.Instance.LivesCount < 1)
        {
            CreateNotLife();
            return;
        }
        LivesManager.Instance.SpendLife(1);
        //GamePlay.ChangeCountLife(-1);
//#if UNITY_ANDROID
        Debug.Log("AdSDK Send Event - REPLAY_LEVEL_" + GameData.numberLoadLevel.ToString("000"));
        AdSDK.SendEvent("REPLAY_LEVEL_" + GameData.numberLoadLevel.ToString("000"));
//#endif

        GamePlay.RestartLevel();
    }
    private void CloseSettings()
    {
        if (GamePlay.interfaceMap != StateInterfaceMap.Start)
        {
            GamePlay.stateUI = EUI.Map;

            Destroy(transform.parent.gameObject);
            GamePlay.interfaceMap = StateInterfaceMap.Start;
            GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.WindowClose, false);
        }

        if (GamePlay.interfaceGame == StateInterfaceGame.Settings)
        {
#if UNITY_ANDROID
           // AdSDK.SetBannerVisible(false);
#endif
            if (GamePlay.interfacePause != null)
            {
                Destroy(GamePlay.interfacePause);
            }
            GamePlay.SetInput(true);
            GamePlay.interfaceGame = StateInterfaceGame.Game;
            GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.ButtonPlay, false);
            GamePlay.EnableButtonsMap(true);
            Time.timeScale = 1;
            if (GamePlay.inventoryCollider != null)
            {
                GamePlay.inventoryCollider.enabled = true;
            }
        }
    }

    private void SwitchMusic()
    {
        if (MusicManager.Instance.IsMusic)
        {
            MusicManager.Instance.IsMusic = false;
            GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.ButtonOff, false);
        }
        else
        {
            MusicManager.Instance.IsMusic = true;
            GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.ButtonOn, false);
        }
        SoundMusicInit();
    }

    private void SwitchSound()
    {
        GamePlay.soundOn = !GamePlay.soundOn;
        if (GamePlay.soundOn)
        {
            PlayerPrefs.SetInt("sound", 0);
            GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.ButtonOff, true);
        }
        else
        {
            PlayerPrefs.SetInt("sound", 1);
            GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.ButtonOn, true);
        }
        //GamePlay.LoadSoundSettings();
        GamePlay.SoundSettings();
        SoundMusicInit();
    }

    private void SoundMusicInit()
    {
        int capture = 0;
        if (button == Buttons.Sound)
        {
            capture = PlayerPrefs.GetInt("sound");            
        }
        else if (button == Buttons.Music)
        {
            //capture = PlayerPrefs.GetInt("music");
            capture = !MusicManager.Instance.IsMusic ? 1 : 0;
            Debug.Log("capture"+capture);
            
        }
        spriteRenderer.sprite = states[capture];
    }

    private void Capitulate()
    {
        GamePlay.lvlManager.Capitulate();
        Destroy(transform.parent.gameObject);
    }

    private void Buy()
    {
        if (!GamePlay.onlyOneMoves)
        {

            Debug.Log("AdSDK Show Video For Moves");
            GamePlay.onlyOneMoves = true;
#if UNITY_EDITOR
            InAppManager.CompleteMoves();
#endif
#if UNITY_ANDROID
            AdSDK.forMoves = true;
           // if (GamePlay.loadVideo != null)
           //    GamePlay.loadVideo.GiveOne();
#endif
        }
    }




}
