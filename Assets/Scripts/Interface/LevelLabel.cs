using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelLabel : MonoBehaviour {

    void Awake()
    {
        SwitchNumber();
    }
	public void SwitchNumber ()
	{
		TextMesh[] textMeshs = GetComponentsInChildren<TextMesh> ();
		foreach(TextMesh textMesh in textMeshs)
		{
			textMesh.text = StringConstants.GetText(StringConstants.TextType.Level)+" "+GameData.numberLoadLevel;
		}

	}
}
