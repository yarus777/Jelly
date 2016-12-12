using UnityEngine;
using System.Collections;

public class LoadEBTexture : MonoBehaviour {
	private SpriteRenderer spriteRenderer;
	public Sprite first;
	public Sprite second;
	public Sprite third;
	// Use this for initialization
	void Awake () {
		spriteRenderer = GetComponent<SpriteRenderer> ();
		if(GameData.numberLoadLevel<40)
		{
			spriteRenderer.sprite = first;
		}
		else if(GameData.numberLoadLevel>=41&&GameData.numberLoadLevel<71)
		{
			spriteRenderer.sprite = second;
		}
		else if(GameData.numberLoadLevel>=71&&GameData.numberLoadLevel<101)
		{
			spriteRenderer.sprite = third;
		}
	}
}
