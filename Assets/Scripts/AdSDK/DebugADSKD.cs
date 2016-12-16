using System.Collections;
using Assets.Scripts.MyScripts.Gates;
using Assets.Scripts.MyScripts.Lives;
using UnityEngine;

public class DebugADSKD : MonoBehaviour {
    public static DebugADSKD inst;
    public static bool downloadtrigger;

    void Start() {
        inst = this;
        DontDestroyOnLoad(gameObject);
    }

#if UNITY_ANDROID
    public void ListenForShow() {
        StartCoroutine(Listen());
    }

    IEnumerator Listen() {
        yield return new WaitForSeconds(1f);
        //while (AdSDK.GetInt ("VIDEO_SHOW_OK") == 0) {
        while (AdSDK.stateVideo == AdSDK.STATE.Dismiss) {
            Debug.Log("Waiting video close...");
            yield return new WaitForSeconds(1f);
        }
        //AdSDK.onDismissVideo ();
        AdSDK.OnVideoFinished();
        PlayerPrefs.SetInt("VIDEO_SHOW_OK", 0);
    }


    public void ListenForDownload() {
        if (!downloadtrigger) {
            Debug.Log("Start Listen fo Download. button lock");
            StartCoroutine(ListenDown());
            downloadtrigger = true;
        }
    }

    IEnumerator ListenDown() {
        yield return new WaitForSeconds(1f);
        while (AdSDK.stateVideo == AdSDK.STATE.Loading) {
            Debug.Log("Waiting video download...");
            yield return new WaitForSeconds(1f);
        }
        if (AdSDK.stateVideo == AdSDK.STATE.Ready) {
            Debug.Log("Video is Ready. Start Show");
            ListenForShow();
            downloadtrigger = false;
            AdSDK.ShowVideoAd();
            //AdSDK.ShowOrPreloadVideo ();
        }
        if (AdSDK.stateVideo == AdSDK.STATE.Fail || AdSDK.stateVideo == AdSDK.STATE.Idle) {
            Debug.Log("Video is loading failed. button unlock");
            downloadtrigger = false;
        }
    }

    public void ShowVideo() {
        Debug.Log("downloadtrigger " + downloadtrigger);
        if (!downloadtrigger) {
            ListenForDownload();
            AdSDK.ShowVideoAd();
            //AdSDK.ShowOrPreloadVideo ();
        }
    }

    void OnApplicationFocus(bool focusStatus) {
        if (!focusStatus) {
            StartLivesPushes();
            StartGatesPushes();
        } else {
            StopPushes();
        }
    }


    private void StartGatesPushes() {
        var currentGates = GatesStorage.Instance.CurrentGates;
        if (currentGates == null) {
            return;
        }
        var unlockDelay = currentGates.TimeLeft.TotalSeconds;
#if UNITY_ANDROID && !UNITY_EDITOR
        AndroidJavaClass clazz = new AndroidJavaClass("com.justmoby.push.demo.PushManager");
        clazz.CallStatic("ShowPushGates", AdSDK.instance.getCurrentActivity(), unlockDelay);
#endif
    }

    private void StartLivesPushes() {
        if (LivesManager.Instance.LivesCount < 10) {
            var time = (9 - LivesManager.Instance.LivesCount)*1200 + LivesManager.Instance.TimeLeftToRefill.TotalSeconds;

            Debug.Log("OnAppQuit\n Current Lifes: " + LivesManager.Instance.LivesCount + "\n Time To Full LIfes: " +
                      time);
#if UNITY_ANDROID && !UNITY_EDITOR
            AndroidJavaClass clazz = new AndroidJavaClass("com.justmoby.push.demo.PushManager");
            clazz.CallStatic("ShowPushLifes", AdSDK.instance.getCurrentActivity(), System.Convert.ToInt32(time));
#endif
        }
    }

    private void StopPushes() {
        //Debug.Log("StopPushes()");
#if UNITY_ANDROID && !UNITY_EDITOR
            AndroidJavaClass clazz = new AndroidJavaClass("com.justmoby.push.demo.PushManager");
            clazz.CallStatic("StopAllPushes", AdSDK.instance.getCurrentActivity());
#endif
    }

#endif
}