using UnityEngine;
using System.Collections;

public class CountInventory : MonoBehaviour {
	public enum Buttons
	{
		Arrow,
		Bomb,
		Prism
	}

	public Buttons button;

	// Use this for initialization
	void Start () {
		UpdateState();
	}

	void OnEnable()
	{
		UpdateState();
	}

	public void UpdateState()
	{
		int count;
		switch(button)
		{
			case Buttons.Arrow:
				count =  GamePlay.GetCountPU(PU.Arrow);
				if(count>9)
				{
					GetComponent<TextMesh>().text = "9+";
				}
				else
				{
					GetComponent<TextMesh>().text = count.ToString();
				}
				break;
			case Buttons.Bomb:
				count =  GamePlay.GetCountPU(PU.Bomb);
				if(count>9)
				{
					GetComponent<TextMesh>().text = "9+";
				}
				else
				{
					GetComponent<TextMesh>().text = count.ToString();
				}
				break;
			case Buttons.Prism:
				count =  GamePlay.GetCountPU(PU.Prism);
				if(count>9)
				{
					GetComponent<TextMesh>().text = "9+";
				}
				else
				{
					GetComponent<TextMesh>().text = count.ToString();
				}
				break;
		}
	}

}
