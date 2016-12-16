using System;
using System.Collections;
using Assets.Scripts.MyScripts.Gates;
using Assets.Scripts.MyScripts.Lives;
using UnityEngine;

#if UNITY_IOS
using GoogleMobileAds.Api;
#endif

public class AdSDK : MonoBehaviour {
    public static AdSDK instance;

    public enum STATE {
        Idle,
        Loading,
        Ready,
        Fail,
        Dismiss
    }

    public static STATE stateVideo = STATE.Idle;
    public static STATE stateInterstitial = STATE.Idle;
    private BannerAlignment alignment = BannerAlignment.BOTTOM;
    public static int bannerHeight;
    private static bool bannerActive;

    public enum BannerAlignment {
        TOP = 0,
        BOTTOM = 1
    }

    public int conversions {
        get { return PlayerPrefs.GetInt("conversions", 0); }
        set { PlayerPrefs.SetInt("conversions", value); }
    }

#if UNITY_ANDROID

#if AMAZON
    private const string publisherId = "Amazon";
    private const string market = "amazon";
    private const string flurryKey = "RT4FRSDPMHQH42ZP7T56";
#endif

    public static string packagePath;


    public AndroidJavaObject getCurrentActivity() {
#if !UNITY_EDITOR
		AndroidJavaClass ajc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject activity = ajc.GetStatic<AndroidJavaObject>("currentActivity");
		return activity;
#else
        return null;
#endif
    }

    public static void SendConversion() {
        Debug.Log("SendConversion");
#if !UNITY_EDITOR
#if UNITY_ANDROID
		AndroidJavaClass clazz = new AndroidJavaClass(packagePath+".SDKProxy");
		clazz.CallStatic("SendConversion", instance.getCurrentActivity());
#elif UNITY_IOS
#endif
#endif
    }

#elif UNITY_IOS

    private const string adUnitMma = "ca-app-pub-8418644287756582/2766069356";
    private const string adUnitInterstitial = "ca-app-pub-8418644287756582/6626996153";
	private const string flurryKey = "VZX5Y2Y8QDRM3SCSNTXR";

    private InterstitialAd interstitial;

    private void Vungle_onAdFinishedEvent(AdFinishedEventArgs obj)
    {
        Debug.Log("Video_OnAdClosed");
        Time.timeScale = 1;
        OnVideoFinished();
    }

    private void Vungle_adPlayableEvent(bool obj)
    {
        if (obj)
        {
            stateVideo = STATE.Ready;
        }
        else {
            stateVideo = STATE.Fail;
        }
    }

    private void VungleManager_OnAdStartEvent()
    {
        stateVideo = STATE.Idle;
    }

    private IEnumerator waitLoadingVungle()
    {
        Debug.Log("waiting");
        yield return new WaitForSeconds(10);
        if (stateVideo == STATE.Loading)
        {
            stateVideo = STATE.Fail;
        }
    }

    private void startWait()
    {
        Debug.Log("startWait");
        StartCoroutine(waitLoadingVungle());
    }


#endif

    public static bool forMoves;
    public static bool forLives;
    public static bool forTimer;
    public static bool forCoins;

    private bool noADS {
        get { return PlayerPrefs.GetInt("NOADS", 0) == 1; }
    }


    private void Init(bool debug = false) {
#if UNITY_ANDROID
        //Debug.Log("Application.bundleIdentifier: " + Application.bundleIdentifier);
        packagePath = "jelly.monster.adventure";
        CheckConversion();

#elif UNITY_IOS
        stateVideo = STATE.Loading;
        Vungle.init("", "576d37478f5548e054000058");
        Vungle.onAdStartedEvent += VungleManager_OnAdStartEvent;
        Vungle.adPlayableEvent += Vungle_adPlayableEvent;
        Vungle.onAdFinishedEvent += Vungle_onAdFinishedEvent;
        if (!noADS)
        {
            interstitial = new InterstitialAd(adUnitInterstitial);
        }
#if UNITY_EDITOR
        stateVideo = STATE.Idle;
#endif
#endif
    }

    public static void PreloadVideoAd() {
#if UNITY_EDITOR
        stateVideo = STATE.Ready;
#elif UNITY_ANDROID
		AndroidJavaClass clazz = new AndroidJavaClass(packagePath+".SDKProxy");
		clazz.CallStatic("PreloadVideoAd", instance.getCurrentActivity());
#elif UNITY_IOS
		stateVideo = State.Loading;
		instance.startWait();

#endif
    }

    public static void ShowVideoAd() {
#if UNITY_EDITOR

#elif UNITY_ANDROID
		AndroidJavaClass clazz = new AndroidJavaClass(packagePath+".SDKProxy");
		clazz.CallStatic("ShowVideoAd", instance.getCurrentActivity());
#elif UNITY_IOS
		Time.timeScale = 0;
		Debug.Log("ShowVideoAd ()");
        System.Collections.Generic.Dictionary<string,object> options = new System.Collections.Generic.Dictionary<string, object> ();
        options["orientation"] = VungleAdOrientation.Landscape;
        options["incentivized"] = true;
        Vungle.playAdWithOptions (options);
		stateVideo = STATE.Loading;
#endif
    }

    public static void PreloadIntersisialAd() {
        if (instance.noADS) {
        }
#if !UNITY_EDITOR
#if UNITY_ANDROID
        AndroidJavaClass clazz = new AndroidJavaClass(packagePath+".SDKProxy");
		clazz.CallStatic("PreloadIntersisialAd", instance.getCurrentActivity());
#elif UNITY_IOS
        if (!instance.interstitial.IsLoaded())
            instance.interstitial.LoadAd(new AdRequest.Builder().Build());
#endif
#endif
    }

    public static void ShowIntersisialAd() {
        if (instance.noADS) {
        }
#if !UNITY_EDITOR
#if UNITY_ANDROID
        AndroidJavaClass clazz = new AndroidJavaClass(packagePath+".SDKProxy");
		clazz.CallStatic("ShowIntersisialAd", instance.getCurrentActivity());
#elif UNITY_IOS
        if (instance.interstitial.IsLoaded ()) {
            instance.interstitial.Show ();
            instance.interstitial.LoadAd (new AdRequest.Builder ().Build ());
        }
#endif
#endif
    }

    public static void SendEvent(string someEvent) {
#if !UNITY_EDITOR
#if UNITY_ANDROID
        AndroidJavaClass clazz = new AndroidJavaClass(packagePath+".SDKProxy");
		clazz.CallStatic("SendEvent", instance.getCurrentActivity(), someEvent);
#elif UNITY_IOS
		FlurryAgent.Instance.logEvent (someEvent);
#endif
#else
        Debug.LogWarning("Event: " + someEvent);
#endif
    }

    public void VideoStateUpdate(string state) {
        var videoState = 0;
        if (Int32.TryParse(state, out videoState)) {
            switch (videoState) {
                case 0:
                    stateVideo = STATE.Idle;
                    break;
                case 1:
                    stateVideo = STATE.Loading;
                    break;
                case 2:
                    stateVideo = STATE.Ready;
                    break;
                case 3:
                    stateVideo = STATE.Fail;
                    break;
                case 4:
                    stateVideo = STATE.Dismiss;
                    OnVideoFinished();
                    break;
                default:
                    stateVideo = STATE.Idle;
                    break;
            }
            Debug.Log("AdSDK\nvideoState: " + stateVideo);
        }
    }

    public void InterstitialStateUpdate(string state) {
        var interstitialState = 0;
        if (Int32.TryParse(state, out interstitialState)) {
            switch (interstitialState) {
                case 0:
                    stateInterstitial = STATE.Idle;
                    break;
                case 1:
                    stateInterstitial = STATE.Loading;
                    break;
                case 2:
                    stateInterstitial = STATE.Ready;
                    break;
                case 3:
                    stateInterstitial = STATE.Fail;
                    break;
                case 4:
                    stateInterstitial = STATE.Dismiss;
                    break;
                default:
                    stateInterstitial = STATE.Idle;
                    break;
            }
            Debug.Log("AdSDK\ninterstitialState: " + stateInterstitial);
        }
    }


    public static void OnDismissVideo(string msg) {
        Debug.Log("AdSDK\nOnDismissVideo:" + msg);

        OnVideoFinished();
    }


    public static void OnVideoFinished() {
        if (forMoves) {
            Debug.Log("Add Moves");
            forMoves = false;
            GameData.buyManager.BuyLimit(GameData.limit.GetTypeLimit());
        }
        if (forLives) {
            Debug.Log("Add Life");
            forLives = false;
            LivesManager.Instance.AddLife(1);
        }
        if (forTimer) {
            Debug.Log("Add Timer");
            forTimer = false;
            GatesStorage.Instance.CurrentGates.AddTime(TimeSpan.FromMinutes(30));
        }
        if (forCoins) {
            forCoins = false;
        }
    }

    private static void CreateBanner(BannerAlignment alignment) {
        Debug.Log("CreateBanner");
        bannerActive = true;
#if !UNITY_EDITOR
#if UNITY_ANDROID
                AndroidJavaClass clazz = new AndroidJavaClass(packagePath+".SDKProxy");
		        clazz.CallStatic("CreateBanner", instance.getCurrentActivity(), (int) alignment);
#endif
#endif
    }

    private static void DestroyBanner() {
        Debug.Log("Destroy Banner");
        bannerActive = false;
#if !UNITY_EDITOR
#if UNITY_ANDROID
                AndroidJavaClass clazz = new AndroidJavaClass(packagePath+".SDKProxy");
		        clazz.CallStatic("DestroyBanner", instance.getCurrentActivity());
#endif
#endif
    }

    public static void SetBannerVisible(bool isVisible, BannerAlignment alignment = BannerAlignment.BOTTOM) {
        Debug.Log("Banner Visibility");
        if (isVisible && !bannerActive) {
            CreateBanner(alignment);
            Debug.Log("isVisible && !bannerActive");
        }
        if (!isVisible && bannerActive) {
            DestroyBanner();
            Debug.Log("!isVisible && bannerActive");
        }
    }

    public static void ShowAppWall() {
#if !UNITY_EDITOR
#if UNITY_ANDROID
        AndroidJavaClass clazz = new AndroidJavaClass(packagePath+".SDKProxy");
		clazz.CallStatic("ShowAppWall", instance.getCurrentActivity());
        Debug.Log("AppWall");
#endif
#endif
    }

    public static void ShowIncentAppWall() {
#if !UNITY_EDITOR
#if UNITY_ANDROID
        AndroidJavaClass clazz = new AndroidJavaClass(packagePath+".SDKProxy");
		clazz.CallStatic("ShowIncentAppwall", instance.getCurrentActivity());
        Debug.Log("ShowIncentAppwall");
#endif
#endif
    }

    public static int GetBalance() {
        Debug.Log("Get Balance");
#if !UNITY_EDITOR
	    AndroidJavaObject jo = new AndroidJavaObject(packagePath+".SDKProxy");
        int balance = jo.GetStatic<int>("balance");
        return balance;
#else
        return 0;
#endif
    }

    public static void Finish() {
#if !UNITY_EDITOR
#if UNITY_ANDROID
		AndroidJavaClass clazz = new AndroidJavaClass(packagePath+".SDKProxy");
		clazz.CallStatic("finish", instance.getCurrentActivity());
#endif
#endif
    }


    void Awake() {
        if (instance) {
            Destroy(gameObject);
        } else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void CheckConversion() {
        conversions++;
        if (conversions%3 == 0) {
            SendConversion();
        }
    }

    IEnumerator Start() {
        Init();
#if UNITY_ANDROID
        if (GamePlay.maxCompleteLevel >= 3) {
            SetBannerVisible(true);
        }

#elif UNITY_IOS
#if !UNITY_EDITOR
		FlurryAgent.Instance.onStartSession (flurryKey);
#endif

#endif
        while (true) {
#if !UNITY_EDITOR
#if UNITY_ANDROID
                if (AdSDK.stateInterstitial != STATE.Ready)
                {
                    PreloadIntersisialAd();
                }

                if (AdSDK.stateVideo != STATE.Ready)
                {
                    PreloadVideoAd();
                }
#elif UNITY_IOS
                if (!interstitial.IsLoaded ()) 
                {
                    interstitial.LoadAd (new AdRequest.Builder ().Build ());
                }
#endif
#endif
            yield return new WaitForSeconds(30);
        }
    }


    void OnApplicationQuit() {
#if UNITY_IOS && !UNITY_EDITOR
		FlurryAgent.Instance.onEndSession ();
#endif
    }
}

/*public static class AdSDK {
	#if UNITY_ANDROID
	
	public enum GenderType {
		UNKNOWN, MALE, FEMALE
	}
	
	//	private static AndroidJavaObject banner = null;
	//	private static AndroidJavaObject debugBanner = null;
	
	public static string publisherId = "adeco";
	public static string appKey = "barleybreak" ;
	public static string affId = "adeco" ;
	
	public static string market = "4shared.com";
	
	public static string placementRKey = "r_game";
	public static string placementVRKey = "vr_game";
	public static string placementIRKey = "ir_game";
	public static string placementFKey = "f_game";
	
	public static string flurryKey = "";
	
	public enum BannerAlignment { TOP = 0, BOTTOM = 1 }
	public static BannerAlignment alignment;
	
	public static int bannerWidth;
	public static int bannerHeight;
	private static bool bannerActive = false;
	
    public static bool isFullScreenReady
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        get {
            AndroidJavaClass clazz = new AndroidJavaClass("com.inappertising.ads.UnityPlugin");
            return clazz.GetStatic<bool>("isFullScreenReady");
        }
#else
        get { return false; }
#endif
    }
#if !UNITY_EDITOR
	private static bool isFirstLaunch = true;
#endif
    private static void Init(bool debug) {
		Debug.Log ("Init SDK");
//		Debug.Log ("MYDEBUG: External storage path = " + PlayerPrefs.GetString ("EXTERNAL_STORAGE_PATH"));
#if !UNITY_EDITOR
		AndroidJavaClass clazz = new AndroidJavaClass("com.inappertising.ads.UnityPlugin");
		clazz.CallStatic("initialize", getCurrentActivity(), debug, flurryKey);
#endif
	}
	
	public static void Finish () {
		Debug.Log ("Finish");
#if !UNITY_EDITOR
		AndroidJavaClass clazz = new AndroidJavaClass("jelly.monster.adventure.SDKProxy");
		clazz.CallStatic("finish", getCurrentActivity());
#endif
	}

//	static float TIME = 0;
	public static void ShowOrPreloadVideo () {
		Debug.Log ("ShowOrPreloadVideo start");
#if !UNITY_EDITOR
		AndroidJavaClass clazz = new AndroidJavaClass("jelly.monster.adventure.SDKProxy");
		clazz.CallStatic("ShowOrPreload", getCurrentActivity());
#endif
	}
	
	public static void PreloadVideo () {
		Debug.Log ("PreloadVideo start");
#if !UNITY_EDITOR
		AndroidJavaClass clazz = new AndroidJavaClass("jelly.monster.adventure.SDKProxy");
		clazz.CallStatic("Preload", getCurrentActivity());
#endif
	}
	
	public static void ShowFullscreen() {
		Debug.Log ("ShowFullscreen start");
#if !UNITY_EDITOR
		AndroidJavaClass clazz = new AndroidJavaClass("com.inappertising.ads.UnityPlugin");
		clazz.CallStatic("showFullScreenOrPreload", getCurrentActivity());
#endif
	}

	public static void StartDebug () {
#if !UNITY_EDITOR
		AndroidJavaObject param = BuildBannerParams(placementFKey);  
		AndroidJavaClass clazz = new AndroidJavaClass("com.inappertising.ads.UnityPlugin");
		clazz.CallStatic("startDebug", param, getCurrentActivity());
#endif
	}
	
	
	public static void StopDebug () {
#if !UNITY_EDITOR
		AndroidJavaClass clazz = new AndroidJavaClass("com.inappertising.ads.UnityPlugin");
		clazz.CallStatic("stopDebug", getCurrentActivity());    
#endif
	}
	
	public static void DestroyBanner() {
		bannerActive = false;
#if !UNITY_EDITOR
		AndroidJavaClass clazz = new AndroidJavaClass("com.inappertising.ads.UnityPlugin");
		clazz.CallStatic("destroy", getCurrentActivity());
#endif
	}
	
	
	private static AndroidJavaObject BuildBannerParams(string placement) {
#if !UNITY_EDITOR
		AndroidJavaObject obj = GetParams();
		obj.Call<AndroidJavaObject>("setPlacementKey", placement);
		AndroidJavaObject obj2 = new AndroidJavaObject ("com.inappertising.ads.ad.AdSize", bannerWidth, bannerHeight);
		obj.Call<AndroidJavaObject> ("setSize", obj2);
		AndroidJavaObject param = obj.Call<AndroidJavaObject>("build");
		return param;
#else
		return null;
#endif
	}
	
	
	private static AndroidJavaObject GetParams() {
#if !UNITY_EDITOR
		AndroidJavaObject obj = new AndroidJavaObject("com.inappertising.ads.ad.AdParametersBuilder");
		obj.Call<AndroidJavaObject>("setPublisherId", publisherId);
		obj.Call<AndroidJavaObject>("setAppKey", appKey);
		
		obj.Call<AndroidJavaObject>("setMarket", market);
		
		obj.Call<AndroidJavaObject>("setAffId", affId);
		return obj;
#else
		return null;
#endif
	}
	
	public static void CreateBanner() {
		bannerActive = true;
#if !UNITY_EDITOR
		AndroidJavaObject param = BuildBannerParams(placementRKey);
		AndroidJavaClass clazz = new AndroidJavaClass("com.inappertising.ads.UnityPlugin");
		clazz.CallStatic("addBanner", param, (int) alignment, getCurrentActivity());
#endif
	}
	
	public static void SetBannerVisible(bool visible, bool top = false, int width = 320, int height = 50) {
//		if (!on) return;
		Debug.Log ("AdSDK Banner - Set VIsible : " + visible);
		if (visible) {
			if (bannerActive) {
				if (top != (alignment == BannerAlignment.TOP)) {
					DestroyBanner ();
					bannerWidth = width;
					bannerHeight = height;
					if (top) {
						alignment = BannerAlignment.TOP;
					} else {
						alignment = BannerAlignment.BOTTOM;
					}
					CreateBanner ();
				}
			} else {
				bannerWidth = width;
				bannerHeight = height;
				if (top) {
					alignment = BannerAlignment.TOP;
				} else {
					alignment = BannerAlignment.BOTTOM;
				}
				CreateBanner ();
			}
		}
		if (!visible && bannerActive) {
			DestroyBanner ();
		}
	}
	
	public static AndroidJavaObject getCurrentActivity() {
#if !UNITY_EDITOR
		AndroidJavaClass ajc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject activity = ajc.GetStatic<AndroidJavaObject>("currentActivity");
		return activity;
#else
		return null;
#endif
	}
	
	// Analytics methods
	
	public static void StartFlurrySession() {
#if !UNITY_EDITOR
		AndroidJavaClass clazz = new AndroidJavaClass("com.inappertising.ads.UnityPlugin");
		AndroidJavaObject param = BuildBannerParams(placementVRKey);
		clazz.CallStatic("onStart", getCurrentActivity(), flurryKey, param);
#endif
	}
	
	public static void StopFlurrySession() {
#if !UNITY_EDITOR
		AndroidJavaClass clazz = new AndroidJavaClass("com.inappertising.ads.UnityPlugin");
		clazz.CallStatic("onStop", getCurrentActivity());
#endif
	}
	
	private static void SendDownload() {
#if !UNITY_EDITOR
		AndroidJavaClass clazz = new AndroidJavaClass("com.inappertising.ads.UnityPlugin");
		clazz.CallStatic("sendDownload", getCurrentActivity(), publisherId, affId, appKey);
#endif
	}
	
	public static void SendConversion() {
#if !UNITY_EDITOR
		AndroidJavaClass clazz = new AndroidJavaClass("com.inappertising.ads.UnityPlugin");
		clazz.CallStatic("sendConversion", getCurrentActivity(), publisherId, affId, appKey);
#endif
	}
	
	public static void SendEvent(string someEvent) {
#if !UNITY_EDITOR
		AndroidJavaClass clazz = new AndroidJavaClass("com.inappertising.ads.UnityPlugin");
		clazz.CallStatic("sendCustomEvent", getCurrentActivity(), publisherId, affId, appKey, someEvent);
#endif
	}
	
	public static void listenForInstall(string packageName, string eventName) {
#if !UNITY_EDITOR
		AndroidJavaClass clazz = new AndroidJavaClass("com.inappertising.ads.UnityPlugin");
		clazz.CallStatic("listenForInstall", getCurrentActivity(), packageName, eventName, publisherId, affId, appKey);
#endif
	}
	
	public static void StartSDK(bool debug = true) {
#if !UNITY_EDITOR
		if (isFirstLaunch) {
			Init (debug);
			StartFlurrySession();
			if(!PlayerPrefs.HasKey("startApp"))
			{
				SendDownload();
				PlayerPrefs.SetInt("startApp", 1);
			}
			isFirstLaunch = false;
		}
#endif
	}
	
	public enum State {
		LoadingFailed,
		Loading,
		Ready,
		Idle
	}

    public static int GetInt(string key, int defaultValue = 0)
    {
#if !UNITY_EDITOR && UNITY_ANDROID
		AndroidJavaClass obj = new AndroidJavaClass("jelly.monster.adventure.SDKProxy");
        return obj.CallStatic<int>("GetPreference", getCurrentActivity(), key, defaultValue);
#else
        return PlayerPrefs.GetInt (key,defaultValue);
#endif
    }

    public static void SetInt(string key, int defaultValue = 0)
    {
#if !UNITY_EDITOR && UNITY_ANDROID
		AndroidJavaClass obj = new AndroidJavaClass("jelly.monster.adventure.SDKProxy");
        obj.CallStatic("SetPreference", getCurrentActivity(), key, defaultValue);
#else
        PlayerPrefs.SetInt(key, defaultValue);
#endif
    } 

    public static State stateVideo {
		get {
			int val = GetInt ("VIDEO_STATE");
            Debug.Log("VIDEO_STATE: " + val);
			if (val == 1) return State.Loading;
			if (val == 2) return State.LoadingFailed;
			if (val == 3) return State.Ready;
			return State.Idle;
		}
	}
	public static int count = 1;
	public static bool forWatch = false;
	public static bool forDouble = false;
    public static bool forTimer = false;
    public static bool forCoins = false;

	public static void onDismissVideo () {
		if (forDouble) {
			forDouble = false;
			InAppManager.CompleteMoves();
		}
		if (forWatch) {
			forWatch = false;
			InAppManager.CompleteLife();
		}
        if (forTimer)
        {
            forTimer = false;
            InAppManager.CompleteTimer();
        }
        if(forCoins)
        {
            forCoins = false;
            InAppManager.CompleteFreeCoinsVideo();
        }
        Debug.Log("ShowVideo onDismiss");
	}
#endif
    }*/