using UnityEngine;
using System.Collections;

public class Confetti : MonoBehaviour {
	private float timeDelete = 0.75f;

	public void DeleteObject()
	{
		GetComponent<Animator> ().SetTrigger ("boom");
		Destroy(gameObject, timeDelete);
	}
}
