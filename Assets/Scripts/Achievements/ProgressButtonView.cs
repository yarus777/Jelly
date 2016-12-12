using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ProgressButtonView : MonoBehaviour {
	[SerializeField]
	GameObject numberObject;
	[SerializeField]
	Text numberText;

    private ProgressController _progressController;

    //public ProgressButtonView(){
    //    ProgressController.instanceButtonView = this;
    //}

    //void Start () {
	    
    //}

    private void Awake()
    {
        //Debug.Log("button awake");
        _progressController = ProgressController.instance;
        var button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
        _progressController.Updated += UpdateView;
        _progressController.OnUpdated();
    }

    private void OnClick()
    {
        //Debug.Log("button clicked");
        _progressController.SetProgressPanelVisibility(true);
    }

    private void OnDestroy()
    {
        _progressController.Updated -= UpdateView;
    }
	


	private void UpdateView(int count){
		//int count = ProgressController.instance.CountNeedTakeHonors ();
		numberObject.SetActive (count > 0);
		numberText.text = count.ToString ();
	}


}
