using System;
using UnityEngine;
using System.Collections;
using Assets.Scripts.MyScripts.Lives;

public class DebugADSKD : MonoBehaviour {
	public static DebugADSKD inst;
	public static bool downloadtrigger = false;
	void Start()
	{
		inst = this;
		DontDestroyOnLoad (gameObject);
	}
	#if UNITY_ANDROID
	public void ListenForShow () {
		StartCoroutine (Listen ());
	}
	
	IEnumerator Listen () {
		yield return new WaitForSeconds (1f);
		//while (AdSDK.GetInt ("VIDEO_SHOW_OK") == 0) {
        while (AdSDK.stateVideo == AdSDK.STATE.Dismiss) {
			Debug.Log ("Waiting video close...");
			yield return new WaitForSeconds (1f);
		}
		//AdSDK.onDismissVideo ();
        AdSDK.OnVideoFinished();
		PlayerPrefs.SetInt ("VIDEO_SHOW_OK", 0);
	}
	
	
	public void ListenForDownload () {
		if (!downloadtrigger) {
			Debug.Log("Start Listen fo Download. button lock");
			StartCoroutine (ListenDown ());
			downloadtrigger = true;
		}
	}
	IEnumerator ListenDown () {
		yield return new WaitForSeconds (1f);
		while (AdSDK.stateVideo == AdSDK.STATE.Loading) {
			Debug.Log ("Waiting video download...");
			yield return new WaitForSeconds (1f);
		}
		if (AdSDK.stateVideo == AdSDK.STATE.Ready) {
			Debug.Log("Video is Ready. Start Show");
			ListenForShow ();
			downloadtrigger = false;
            AdSDK.ShowVideoAd();
			//AdSDK.ShowOrPreloadVideo ();
			
		}
		if(AdSDK.stateVideo == AdSDK.STATE.Fail || AdSDK.stateVideo == AdSDK.STATE.Idle)
		{
			Debug.Log("Video is loading failed. button unlock");
			downloadtrigger = false;
		}
	}
	public void ShowVideo(){
		Debug.Log ("downloadtrigger " + downloadtrigger);
		if (!downloadtrigger) {
			ListenForDownload ();
            AdSDK.ShowVideoAd();
			//AdSDK.ShowOrPreloadVideo ();
			
		}
	}

    void OnApplicationFocus(bool focusStatus)
    {
        if (!focusStatus)
        {
            StartLivesPushes();
            StartGatesPushes();
        }
        else
        {
            StopPushes();
        }
    }



    private void StartGatesPushes()
    {
        Debug.Log("StartGatesPushes()");
        if (GamePlay.mapLocker == null || GamePlay.mapLocker.activeGate == null)
        {
            return;
        }
        var unlock_time = GamePlay.mapLocker.activeGate.unlockTime;
        var currentTime = DateTime.Now.ToFileTime() / 10000000;
        int unlockDelay = (int)(unlock_time - currentTime);
        var state =
            (Gate.GateStates)
                System.Convert.ToInt32(
                    PlayerPrefs.GetInt("Gate_" + GamePlay.mapLocker.activeGate.numberGate + "_State", 0));
        Debug.Log("stateof gates " + state.ToString() + " unlock_time " + unlockDelay);

        if (state == Gate.GateStates.LockToGame)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
        AndroidJavaClass clazz = new AndroidJavaClass("com.justmoby.push.demo.PushManager");
        clazz.CallStatic("ShowPushGates", AdSDK.instance.getCurrentActivity(), unlockDelay);
#endif
        }
    }

    private void StartLivesPushes()
    {
        Debug.Log("StartLivesPushes()");
        if (LivesManager.Instance.LivesCount < 10)
        {
            //long saveTime = System.Convert.ToInt64(PlayerPrefs.GetString("lastTimeLife"));
            //long nowTime = System.DateTime.Now.ToFileTime() / 10000000;
            //long time = GameData.updateLifeTime - (nowTime - saveTime) +
            //            (9 - GamePlay.currentCountLife) * GameData.updateLifeTime;


            double time = (9 - LivesManager.Instance.LivesCount)*1200 + LivesManager.Instance.TimeLeftToRefill.TotalSeconds;

            Debug.Log("OnAppQuit\n Current Lifes: " + LivesManager.Instance.LivesCount + "\n Time To Full LIfes: " + time);
#if UNITY_ANDROID && !UNITY_EDITOR
            AndroidJavaClass clazz = new AndroidJavaClass("com.justmoby.push.demo.PushManager");
            clazz.CallStatic("ShowPushLifes", AdSDK.instance.getCurrentActivity(), System.Convert.ToInt32(time));
#endif
        }
    }

    private void StopPushes()
    {
        //Debug.Log("StopPushes()");
#if UNITY_ANDROID && !UNITY_EDITOR
            AndroidJavaClass clazz = new AndroidJavaClass("com.justmoby.push.demo.PushManager");
            clazz.CallStatic("StopAllPushes", AdSDK.instance.getCurrentActivity());
#endif
    }

#endif
}