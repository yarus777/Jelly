using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MovesUI : MonoBehaviour {
//	public GameObject sprites;
//	public GameObject texts;
	private SpriteRenderer[] spriteRenderes;
	private MeshRenderer[] meshRenderers;

	// Use this for initialization
	void Awake () {
		spriteRenderes = GetComponentsInChildren<SpriteRenderer> ();
		meshRenderers = GetComponentsInChildren<MeshRenderer> ();
	}

	public virtual void Start()
	{
		MoveSwitch (true);
		GamePlay.moveUI = this;
	}
	
	public virtual void MoveSwitch(bool state)
	{
		foreach (SpriteRenderer sprite in spriteRenderes) {
			sprite.enabled = state;		
		}

		foreach (MeshRenderer mesh in meshRenderers) {
			mesh.enabled = state;		
		}
	}


}
