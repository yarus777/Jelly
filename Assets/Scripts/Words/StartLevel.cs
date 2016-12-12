using UnityEngine;
using System.Collections;

public class StartLevel : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		GetComponentInChildren<WordsInstance>().text = StringConstants.GetStartText(Random.Range(0, StringConstants.GetCountStartText()));
	}
}
