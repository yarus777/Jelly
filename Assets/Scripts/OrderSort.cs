using UnityEngine;
using System.Collections;

public class OrderSort : MonoBehaviour {
	public OrderLayers layer;
	// Use this for initialization
	void Start () {
		GetComponent<SpriteRenderer> ().sortingOrder = OrderObjects.GetOrderLayer(layer);
	}

}
