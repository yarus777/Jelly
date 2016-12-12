using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ProgressView : MonoBehaviour {
	[SerializeField]
	Text nameText;
	[SerializeField]
	Text descriptionText;
	[SerializeField]
	GameObject[] stars;
	[SerializeField]
	Text sliderText;
	[SerializeField]
	Slider slider;
	[SerializeField]
	Button takeHonorButton;
	[SerializeField]
	Text horrorText;
	[SerializeField]
	ScrollRect horrorScrollRect;
	[SerializeField]
	HonnorView instanceHonnor;

	public HonnorView[] honnorsViews;

	public string[] textHonor = { "Honors", "Награды" };
	public string[] textTakeHonor = { "Take honor", "Забрать награду" };

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
		
	JMAchivementPack achivementPack = null;

	public void UpdateView(JMAchivementPack achivementPack){
		if (!achivementPack.IsAchivementPack ()) {
			DeleteView ();
			return;
		}

		this.achivementPack = achivementPack;
		nameText.text = achivementPack.name;
		descriptionText.text = achivementPack.GetDescriptionText ();
		for (int i = 0; i < stars.Length; i++) {
			stars [i].SetActive (i < achivementPack.CountStars ());
		}

		sliderText.text = achivementPack.GetAchivementProgressText ();
		slider.value = achivementPack.GetAchivementProgress ();
		takeHonorButton.GetComponentInChildren<Text> ().text = textTakeHonor [JMAchivementSettings.jmAchivementSettings.GetLauguage ()];
		takeHonorButton.gameObject.SetActive(achivementPack.CountNeedTakeHonor()>0);
		slider.gameObject.SetActive(achivementPack.CountNeedTakeHonor()<=0);

		horrorText.text = textHonor[JMAchivementSettings.jmAchivementSettings.GetLauguage()];

		if (honnorsViews.Length > 0) {
			foreach (HonnorView view in honnorsViews) {
				DestroyImmediate (view.gameObject);
			}
		}
		JMHonor[] honors = achivementPack.jmAchivements [achivementPack.countTakeHonor].honors;
		honnorsViews = new HonnorView[honors.Length];
		for (int i = 0; i < honors.Length; i++) {
			honnorsViews [i] = Instantiate(instanceHonnor.gameObject).GetComponent<HonnorView>() ;
			honnorsViews [i].GetComponent<RectTransform> ().SetParent (horrorScrollRect.content, true);
            honnorsViews[i].transform.localScale = Vector3.one;
			honnorsViews [i].UpdateView (honors[i]);
		}

		horrorScrollRect.horizontal = honors.Length < 2 ? false : true;
		horrorScrollRect.content.GetComponent<HorizontalLayoutGroup>().padding.left = honors.Length < 2 ? 50 : 0;


	}

	/// <summary>
	/// Этот метод можно настроить под себя, сюда же сделать анимацию
	/// </summary>
	public void DeleteView(){
//		ProgressController.instanceView.progressViews.Remove (this);
		DestroyImmediate (gameObject);
	}

	public void TakeHonorClick(){
        GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.ButtonPush1, false);
		achivementPack.TakeHonor ();
		UpdateView (achivementPack);
		ProgressController.instance.OnUpdated();
	}


}
