using UnityEngine;
using System.Collections;

public class TextLocalization : MonoBehaviour {
	public StringConstants.TextType type;
	public bool ToUpper = false;
	public bool ToLower = false;
	// Use this for initialization
	void Start () {
		if(ToUpper)
			GetComponentInChildren<TextMesh> ().text = StringConstants.GetText (type).ToUpper();
		else if(ToLower)
			GetComponentInChildren<TextMesh> ().text = StringConstants.GetText (type).ToLower();
		else
			GetComponentInChildren<TextMesh> ().text = StringConstants.GetText (type);
	}
}
