using UnityEngine;
using System.Collections;
using System;

//Здесь необходимо написать id наград, которые будут даны
public enum EHonorType{
	Money,
    Energy
}

public class JMAchivementSettings : MonoBehaviour {
	public delegate void AddHonorDelegate(EHonorType type, float count);
	public event AddHonorDelegate OnAddHonor;

	public static JMAchivementSettings jmAchivementSettings = null;
	[Serializable]
	public class HonorSettings {
		public Sprite sprite;
		public EHonorType id;
	}

	/// <summary>
	/// Здесь в редакторе необходимо добавить id наград и их картинки
	/// </summary>
	[SerializeField]
	HonorSettings[] honors;

	void Awake(){
		if (jmAchivementSettings == null) {
			jmAchivementSettings = this;

		} 
		else {
			DestroyImmediate (gameObject);
		}
	}

	// Use this for initialization
    //void Start () {
    //    ProgressController.instance.AddEventHonnor(this);
    //}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void AddHonor(JMHonor honor){
        Debug.Log("AddHonor");
		if (OnAddHonor != null) {
			OnAddHonor (honor.type, honor.count);
		}
	}

	public Sprite GetSprite(EHonorType type){
		foreach (HonorSettings settings in honors) {
			if (settings.id == type) {
				return settings.sprite;
			}
		}
		return null;
	}

	public int GetLauguage(){
		switch (Application.systemLanguage) {
			case SystemLanguage.Russian:
				return 1;
			default:
				return 0;
		}
	}

	public string GetLanguageParcer(){
		switch (Application.systemLanguage) {
			case SystemLanguage.Russian:
				return "ru";
			default:
				return "en";
		}
	}

	public EHonorType GetHonorType(string type){
		switch (type) {
			case "Money":
				return EHonorType.Money;
			case "Energy":
				return EHonorType.Energy;
			default:
				return EHonorType.Money;
		}
	}


}
