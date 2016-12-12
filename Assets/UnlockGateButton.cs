using UnityEngine;
using System.Collections;

public class UnlockGateButton : MonoBehaviour {

    public Sprite[] states;
    public Gate gate;
    private SpriteRenderer spriteRenderer;
    void Start()
    {
        if (gate.state == Gate.GateStates.LockToGame)
            UpdateState(StateButton.Normal);
        else
            UpdateState(StateButton.Highlight);

    }
    void OnMouseUpAsButton()
    {
        if (gate.state != Gate.GateStates.LockToGame) return;
        if (GamePlay.unlockInterface != null) return;
        UpdateState(StateButton.Normal);
        if (GamePlay.interfaceMap == StateInterfaceMap.Start)
        {
            GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.WindowOpen, false);
            GamePlay.stateUI = EUI.Store;
            GamePlay.interfaceMap = StateInterfaceMap.Interface1;
            GameObject ob = Instantiate(Resources.Load("Prefabs/Interface/Unlock")) as GameObject;
            ob.transform.parent = Camera.main.transform;
            ob.transform.localPosition = new Vector3(0, 1, 6);
            GamePlay.EnableButtonsMap(false);
        }
    }

    void OnMouseDown()
    {
        if (UpdateApp.stopOther) return;
        if (GamePlay.unlockInterface != null) return;
        if (gate.state != Gate.GateStates.LockToGame) return;
        if (GamePlay.enableButtonInterface)
        {
            UpdateState(StateButton.Highlight);
            GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.ButtonPush1, false);
            //Debug.Log("-");
        }
    }

    void OnMouseUp()
    {
        if (gate.state != Gate.GateStates.LockToGame) return;
        if (GamePlay.unlockInterface != null) return;

        if (UpdateApp.stopOther) return;
        if (GamePlay.enableButtonInterface)
        {
            UpdateState(StateButton.Normal);
            GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.ButtonPush2, false);
            //Debug.Log("+");
        }
    }

    public void UpdateState(StateButton state)
    {
        this.GetComponent<SpriteRenderer>().sprite = states[(int)state];
    }
}
