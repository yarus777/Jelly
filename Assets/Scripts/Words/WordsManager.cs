using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WordsManager : MonoBehaviour {
	public List<GameObject> words;
//	public List<Sprite> chars;
//	private char[] simbols = {'A','a','B','b','C','c','D','d','E','e','F','f','G','g','H','h','I','i','J','j','K','k','L','l',
//		'M','m','N','n','O','o','P','p','Q','q','R','r','S','s','T','t','U','u','V','v','W','w','X','x','Y','y','Z','z','!','.'};

	void Awake()
	{
		GamePlay.wordsManager = this;
	}
//
//	public Sprite GetChar(char c)
//	{
////		int number = 0;
//		for(int i=0; i<simbols.Length; i++)
//		{
//			if(simbols[i]==c)
//			{
//				return chars[i];
//			}
//		}
//		return null;
//	}

	public void InstanceText(Words word)
	{
		Instantiate (words [(int)word]);
	}

}
