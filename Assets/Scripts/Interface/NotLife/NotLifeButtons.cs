using UnityEngine;
using System.Collections;

public class NotLifeButtons : MonoBehaviour {

	void OnMouseUpAsButton() 
	{
		GamePlay.interfaceMap = StateInterfaceMap.Start;
		Destroy(this.transform.parent.gameObject);
	}
}
