using UnityEngine;
using System.Collections;

public class SplashScreen : MonoBehaviour {
	// Use this for initialization
	IEnumerator Start () {
		if(GameData.pool!=null)
		{
			GameData.pool.ResetData ();
		}
		#if UNITY_ANDROID
		AdSDK.SetBannerVisible(false);
        //AdSDK.ShowIntersisialAd();
        //Debug.Log("isFullScreenReady: " + AdSDK.isFullScreenReady);
        //if (!AdSDK.isFullScreenReady)
        //    AdSDK.ShowFullscreen();
		#endif
		AsyncOperation unloadResources = Resources.UnloadUnusedAssets ();
		yield return unloadResources;
		AsyncOperation loadLevel = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync ("GameField");
		yield return loadLevel;
	}

    void OnDestroy()
    {
        AdSDK.SetBannerVisible(true);
    }

}
