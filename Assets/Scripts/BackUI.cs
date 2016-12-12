using UnityEngine;
using System.Collections;

public class BackUI : MonoBehaviour {
	public SpriteRenderer[] spriteRenderers;

	void Awake () {
		GamePlay.backUI = this; 
		spriteRenderers = GetComponentsInChildren<SpriteRenderer> ();
	}
}
