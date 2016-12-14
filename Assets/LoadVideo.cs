using System;
using UnityEngine;
using System.Collections;
using Assets.Scripts.MyScripts.Gates;
using Assets.Scripts.MyScripts.Lives;
using UnityEngine.UI;

public class LoadVideo : MonoBehaviour {
	public Text dotsTxt;
	public Text noConnectionTxt;
	public Text noVideoTxt;

	public GameObject spriteVideo;

	public Button videoBtn;


	void Start () {      
        spriteVideo.SetActive(true);
        dotsTxt.gameObject.SetActive(false);
        noVideoTxt.gameObject.SetActive(false);
        noConnectionTxt.gameObject.SetActive(false);
	}

    private void PrepareVideo()
    {
        if (AdSDK.stateVideo != AdSDK.STATE.Ready && AdSDK.stateVideo != AdSDK.STATE.Loading)
        {
            AdSDK.PreloadVideoAd();
        }
    }
	
	public void GiveOneForLife () {
#if UNITY_EDITOR
        LivesManager.Instance.AddLife(1);
#endif
#if UNITY_ANDROID
        AdSDK.forLives = true;
        StartCoroutine(StartPreload());
#endif        
	}

    public void GiveOneForTimer()
    {
#if UNITY_EDITOR
        GatesStorage.Instance.CurrentGates.AddTime(TimeSpan.FromMinutes(30));
#endif
#if UNITY_ANDROID
        AdSDK.forTimer = true;
        StartCoroutine(StartPreload());
#endif
    }

    public void GiveOneForMoves()
    {
#if UNITY_EDITOR
        
#endif
#if UNITY_ANDROID
        AdSDK.forMoves = true;
        StartCoroutine(StartPreload());
#endif
    }
	
	IEnumerator StartPreload () {
        videoBtn.interactable= false;

		spriteVideo.SetActive(false);
        noVideoTxt.gameObject.SetActive(false);
        noConnectionTxt.gameObject.SetActive(false);
        dotsTxt.gameObject.SetActive(true);

		while (AdSDK.stateVideo == AdSDK.STATE.Loading) {
            Debug.Log("AdSDK.STATE.Loading" + AdSDK.stateVideo);

			int k = Mathf.FloorToInt (Time.time) % 4;
            dotsTxt.text = "";
			for (int i = 0; i < k; i++) {
                dotsTxt.text += ".";
			}
			yield return new WaitForSeconds (1f);
		}

		videoBtn.interactable = true;

		if (AdSDK.stateVideo == AdSDK.STATE.Fail || AdSDK.stateVideo == AdSDK.STATE.Dismiss) {
			if (Application.internetReachability == NetworkReachability.NotReachable) {
				spriteVideo.SetActive(false);
                dotsTxt.gameObject.SetActive(false);
                noVideoTxt.gameObject.SetActive(false);
                noConnectionTxt.gameObject.SetActive(true);
			} else {
                Debug.Log("There are no videos");
				spriteVideo.SetActive(false);
                dotsTxt.gameObject.SetActive(false);
                noVideoTxt.gameObject.SetActive(true);
                noConnectionTxt.gameObject.SetActive(false);
			}
		} 
        else 
        {
			if (AdSDK.stateVideo == AdSDK.STATE.Ready) {
                    Debug.Log("AdSDK.STATE.Ready" + AdSDK.stateVideo);
                    AdSDK.ShowVideoAd();
					spriteVideo.SetActive(true);
                    dotsTxt.gameObject.SetActive(false);
                    noVideoTxt.gameObject.SetActive(false);
                    noConnectionTxt.gameObject.SetActive(false);
			} else {
                Debug.LogWarning ("WTF IN FUCKING CODE?");
			}
		}
		yield return null;
	}
}
