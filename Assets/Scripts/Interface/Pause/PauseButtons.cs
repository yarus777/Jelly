using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.MyScripts.Lives;

public class PauseButtons : MonoBehaviour {

	public enum Buttons
	{
		Pause,
		Continue,
		Restart,
		MainMenu,
		Settings
	}

	public Buttons button;	

	public enum StateButton
	{
		Normal,
		Highlight
	}

	public List<Sprite> states;

	private SpriteRenderer spriteRenderer;

	void Start()
	{
		GamePlay.pauseCollider = GetComponent<SphereCollider> ();
		spriteRenderer = GetComponent<SpriteRenderer> ();
		UpdateState (StateButton.Normal);
	}

	void OnMouseUpAsButton() {
		if(!GamePlay.blockPauseButton)
		{
			if(button ==  Buttons.Pause)
			{
				PauseClick();
			}
		}
		if(GamePlay.interfaceGame == StateInterfaceGame.Pause)
		{
			if(!GamePlay.blockPauseButton)
			{
				switch(button)
				{
					case Buttons.Continue:
						ContinueClick();
						break;
					case Buttons.Restart:
						PrepareRestartClick();
						break;
					case Buttons.MainMenu:
						PrepareMainMenuClick();
						break;
					case Buttons.Settings:
						SettingsClick();
						break;
				}
				UpdateState (StateButton.Normal);
			}
		}
	}

	void OnMouseDown()
	{
		if(!GamePlay.blockPauseButton)
		{
			UpdateState (StateButton.Highlight);
			GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.ButtonPush1, false);
		}
	}

	void OnMouseUp()
	{
		if(!GamePlay.blockPauseButton)
		{
			UpdateState (StateButton.Normal);
			GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.ButtonPush2, false);
		}
	}

	private void PauseClick()
	{
		if(GamePlay.interfaceGame==StateInterfaceGame.Game)
		{
#if UNITY_ANDROID
			//AdSDK.SetBannerVisible(true);
#endif
			GamePlay.interfacePause = Instantiate(Resources.Load("Prefabs/Interface/Pause")) as GameObject;
			GamePlay.SetInput(false);
			if(GamePlay.inventoryCollider!=null)
			{
				GamePlay.inventoryCollider.enabled = false;
			}
			GamePlay.interfaceGame = StateInterfaceGame.Pause;
			GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.WindowOpen, false);
			if(GamePlay.finger!=null)
			{
				GamePlay.finger.Pause();
			}
			GamePlay.lvlManager.PauseStroke();

		}
	}

	private void ContinueClick()
	{
#if UNITY_ANDROID
		//AdSDK.SetBannerVisible(false);
#endif
		if(GamePlay.interfacePause!=null)
		{
			Destroy(GamePlay.interfacePause);
		}
		GamePlay.SetInput(true);
		GamePlay.interfaceGame = StateInterfaceGame.Game;
		GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.ButtonPlay, false);
		Time.timeScale = 1;
		GamePlay.lvlManager.ResumeStroke();
		if(GamePlay.inventoryCollider!=null)
		{
			GamePlay.inventoryCollider.enabled = true;
		}
	}

	private void PrepareRestartClick()
	{
        if (LivesManager.Instance.LivesCount < 1)
		{
			CreateNotLife();
			return;
		}
		else
		{
			Time.timeScale = 1;
		}

        LivesManager.Instance.SpendLife(1);
		//GamePlay.ChangeCountLife (-1);
//#if UNITY_ANDROID
        //Debug.Log("AdSDK Send Event - START_LEVEL_" + GameData.numberLoadLevel.ToString("000"));
        //AdSDK.SendEvent("START_LEVEL_" + GameData.numberLoadLevel.ToString("000"));
        Debug.Log("AdSDK Send Event - REPLAY_LEVEL_" + GameData.numberLoadLevel.ToString("000"));
        AdSDK.SendEvent("REPLAY_LEVEL_" + GameData.numberLoadLevel.ToString("000"));
//#endif
        GamePlay.blockPauseButton = true;
		GamePlay.soundManager.CreateSoundTypeUI (SoundsManager.UISoundType.ButtonReplay, false);
		Invoke ("RestartClick", 0.7f);
	}

	private void CreateNotLife()
	{
		GamePlay.interfaceGame = StateInterfaceGame.NotLife;
		GameObject ob = Instantiate (Resources.Load ("Prefabs/Interface/NotLife2")) as GameObject;
		ob.transform.localPosition = new Vector3 (5.6f, 7.7f, -8);
//		ob.transform.localScale = new Vector3 (0.6f, 0.56f, 1);
		GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.WindowNotMoves, false);
	}

	private void RestartClick()
	{

		GamePlay.RestartLevel();
	}

	private void PrepareMainMenuClick()
	{
#if UNITY_ANDROID
		//AdSDK.SetBannerVisible(true);
#endif
		Time.timeScale = 1;
		GamePlay.blockPauseButton = true;
		GamePlay.soundManager.CreateSoundTypeUI (SoundsManager.UISoundType.ButtonToMap, false);
		Invoke ("MainMenuClick", 0.7f);
	}

	private void MainMenuClick()
	{
        UnityEngine.SceneManagement.SceneManager.LoadScene("ToMapScene");
	}

	public static void SettingsClick()
	{
		if(GamePlay.interfaceGame==StateInterfaceGame.Pause)
		{
			if(GamePlay.interfacePause!=null)
			{
				Destroy(GamePlay.interfacePause);
			}

            GameObject obj = Instantiate(Resources.Load("Prefabs/Popups/SettingsPopup")) as GameObject;
            obj.transform.parent = GameObject.Find("UICanvas").transform;
            obj.transform.localScale = Vector3.one;
            var tr = obj.GetComponent<RectTransform>();
            tr.anchoredPosition3D = Vector3.zero;
            tr.offsetMax = Vector2.zero;
            tr.offsetMin = Vector2.zero;

			//GamePlay.interfacePause = Instantiate(Resources.Load("Prefabs/Interface/Settings")) as GameObject;
			//GamePlay.interfacePause.transform.localPosition=new Vector3(5.5f,8.25f,-2);
			//GamePlay.interfacePause.transform.localScale = new Vector3(0.5f,0.5f,1);


//			GamePlay.SetInput(false);
			GamePlay.interfaceGame = StateInterfaceGame.Settings;
			GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.WindowOpen, false);
		}
	}

	private void UpdateState(StateButton state)
	{
		spriteRenderer.sprite = states [(int)state];
	}
}
