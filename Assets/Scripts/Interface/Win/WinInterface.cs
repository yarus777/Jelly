using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WinInterface : MonoBehaviour {
	public List<GameObject> stars;
	public List<GameObject> loadStar;
	private int lastStar;
//	private int lastLoadStar;

	public TextMesh scored;

	void Awake()
	{
		InitStars ();
		InitScored ();
		GamePlay.soundManager.CreateSoundTypeUI (SoundsManager.UISoundType.WindowLevelWin, false);
	}

	void InitStars()
	{
		int countStars = GamePlay.countStarsLevel;
		lastStar = 0;
//		lastLoadStar = 0;
		float time = 0;
		float step = 0.533f;
		for(int i=0; i<countStars; i++)
		{
			//Invoke("LoadStar", time);
			stars[lastStar].SetActive(true);
			lastStar++;

			time+=step;
		}
	}

	/*private void LoadStar()
	{
		loadStar[lastLoadStar].SetActive(true);
		lastLoadStar++;
		Invoke ("SwitchStar", 0.4f);
	}*/

	private void SwitchStar()
	{
		stars[lastStar].SetActive(true);
		lastStar++;
	}

	private void InitScored()
	{
		string level = "recordLevel" + GameData.numberLoadLevel;
		int record = PlayerPrefs.GetInt (level);
		if(GameData.score>record)
		{
			PlayerPrefs.SetInt(level, GameData.score);
			//scored.text = "New record: "+ GameData.score;
			scored.text = StringConstants.GetText(StringConstants.TextType.NewRecord)+": "+ GameData.score;

			//Post in Facebook

		}
		else
		{
			//scored.text = "Scored: "+GameData.score;
			scored.text = StringConstants.GetText(StringConstants.TextType.Scored)+": "+GameData.score;
		}
	}
}
