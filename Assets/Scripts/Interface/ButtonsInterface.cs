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
        Sound,
        Music,
        Capitulate,
        Buy

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
                case Buttons.Music:
                    SwitchMusic();
                    break;
                case Buttons.Sound:
                    SwitchSound();
                    break;
                case Buttons.Buy:
                    Buy();
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
