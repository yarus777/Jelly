using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HonnorView : MonoBehaviour {
	[SerializeField]
	Image image;
	[SerializeField]
	Text valueText;

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
	
	}

	public void UpdateView(JMHonor honor) {
		//image.rectTransform.anchoredPosition = new Vector2 (honor.isBoolType ? 0 : 25, 0);
		image.sprite = JMAchivementSettings.jmAchivementSettings.GetSprite (honor.type);
		valueText.text = honor.isBoolType? "" : honor.count.ToString();
	}
}
