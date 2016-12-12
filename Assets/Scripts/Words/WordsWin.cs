using UnityEngine;
using System.Collections;

public class WordsWin : MonoBehaviour {

	// Use this for initialization
	void Start () {
//		Debug.Log (StringConstants.GetCountWinText ());
		GetComponentInChildren<TextMesh> ().text = StringConstants.GetWinText (Random.Range (0, StringConstants.GetCountWinText ()));
	}

}
