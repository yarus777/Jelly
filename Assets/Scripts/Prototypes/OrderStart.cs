using UnityEngine;
using System.Collections;

public class OrderStart : MonoBehaviour {
	public int startLayer;

	// Use this for initialization
	void Start () {
		GetComponent<SpriteRenderer> ().sortingOrder = startLayer;
	}
}
