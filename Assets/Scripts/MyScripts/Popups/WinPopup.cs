﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.MyScripts.Lives;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.MyScripts.Popups
{
    
    class WinPopup : Popup
    {
        [SerializeField]
        private Text recordTxt;
        [SerializeField]
        private Image img;
        [SerializeField]
        private Text levelTxt;
        private int _currentLvl;
        [SerializeField]
        private List<GameObject> stars;
        [SerializeField]
        private Sprite[] sprites;
        private int lastStar;
        public override void Close()
        {
            HideStars();
            GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.WindowClose, false);
            base.Close();

        }

        public override void OnShow()
        {
            _currentLvl = GameData.numberLoadLevel;
            levelTxt.text = Texts.GetText(WhatText.LevelTxt) + " " + _currentLvl;
            InitRecord();
            InitStars();
            ChangeImg();
            GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.WindowLevelWin, false);
            base.OnShow();

        }

        private void ChangeImg()
        {
            switch (Application.systemLanguage)
            {
                case SystemLanguage.Russian:
                    img.sprite = sprites[(int)(StringConstants.Language.Russian)];
                    break;
                default:
                    img.sprite = sprites[(int)(StringConstants.Language.English)];
                    break;
            }
        }

        private void InitRecord()
        {
            int record = PlayerPrefs.GetInt("recordLevel" + _currentLvl);
            if (GameData.score > record)
            {
                PlayerPrefs.SetInt("recordLevel" + _currentLvl, GameData.score);
                recordTxt.text = Texts.GetText(WhatText.NewRecordTxt) + ": " + GameData.score;
            }
            else
            {
                recordTxt.text = Texts.GetText(WhatText.ScoredTxt) + ": " + GameData.score;
            }
        }

        private void InitStars()
        {
            int countStars = GamePlay.countStarsLevel;
            lastStar = 0;
            for (int i = 0; i < countStars; i++)
            {
                stars[lastStar].SetActive(true);
                lastStar++;
            }
        }

        private void HideStars()
        {
            for (int i = 0; i < 3; i++)
            {
                stars[i].SetActive(false);
            }
        }

        public void OnHomeBtnClick()
        {
            if (PlayerPrefs.GetInt("firstWin", 0) == 0 && GameData.numberLoadLevel - 1 == 1)
            {
                Debug.Log("AdSDK Send Event\n\tWIN_1LEVEL_CLICK_MENU");
                AdSDK.SendEvent("WIN_1LEVEL_CLICK_MENU");
                PlayerPrefs.SetInt("firstWin", 1);
            }
            GameData.numberLoadLevel++;
            GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.ButtonToMap, false);
            UnityEngine.SceneManagement.SceneManager.LoadScene("ToMapScene");
        }

        public void OnShareBtnClick()
        {
            if (PlayerPrefs.GetInt("firstWin", 0) == 0 && GameData.numberLoadLevel - 1 == 1)
                {
                    Debug.Log("AdSDK Send Event\n\tWIN_1LEVEL_CLICK_SHARE");
                    AdSDK.SendEvent("WIN_1LEVEL_CLICK_SHARE");
                    PlayerPrefs.SetInt("firstWin", 1);
                }
                 string filename = "temp.png";
                StartCoroutine(DoScreenshot(filename));
                StartCoroutine(SaveAndShare(filename, GameData.numberLoadLevel-1, GameData.score));
                GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.ButtonPush1, false);
        }

        public void OnRestartBtnClick()
        {
            if (PlayerPrefs.GetInt("firstWin", 0) == 0 && GameData.numberLoadLevel - 1 == 1)
            {
                Debug.Log("AdSDK Send Event\n\tWIN_1LEVEL_CLICK_RESTART");
                AdSDK.SendEvent("WIN_1LEVEL_CLICK_RESTART");
                PlayerPrefs.SetInt("firstWin", 1);
            }
            if (LivesManager.Instance.LivesCount < 1)
            {
                PopupsController.Instance.Show(PopupType.NoLives);
            }
            else
            {
                LivesManager.Instance.SpendLife(1);
                GameData.numberLoadLevel--;
                Debug.Log("AdSDK Send Event - REPLAY_LEVEL_" + GameData.numberLoadLevel.ToString("000"));
                AdSDK.SendEvent("REPLAY_LEVEL_" + GameData.numberLoadLevel.ToString("000"));
                UnityEngine.SceneManagement.SceneManager.LoadScene("SplashScreen");
            }
            GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.ButtonReplay, false);

        }

        public void OnPlayBtnClick()
        {
            if (PlayerPrefs.GetInt("firstWin", 0) == 0 && GameData.numberLoadLevel - 1 == 1)
            {
                AdSDK.SendEvent("WIN_1LEVEL_CLICK_NEXT");
                PlayerPrefs.SetInt("firstWin", 1);
            }

                GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.ButtonPlay, false);
                UnityEngine.SceneManagement.SceneManager.LoadScene("ToMapScene");
            
        }

        public IEnumerator DoScreenshot(string fileName)
        {
            Application.CaptureScreenshot(fileName);
            yield return new WaitForEndOfFrame();
        }

        public IEnumerator SaveAndShare(string fileName, int level, int coins)
        {
            yield return new WaitForEndOfFrame();
#if UNITY_ANDROID

            string path = Application.persistentDataPath + "/" + fileName;
            AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
            AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");

            intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
            intentObject.Call<AndroidJavaObject>("setType", "image/*");
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), " Jelly Monster ");
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TITLE"), " Jelly Monster ");
            string shareMessage = string.Format(StringConstants.ShareTextToWin(level, coins) + "\nhttps://play.google.com/store/apps/details?id=jelly.monster.adventure");
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), shareMessage);

            AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");

            AndroidJavaObject fileObject = new AndroidJavaObject("java.io.File", path); // Set Image Path Here

            AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("fromFile", fileObject);

            string uriPath = uriObject.Call<string>("getPath");
            bool fileExist = fileObject.Call<bool>("exists");
            if (fileExist)
                intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uriObject);

            AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
            currentActivity.Call("startActivity", intentObject);
#endif
        }
    }
}
