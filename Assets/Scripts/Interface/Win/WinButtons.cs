using UnityEngine;
using System.Collections;

public class WinButtons : MonoBehaviour {

	public enum Buttons
	{
		MainMenu,
		Restart,
		NextLevel
	}

	public Buttons button;

	void OnMouseUpAsButton() 
	{
		switch(button)
		{
			case Buttons.MainMenu:
				MainMenu();
				break;
			case Buttons.NextLevel:
				NextLevel();
				break;
			case Buttons.Restart:
				Restart();
				break;
		}
	}

	private void MainMenu()
	{
        UnityEngine.SceneManagement.SceneManager.LoadScene("Map");
	}

	private void NextLevel()
	{
		if(GameData.numberLoadLevel<100)
		{
			GameData.numberLoadLevel++;
            UnityEngine.SceneManagement.SceneManager.LoadScene("SplashScreen");
			PlayerPrefs.SetInt ("lastOpenLevel", GameData.numberLoadLevel);
		}
	}

	private void Restart()
	{
        GamePlay.RestartLevel();
	}
}
