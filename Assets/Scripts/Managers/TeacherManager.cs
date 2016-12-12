using UnityEngine;
using System.Collections;

public class TeacherManager : MonoBehaviour {

	public TextMesh textMesh;
//	private string textUpdate;
//	private float speed = 0.025f;
//	private float speed = 0.04f;
//	private int simbol;

	public void UpdateText(string text)
	{
//		simbol = 1;
		//textUpdate = text;
		textMesh.text = text;
		//SpeedText ();
//		Invoke ("SpeedText", GamePlay.timePhysics);
	}

//	public void SpeedText()
//	{
//		if(simbol>textUpdate.Length)
//		{
//			return;
//		}
//		textMesh.text = textUpdate.Substring (0, simbol);
//		simbol++;
//		Invoke ("SpeedText", speed);
//	}

	public void DestroyTeacher()
	{
		GamePlay.finger.DestroyFinger ();
		GetComponent<Animator>().SetTrigger("close");
		Destroy (gameObject, 0.5f);
	}
}
