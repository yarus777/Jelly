using System;
using UnityEngine;
using System.Collections;
using Assets.Scripts.Map.Tutorials;

public class ProgressController : MonoBehaviour {
	public static ProgressController instance = null;
	//public static ProgressPannelView instanceView = null;
	//public static ProgressButtonView instanceButtonView = null;

	[SerializeField]
	public JMAchivementPack[] jmAchivementPacks;

    [SerializeField]
    private ProgressPannelView _progressPanel;

    [SerializeField]
    private InteractionLocker _locker;

    public InteractionLocker Locker { get { return _locker; } }

    public event Action<int> Updated;
    public bool IsWindowActive { get; private set; }

    void Awake(){
		if (instance == null) {
			instance = this;
			DontDestroyOnLoad (gameObject);
		} 
		else {
			DestroyImmediate (gameObject);
		}
        
    }

    public void OnUpdated()
    {
        if (Updated != null)
        {
            Updated.Invoke(CountNeedTakeHonors());
        }
    }

    public void SetProgressPanelVisibility(bool visibility)
    {
        _progressPanel.gameObject.SetActive(visibility);
        IsWindowActive = visibility;
        if (PanelVisibilityChanged != null)
        {
            //Debug.Log("Panel visibility toggled to: " + visibility);
            PanelVisibilityChanged.Invoke(visibility);
           
        }
       
        
    }

    public void OnBackBtnClick()
    {
        GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.WindowClose, false);
    }

	// Use this for initialization
	void Start () {
		LoadJson ();
		LoadDates ();
        _progressPanel.CreateView();
        var settings = gameObject.GetComponentInChildren<JMAchivementSettings>();
        AddEventHonnor(settings);
		//instanceButtonView.UpdateView ();
        OnUpdated();
	}
	

	private int CountNeedTakeHonors(){
		int count = 0;
		foreach (JMAchivementPack pack in jmAchivementPacks) {
			count += pack.CountNeedTakeHonor ();
		}
		return count;
	}

	/// <summary>
	/// Этим методом можно добавлять прогресс
	/// </summary>
	/// <param name="id">Идентификатор пакета</param>
	/// <param name="count">Количество добавляемого прогресса</param>
	public void AddProgress(string id, int count){
		for (int i=0; i<jmAchivementPacks.Length; i++) {
			if (jmAchivementPacks [i].id == id) {
				if (jmAchivementPacks [i].IsAchivementPack ()) {
					jmAchivementPacks[i].AddProgress (count);
                    _progressPanel.progressViews[i].UpdateView(jmAchivementPacks[i]);
					//instanceButtonView.UpdateView ();
                    OnUpdated();
				}
			}
		}
	}

	/// <summary>
	/// Этим методом можно установить прогресс
	/// </summary>
	/// <param name="id">Идентификатор пакета</param>
	/// <param name="count">Количество добавляемого прогресса</param>
	public void SetProgress(string id, int count){
        //Debug.Log("SetProgress");
		for (int i=0; i<jmAchivementPacks.Length; i++) {
            //Debug.Log("jmAchivementPacks [i].id " +  jmAchivementPacks [i].id);
			if (jmAchivementPacks [i].id == id) {
               
				if (jmAchivementPacks [i].IsAchivementPack ()) {
					jmAchivementPacks [i].SetProgress (count);
                    _progressPanel.progressViews[i].UpdateView(jmAchivementPacks[i]);

				    //instanceButtonView.UpdateView ();
                    OnUpdated();
				}
			}
		}
	}

	private void AddEventHonnor(JMAchivementSettings settings){
      
		foreach (JMAchivementPack pack in jmAchivementPacks) {
			foreach (JMAchivement achivement in pack.jmAchivements) {
				foreach (JMHonor honor in achivement.honors) {
					honor.OnAddHonor += settings.AddHonor;
				}
			}
		}
	}

	void LoadDates(){
		#if UNITY_EDITOR
		//PlayerPrefs.DeleteAll ();
		#endif
		foreach (JMAchivementPack pack in jmAchivementPacks) {
			pack.SetProgress(PlayerPrefs.GetFloat ("jmAchivementProgress_" + pack.id));
			pack.countTakeHonor = PlayerPrefs.GetInt ("jmAchivementHonnors_" + pack.id);
		}
	}

	void LoadJson(){
		Parcer parcer = new Parcer ();
		jmAchivementPacks = parcer.LoadModels ();
	}

    public event Action<bool> PanelVisibilityChanged;
}
