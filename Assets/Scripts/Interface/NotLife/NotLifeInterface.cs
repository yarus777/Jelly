using UnityEngine;
using System.Collections;
using Assets.Scripts.MyScripts.Lives;

public class NotLifeInterface : MonoBehaviour {
	public bool winLose = false;
	public TextMesh text;
	// Use this for initialization
	void Start () {
		text.text = "";
		GamePlay.notLifeUI = this;
		if(winLose)
		{
			transform.localScale = new Vector3(0.75f,0.75f,1f);
		}

	}
	
	// Update is called once per frame
	void Update () {
		if(LivesManager.Instance.LivesCount <10)
		{
			TimeLife ();
		}
		else
		{
			text.text = "";
		}
	}

	/// <summary>
	/// Осталось времени до восстановления жизней
	/// </summary>
	void TimeLife()
	{
        if (LivesManager.Instance.LivesCount <= 9)
		{
			text.text = GamePlay.lifeTimeString;
		}
		else
		{
			text.text = "";
			GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.WindowClose, false);
			if(GamePlay.interfaceGame == StateInterfaceGame.NotLife)
			{
				GamePlay.interfaceGame = StateInterfaceGame.Pause;
			}
			else
			{
                if (GamePlay.interfaceMap != StateInterfaceMap.StartNextLvl)
                {
                    GamePlay.interfaceMap = StateInterfaceMap.Start;
                    GamePlay.EnableButtonsMap(true);
                }
			}
			Destroy(this.gameObject);
		}
	}

}
