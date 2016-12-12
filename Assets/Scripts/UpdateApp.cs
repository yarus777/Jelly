using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UpdateApp : MonoBehaviour {
	public Text descr, leftBtn, rigthBtn;
	public static bool stopOther = false;
    public GameObject window, back;
	void Awake () {
        int app = PlayerPrefs.GetInt("updateAppScreen", 1);
        Debug.Log(app);
        if (app%2 == 0) {
			descr.text = StringConstants.GetUpdateAppText (0);
			leftBtn.text = StringConstants.GetUpdateAppText (2);
			rigthBtn.text = StringConstants.GetUpdateAppText (1);
            window.SetActive(true);
            back.SetActive(true);
            stopOther = true;
		}
    }

	public void GetNew () {
		Application.OpenURL ("https://play.google.com/store/apps/details?id=jelly.monster.adventure");
        window.SetActive(false);
        back.SetActive(false);
        stopOther = false;
	}

	public void NoGetNew () {
        window.SetActive(false);
        back.SetActive(false);
        stopOther = false;
	}
}
