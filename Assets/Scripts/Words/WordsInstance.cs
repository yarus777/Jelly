using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WordsInstance : CacheTransform {
	public string text;
//	public GameObject simbolObject;
//	private float offset = 0f;
//	private float stepOffset = 0.5f;
//	private Vector3 scaleSimbol = new Vector3 (1f, 1f, 1);

//	void Start()
//	{
//		GetSimbolsString ();
//	}

	private void GetSimbolsString()
	{
//		foreach(char c in text)
//		{
//			InstanceChar(c);
//		}
//		transform.localPosition -= new Vector3 ((offset - stepOffset) / 2f, 2.56f,0);
	}

//	private void InstanceChar(char c)
//	{
//		GameObject ob = Instantiate (simbolObject) as GameObject;
//		ob.transform.parent = transform;
//		ob.transform.localScale = scaleSimbol;
//		ob.transform.localPosition = new Vector3(offset,0,0);
//		ob.SetActive (true);
//		if (GamePlay.wordsManager.GetChar (c)!=null)
//		{
//			ob.GetComponent<SpriteRenderer> ().sprite = GamePlay.wordsManager.GetChar (c);
//			offset += ob.GetComponent<SpriteRenderer> ().sprite.bounds.size.x;
//			offset += stepOffset;
//		}
//		else
//		{
//			offset += stepOffset*10;
//		}
//
//	}
}
