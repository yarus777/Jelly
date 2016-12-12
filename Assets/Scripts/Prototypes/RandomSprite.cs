using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RandomSprite : MonoBehaviour {
	public List<Sprite> sprites;


	// Use this for initialization
	void Start () {
		int random = Random.Range (0, sprites.Count);
		GetComponent<SpriteRenderer> ().sprite = sprites [random];
	}

}
