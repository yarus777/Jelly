using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using Assets.Scripts.MyScripts.Lives;

public class ButtonNumber : MonoBehaviour {
	public List<Sprite> sprite;
	private SpriteRenderer spriteRenderer;

	private Color colorDisable = new Color(0.3921f,0.3921f,0.3921f, 1);
	private Color colorNormal = new Color(0.4784f,0.0431f,0.0431f,1);

	public int numberLevel;

    private bool _enabled = true;

	void Start()
	{
		spriteRenderer = GetComponentInChildren<SpriteRenderer> ();
		LoadMaxLevel ();
		SwitchState ();
	}

    private void Awake()
    {
        ProgressController.instance.PanelVisibilityChanged += OnAchievementVisibilityChanged;
    }

    private void OnDestroy()
    {
        ProgressController.instance.PanelVisibilityChanged -= OnAchievementVisibilityChanged;
    }

    private void OnAchievementVisibilityChanged(bool visibility)
    {
        _enabled = !visibility;
    }

    public enum EStateButton
	{
		Disable,
		Normal, 
		Tap
	}

	public EStateButton state;

	void OnMouseUpAsButton() {
        if (!_enabled)
        {
            return;
        }
        //Debug.Log("GamePlay.enableButtonInterface as button level " + GamePlay.enableButtonInterface);
		if (UpdateApp.stopOther) return;
	    if (GamePlay.enableButtonInterface)
	    {
	        if (state != EStateButton.Disable && GamePlay.interfaceMap == StateInterfaceMap.Start)
	        {
                if (LivesManager.Instance.LivesCount > 0)
	            {

	                GamePlay.interfaceMap = StateInterfaceMap.Interface1;
	            }
	            else
	            {
	                NotLife();
	                GamePlay.interfaceMap = StateInterfaceMap.Interface1;
	            }
	            GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.ButtonPush2, false);
	            GamePlay.EnableButtonsMap(false);
	        }
	    }
	}

	void OnMouseDown()
    {
        //Debug.Log("GamePlay.enableButtonInterface down level " + GamePlay.enableButtonInterface);
		if (UpdateApp.stopOther) return;
	    if (GamePlay.enableButtonInterface)
	    {
	        if (state != EStateButton.Disable && GamePlay.interfaceMap == StateInterfaceMap.Start)
	        {
	            state = EStateButton.Tap;
	            SwitchState();
	            GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.ButtonPush1, false);
	        }
	    }
	}

	void OnMouseUp() 
	{
        //Debug.Log("GamePlay.enableButtonInterface up level " + GamePlay.enableButtonInterface);
		if (UpdateApp.stopOther) return;
	    if (GamePlay.enableButtonInterface)
	    {
	        if (state != EStateButton.Disable)
	        {
	            state = EStateButton.Normal;
	            SwitchState();
	            if (GamePlay.interfaceMap == StateInterfaceMap.Start)
	            {
	                GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.ButtonPush2, false);
	            }
	        }
	    }
	}

	public void SwitchState()
	{
		spriteRenderer.sprite = sprite[(int)state];
		if(state == EStateButton.Disable)
		{
			GetComponentInChildren<TextMesh>().color = colorDisable;
		}
		else
		{
			GetComponentInChildren<TextMesh>().color = colorNormal;
		}
	}

	public void LoadMaxLevel()
	{
		if(GamePlay.maxCompleteLevel < 1)
		{
			GamePlay.maxCompleteLevel = 1;
		}
		if (numberLevel <= GamePlay.maxCompleteLevel)
		{
			state = EStateButton.Normal;
		}
		else
		{
			state = EStateButton.Disable;
		}
	}


	private void NotLife()
	{
		GameObject ob = Instantiate (Resources.Load ("Prefabs/Interface/NotLife")) as GameObject;
		ob.transform.parent = Camera.main.transform;
		ob.transform.localPosition = new Vector3 (0, 0, 8);
		GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.WindowNotMoves, false);
	}
}
