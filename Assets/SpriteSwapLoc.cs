using UnityEngine;
using System.Collections;

public class SpriteSwapLoc : MonoBehaviour {
	public SpriteRenderer target;
	public Sprite[] sprites;
	// Use this for initialization
	void Start () {
		switch (Application.systemLanguage) {
		case SystemLanguage.Russian:
			target.sprite = sprites [(int)(StringConstants.Language.Russian)];
			break;
		default:
			target.sprite = sprites [(int)(StringConstants.Language.English)];
			break;
		}
	}
}
