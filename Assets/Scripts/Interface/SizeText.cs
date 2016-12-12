using UnityEngine;
using System.Collections;

public class SizeText : CacheTransform {
	public TextMesh textMesh;

	//private Color colorPressed = new Color (1f,1f,1f,0.47f);
	//private Color colorNormal = new Color (1f,1f,1f,0.78f);
	private float persent = 6f;

	private Vector3 scaleNormal;
	private Vector3 scalePressed;

	private Transform textMeshTransform;

	// Use this for initialization
	void Start () {
		textMeshTransform = textMesh.transform;
		scaleNormal = textMeshTransform.localScale;
		scalePressed = new Vector3 (scaleNormal.x*((100f-persent)/100f), 
		                            scaleNormal.y*((100f-persent)/100f), 
		                            scaleNormal.z);
		//textMesh.color = colorNormal;
	}

	void OnMouseDown()
	{
		//textMesh.color = colorPressed;
		textMeshTransform.localScale = scalePressed;
	}
	
	void OnMouseUp()
	{
		//textMesh.color = colorNormal;
		textMeshTransform.localScale = scaleNormal;
	}

	void OnMouseUpAsButton()
	{
		//textMesh.color = colorNormal;
		textMeshTransform.localScale = scaleNormal;
	}
}

