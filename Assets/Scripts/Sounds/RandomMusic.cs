using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RandomMusic : MonoBehaviour {
	public List<AudioClip> musics;

	void Awake()
	{
		GamePlay.musicSource = GetComponent<AudioSource> ();
	}

	// Use this for initialization
	void Start () {
		GamePlay.LoadSoundSettings ();
		GetComponent<AudioSource>().clip = musics[0];
//		if(GameData.numberLoadLevel<21)
//		{
//			GetComponent<AudioSource>().clip = musics[2];
//		}
//		else if(GameData.numberLoadLevel>=21&&GameData.numberLoadLevel<61)
//		{
//			GetComponent<AudioSource>().clip = musics[0];
//		}
//		else if(GameData.numberLoadLevel>=61&&GameData.numberLoadLevel<101)
//		{
//			GetComponent<AudioSource>().clip = musics[1];
//		}
		GetComponent<AudioSource> ().Play ();

        if (!GamePlay.musicOn || (GamePlay.musicOn && GamePlay.soundOn)) GetComponent<AudioSource>().Pause();
    }
}
