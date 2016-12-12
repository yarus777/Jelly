using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ProgressPannelView : MonoBehaviour {
	[SerializeField]
	Text nameTextUI;
	[SerializeField]
	ScrollRect scrollRect;
	[SerializeField]
	ProgressView instanceProgressView;

	public ProgressView[] progressViews;

	public string[] textTitle = { "Achivements", "Достижения" };

	/*public ProgressPannelView(){
		ProgressController.instanceView = this;
	}*/

	
    public void CreateView(){
		nameTextUI.text = textTitle [JMAchivementSettings.jmAchivementSettings.GetLauguage ()];
		progressViews = new ProgressView[ProgressController.instance.jmAchivementPacks.Length];
		for (int i=0; i<ProgressController.instance.jmAchivementPacks.Length; i++) {
			if (ProgressController.instance.jmAchivementPacks [i].IsAchivementPack ()) {
				ProgressView view = Instantiate(instanceProgressView.gameObject).GetComponent<ProgressView>();
                view.GetComponent<RectTransform>().SetParent(scrollRect.content, true);
                view.transform.localScale = Vector3.one;		
				view.UpdateView (ProgressController.instance.jmAchivementPacks [i]);
                //view.GetComponent<RectTransform>().localScale = Vector3.one;
				progressViews[i] = view;
			}
		}
		scrollRect.verticalNormalizedPosition = 0f;
	}


}
