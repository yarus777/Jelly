using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.MyScripts.Gates;
using Assets.Scripts.MyScripts.Lives;
using UnityEngine;
using UnityEngine.UI;

public class LoadVideo : MonoBehaviour {
    private enum VideoState {
        Loading,
        NoConnection,
        NoVideo,
        Available
    }

    public Text dotsTxt;
    public Text noConnectionTxt;
    public Text noVideoTxt;

    public GameObject spriteVideo;

    public Button videoBtn;

    private Dictionary<VideoState, GameObject> _components;

    private void Awake() {
        _components = new Dictionary<VideoState, GameObject> {
            {VideoState.Loading, dotsTxt.gameObject},
            {VideoState.NoConnection, noConnectionTxt.gameObject},
            {VideoState.NoVideo, noVideoTxt.gameObject},
            {VideoState.Available, spriteVideo.gameObject}
        };
    }

    private void SwitchState(VideoState state) {
        videoBtn.interactable = state == VideoState.Available;
        foreach (var component in _components) {
            component.Value.SetActive(component.Key == state);
        }
    }


    void Start() {
        /*spriteVideo.SetActive(true);
        dotsTxt.gameObject.SetActive(false);
        noVideoTxt.gameObject.SetActive(false);
        noConnectionTxt.gameObject.SetActive(false);*/
    }

    public void OnEnable() {
        SwitchState(VideoState.Available);
    }

    private void PrepareVideo() {
        if (AdSDK.stateVideo != AdSDK.STATE.Ready && AdSDK.stateVideo != AdSDK.STATE.Loading) {
            AdSDK.PreloadVideoAd();
        }
    }

    public void GiveOneForLife() {
#if UNITY_EDITOR
        LivesManager.Instance.AddLife(1);
#endif
#if UNITY_ANDROID
        AdSDK.forLives = true;
        StartCoroutine(StartPreload());
#endif
    }

    public void GiveOneForTimer() {
#if UNITY_EDITOR
        GatesStorage.Instance.CurrentGates.AddTime(TimeSpan.FromMinutes(30));
#endif
#if UNITY_ANDROID
        AdSDK.forTimer = true;
        StartCoroutine(StartPreload());
#endif
    }

    public void GiveOneForMoves() {
        if (!GamePlay.onlyOneMoves) {
            GamePlay.onlyOneMoves = true;

#if UNITY_EDITOR
            GameData.buyManager.BuyLimit(GameData.limit.GetTypeLimit());
#endif
#if UNITY_ANDROID
            AdSDK.forMoves = true;
            StartCoroutine(StartPreload());
#endif
        }
    }

    IEnumerator StartPreload() {
        /*videoBtn.interactable = false;

        spriteVideo.SetActive(false);
        noVideoTxt.gameObject.SetActive(false);
        noConnectionTxt.gameObject.SetActive(false);
        dotsTxt.gameObject.SetActive(true);*/
        SwitchState(VideoState.Loading);

        while (AdSDK.stateVideo == AdSDK.STATE.Loading) {
            Debug.Log("AdSDK.STATE.Loading" + AdSDK.stateVideo);

            var k = Mathf.FloorToInt(Time.time)%4;
            dotsTxt.text = "";
            for (var i = 0; i < k; i++) {
                dotsTxt.text += ".";
            }
            yield return new WaitForSeconds(1f);
        }

        //videoBtn.interactable = true;

        if (AdSDK.stateVideo == AdSDK.STATE.Fail || AdSDK.stateVideo == AdSDK.STATE.Dismiss) {
            if (Application.internetReachability == NetworkReachability.NotReachable) {
                /*spriteVideo.SetActive(false);
                dotsTxt.gameObject.SetActive(false);
                noVideoTxt.gameObject.SetActive(false);
                noConnectionTxt.gameObject.SetActive(true);*/
                SwitchState(VideoState.NoConnection);
            } else {
                Debug.Log("There are no videos");
                /*spriteVideo.SetActive(false);
                dotsTxt.gameObject.SetActive(false);
                noVideoTxt.gameObject.SetActive(true);
                noConnectionTxt.gameObject.SetActive(false);*/
                SwitchState(VideoState.NoVideo);
            }
        } else {
            if (AdSDK.stateVideo == AdSDK.STATE.Ready) {
                Debug.Log("AdSDK.STATE.Ready" + AdSDK.stateVideo);
                AdSDK.ShowVideoAd();
                /*spriteVideo.SetActive(true);
                dotsTxt.gameObject.SetActive(false);
                noVideoTxt.gameObject.SetActive(false);
                noConnectionTxt.gameObject.SetActive(false);*/
                SwitchState(VideoState.Available);
            } else {
                Debug.LogWarning("WTF IN FUCKING CODE?");
            }
        }
        yield return null;
    }
}