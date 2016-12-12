using UnityEngine;
using System.Collections;

public class WriteTextTutorial : MonoBehaviour {
	private TextMesh textMesh;
	private string textUpdate;
	//	private float speed = 0.025f;
	private float speed = 0.04f;
	private int simbol;

	void Awake()
	{
		textMesh = GetComponent<TextMesh> ();
		UpdateText (" Select 3 jelly \nfor match");
	}
	
	public void UpdateText(string text)
	{
		simbol = 1;
		textUpdate = text;
		SpeedText ();
	}
	
	public void SpeedText()
	{
		if(simbol>textUpdate.Length)
		{
			return;
		}
		textMesh.text = textUpdate.Substring (0, simbol);
		simbol++;
		Invoke ("SpeedText", speed);
	}
}
