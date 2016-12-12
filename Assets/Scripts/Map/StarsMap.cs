using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StarsMap : MonoBehaviour {
	public List<GameObject> stars;
	public int numberLevel;
	public Sprite on;
	public Sprite off;
	void Awake()
	{
		VisibleStars ();
	}

	public void VisibleStars()
	{
		string level = "starsLevel" + numberLevel;
		int countStars = PlayerPrefs.GetInt (level);
//		Debug.Log ("countStars" + numberLevel + " - " + countStars);
		for(int i=0; i<countStars; i++)
		{
			stars[i].GetComponent<SpriteRenderer>().sprite = on;
		}
		for(int i=countStars; i<3; i++)
		{
			stars[i].GetComponent<SpriteRenderer>().sprite = off;
		}
	}
}
