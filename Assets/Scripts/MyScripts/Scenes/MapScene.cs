﻿using System.Collections;
using Assets.Scripts;
using Assets.Scripts.MyScripts.Lives;
using Assets.Scripts.MyScripts.Popups;
using UnityEngine;
using UnityEngine.UI;

public class MapScene : MonoBehaviour {
    [SerializeField]
    private Text livesTxt;

    [SerializeField]
    private Text starsTxt;

    [SerializeField]
    private Text timerTxt;

    [SerializeField]
    private GameObject achivementsBtn;

    public void OnSettingsBtnClick() {
        PopupsController.Instance.Show(PopupType.Settings);
    }

    public void OnShareBtnClick() {
        var filename = "temp.png";
        StartCoroutine(DoScreenshot(filename));
        StartCoroutine(SaveAndShare(filename));
    }

    public void OnAchivementsBtnClick() {
        //PopupsController.Instance.Show(PopupType.AchivementsPopup);
        GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.WindowOpen, false);
    }

    public void OnBtnUp() {
        GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.ButtonPush2, false);
    }

    public void OnBtnDown() {
        GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.ButtonPush1, false);
    }

    void Awake() {
        livesTxt.text = LivesManager.Instance.LivesCount + "/10";
        InitStars();
        AchivementsBtnCheck();

        if (LivesManager.Instance.LivesCount < 10) {
            StartCoroutine(UpdateTimer());
        }

        LivesManager.Instance.LivesCountChanged += OnLivesCountChanged;
        JMAchivementSettings.jmAchivementSettings.OnAddHonor += OnAddHonor;
    }

    void Start() {
        RateUsCheck();
        if (GamePlay.maxCompleteLevel >= 3) {
            AdSDK.SetBannerVisible(true);
        }
    }

    private void OnLivesCountChanged(int count) {
        livesTxt.text = count + "/10";
    }

    IEnumerator UpdateTimer() {
        while (true) {
            var timeLeft = LivesManager.Instance.TimeLeftToRefill;
            timerTxt.text = string.Format("{0:00}:{1:00}", timeLeft.Minutes, timeLeft.Seconds);
            yield return new WaitForSeconds(1);
        }
    }

    private void AchivementsBtnCheck() {
        if (GamePlay.maxCompleteLevel >= 6) {
            achivementsBtn.SetActive(true);
        } else {
            achivementsBtn.SetActive(false);
        }
    }

    private void RateUsCheck() {
        Debug.Log("noRateUs " + PlayerPrefs.GetInt("noRateUs") + " countRateUs " + PlayerPrefs.GetInt("countRateUs"));
        if (PlayerPrefs.GetInt("noRateUs") == 0) {
            if (PlayerPrefs.GetInt("countRateUs") >= 6) {
                Debug.Log(">=6");
                PopupsController.Instance.Show(PopupType.RateUs);
#if UNITY_ANDROID
                AdSDK.SendEvent("RATE_US_SHOW");
#endif
            }
        }
    }

    private void OnAddHonor(EHonorType type, float count) {
        switch (type) {
            case EHonorType.Money:


                break;
            case EHonorType.Energy:
                Debug.Log("EHonorType.Energy");
                LivesManager.Instance.AddLife((int) count);
                break;
        }
    }


    public IEnumerator DoScreenshot(string fileName) {
        Application.CaptureScreenshot(fileName);
        yield return new WaitForEndOfFrame();
    }


    public IEnumerator SaveAndShare(string fileName) {
        yield return new WaitForEndOfFrame();
#if UNITY_ANDROID

        var path = Application.persistentDataPath + "/" + fileName;
        var intentClass = new AndroidJavaClass("android.content.Intent");
        var intentObject = new AndroidJavaObject("android.content.Intent");

        intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
        intentObject.Call<AndroidJavaObject>("setType", "text/*");
        intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), "Jelly Monster");
        intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TITLE"), "Jelly Monster");
        var shareMessage =
            string.Format(Texts.GetText(WhatText.ShareTxt) +
                          "\nhttps://play.google.com/store/apps/details?id=jelly.monster.adventure");
        intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), shareMessage);

        var uriClass = new AndroidJavaClass("android.net.Uri");
        var unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        var currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
        currentActivity.Call("startActivity", intentObject);
#endif
    }

    private void InitStars() {
        var sumStars = 0;
        for (var i = 1; i <= GameData.allLevels; i++) {
            var level = "starsLevel" + i;
            sumStars += PlayerPrefs.GetInt(level);
        }

        var countStars = sumStars + "/" + (GameData.allLevels*3);

        starsTxt.text = countStars;
    }

    void OnDestroy() {
        LivesManager.Instance.LivesCountChanged -= OnLivesCountChanged;
        JMAchivementSettings.jmAchivementSettings.OnAddHonor -= OnAddHonor;
    }
}