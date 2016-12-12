using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LoadBackground : CacheTransform {
	public bool statePosition = false;
//	public bool winLoseScene = false;
	// Use this for initialization
	void Start () {
		GameObject obj = null;
		if(GameData.numberLoadLevel<21)
		{
			obj = Instantiate(Resources.Load<GameObject>("Prefabs/Backgrounds/Back01"))as GameObject;
            //obj.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
		}
		else if(GameData.numberLoadLevel>=21&&GameData.numberLoadLevel<41)
		{
			obj = Instantiate(Resources.Load<GameObject>("Prefabs/Backgrounds/Back02")) as GameObject;
		}
		else if(GameData.numberLoadLevel>=41&&GameData.numberLoadLevel<61)
		{
			obj = Instantiate(Resources.Load<GameObject>("Prefabs/Backgrounds/Back03"))as GameObject;
		}
		else if(GameData.numberLoadLevel>=61&&GameData.numberLoadLevel<81)
		{
			obj = Instantiate(Resources.Load<GameObject>("Prefabs/Backgrounds/Back04"))as GameObject;
		}
		else if(GameData.numberLoadLevel>=81&&GameData.numberLoadLevel<101)
		{
			obj = Instantiate(Resources.Load<GameObject>("Prefabs/Backgrounds/Back05"))as GameObject;
		}
		if(statePosition)
		{
			obj.transform.position = new Vector3(0,0,0);
		}
	}

    void Awake()
    {
        if (GamePlay.maxCompleteLevel >= 3)
        {
            AdSDK.SetBannerVisible(true);
        }
    }
}
