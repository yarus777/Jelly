using UnityEngine;
using System.Collections;

public class PUpsUI : MonoBehaviour {
	public SpriteRenderer spriteRenderer;
	public Sprite[] sprites;

	// Use this for initialization
	void Start () {
		GamePlay.puUI = this;
	}

	public void OpenPU()
	{
		switch(GamePlay.puActive)
		{
			case PU.Arrow:
				spriteRenderer.sprite = sprites[0];
				spriteRenderer.transform.localScale = new Vector3(1.5f,1.5f,1f);
				break;
			case PU.Bomb:
				spriteRenderer.sprite = sprites[1];
				spriteRenderer.transform.localScale = new Vector3(1.5f,1.5f,1f);
				break;
			case PU.Prism:
				spriteRenderer.sprite = sprites[2];
				spriteRenderer.transform.localScale = new Vector3(2f,2f,1f);
				break;
		}
	}

	public void ClosePU()
	{
		spriteRenderer.sprite = null;
		GamePlay.puActive = PU.Empty;
	}
}
