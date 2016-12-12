using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ToMapLoad : MonoBehaviour {

	IEnumerator Start () {
		AsyncOperation unloadResources = Resources.UnloadUnusedAssets ();
		yield return unloadResources;
		//AsyncOperation loadLevel = SceneManager.LoadSceneAsync("Map");
        AsyncOperation loadLevel = SceneManager.LoadSceneAsync("TestMap");
		yield return loadLevel;
	}
    void OnEnable()
    {
        Debug.Log("OnEnable() ToMapLoad");
        AdSDK.SetBannerVisible(false);
    }

}
