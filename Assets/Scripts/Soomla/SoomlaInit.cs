using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Soomla.Store;

public class SoomlaInit : MonoBehaviour {

	void Start () {

        StoreEvents.OnSoomlaStoreInitialized += OnSoomlaStoreInitialized;
        SoomlaStore.Initialize(new MySoomlaStore());
        DontDestroyOnLoad (transform.gameObject);
	}

	public void OnSoomlaStoreInitialized()
	{
		if(PlayerPrefs.GetInt("firstSoomla")==0)
		{
			PlayerPrefs.SetInt("firstSoomla", 1);
            MySoomlaStore.GOLD_COINS.Give(100);
        }
    }

}
