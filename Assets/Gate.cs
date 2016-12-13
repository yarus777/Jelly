using UnityEngine;
using UnityEngine.UI;

public class Gate : MonoBehaviour {
    public int numberGate;
    public GateStates state = GateStates.Lock;
    public long locktime;
    public long unlockTime;
    public long ostTime;
    private long nowTime;
    public Button button;
    public Text timer;

    public enum GateStates {
        Lock,
        Waiting,
        LockToGame,
        Unlock
    }

    void Awake() {
        locktime = System.Convert.ToInt64(PlayerPrefs.GetString("Gate_" + numberGate + "_LockTime", "-1"));
        //Debug.Log("locktime (Awake)" + locktime);
        state = (GateStates)System.Convert.ToInt32(PlayerPrefs.GetInt("Gate_" + numberGate + "_State", 0));
        //Debug.Log("state (Awake)" + state.ToString());
        if (locktime != -1) {
            unlockTime = locktime + numberGate * 380 * 60;
            GamePlay.mapLocker.activeGate = this;
        }
    }

    void Start() {
        //Debug.Log("GamePlay.maxCompleteLevel++ " + GamePlay.maxCompleteLevel);
        //Debug.Log("Gate_2_State"+PlayerPrefs.GetInt("Gate_2_State", (int)GateStates.Unlock));

        nowTime = System.DateTime.Now.ToFileTime() / 10000000;
        //Debug.Log("state: (Start)" + state);
        if (state == GateStates.Waiting) {
            locktime = nowTime;
            PlayerPrefs.SetString("Gate_" + numberGate + "_LockTime", locktime.ToString());
            state = GateStates.LockToGame;
            PlayerPrefs.SetInt("Gate_" + numberGate + "_State", (int)state);
            unlockTime = locktime + numberGate * 380 * 60;
            GamePlay.mapLocker.activeGate = this;
            button.interactable = true;
            //button.GetComponent<UnlockGateButton>().UpdateState(StateButton.Normal);
        }
        if (GamePlay.maxCompleteLevel > numberGate * GameData.allLevels / GameData.locationsCount) {
            Debug.Log("GATE UNLOCKED (Start): " + numberGate);
            state = GateStates.Unlock;
            PlayerPrefs.SetInt("Gate_" + numberGate + "_State", (int)GateStates.Unlock);

            this.gameObject.SetActive(false);
        }
    }

    void FixedUpdate() {
        if (state == GateStates.LockToGame) {
            nowTime = System.DateTime.Now.ToFileTime() / 10000000;
            ostTime = unlockTime - nowTime;
            timer.text = SecondsToHHMMSS((int)ostTime);
            if (ostTime < 0) {
                Unlocker();
            }
        }
    }

    public string SecondsToHHMMSS(int seconds) {
        var hh = seconds / 60 / 60;
        var mm = seconds / 60;
        while (mm > 60) {
            mm -= 60;
        }
        var ss = seconds % 60;
        return hh.ToString("00") + ":" + mm.ToString("00") + ":" + ss.ToString("00");
    }

    public void UpdateState() {
        locktime = System.Convert.ToInt64(PlayerPrefs.GetString("Gate_" + numberGate + "_LockTime", "-1"));
        state = (GateStates)System.Convert.ToInt32(PlayerPrefs.GetInt("Gate_" + numberGate + "_State", 0));
        if (locktime != -1) {
            unlockTime = locktime + numberGate * 380 * 60;
        }
        if (unlockTime < 0) {
            Debug.Log("GATE UNLOCKED (UpdateState): " + numberGate);
            state = GateStates.Unlock;
            GamePlay.maxCompleteLevel++;
            for (var i = 0; i < GamePlay.mapController.levels.Count; i++) {
                //GamePlay.mapController.levels[i].GetComponent<ButtonNumber>().LoadMaxLevel();
                //GamePlay.mapController.levels[i].GetComponent<ButtonNumber>().SwitchState();
            }
            this.gameObject.SetActive(false);
        }
    }

    public void Unlocker() {
        Debug.Log("GATE UNLOCKED (Unlocker): " + numberGate);
        ProgressController.instance.SetProgress("Discoverer", numberGate);
        state = GateStates.Unlock;
        GamePlay.maxCompleteLevel++;
        for (var i = 0; i < GamePlay.mapController.levels.Count; i++) {
            //GamePlay.mapController.levels[i].GetComponent<ButtonNumber>().LoadMaxLevel();
            //GamePlay.mapController.levels[i].GetComponent<ButtonNumber>().SwitchState();
        }
        PlayerPrefs.SetInt("Gate_" + numberGate + "_State", (int)GateStates.Unlock);

        this.gameObject.SetActive(false);
    }

    public void Unlock() {
        var sumStars = 0;
        for (var i = 1; i <= GameData.allLevels; i++) {
            var level = "starsLevel" + i;
            sumStars += PlayerPrefs.GetInt(level);
        }
        Debug.Log("Unlocking Gate: " + numberGate + "\nStars: " + sumStars + "\nTime to unlock: " + ostTime);
        if (ostTime < 0 || sumStars > numberGate * 20 * GameData.countStars - numberGate * 5 && state == GateStates.LockToGame) {
            Unlocker();
        }
        else {
            Debug.Log("SHOW VIDEO FOR GATE");
            if (GamePlay.interfaceMap == StateInterfaceMap.Start) {
                GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.WindowOpen, false);
                GamePlay.stateUI = EUI.Inventory;
                GamePlay.interfaceMap = StateInterfaceMap.Interface1;
                var ob = Instantiate(Resources.Load("Prefabs/Interface/Unlock")) as GameObject;
                ob.transform.parent = Camera.main.transform;
                ob.transform.localPosition = new Vector3(0, 1, 5);
                GamePlay.EnableButtonsMap(false);
            }
        }
    }
}