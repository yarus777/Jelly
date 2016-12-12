using UnityEngine;
using System.Collections;

public class Jam : CacheTransform, IJam {
	private Properties property;
	public Jams typeImage = Jams.JamFull;
	private SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Awake()
	{
		property = GetComponent<Properties>();
		property.iPoints = new Points (PointManager.jam);
		property.iJam = this;
		spriteRenderer = GetComponent<SpriteRenderer> ();
	}

	void Start()
	{
		transform.position += new Vector3 (0, 0, 1);
	}

	void Update()
	{
		//ChangeImage ();
	}

	public void SetTypeImage(Jams typeImage)
	{

		this.typeImage = typeImage;
	}

	public void ChangeImage()
	{
		//пересобрать атлас в соответствии с enum!!!
		switch(typeImage)
		{
			case Jams.JamFull:
				spriteRenderer.sprite = GameField.jamSprites[0];
				break;
			case Jams.JamTop:
				spriteRenderer.sprite = GameField.jamSprites[15];
				break;
			case Jams.JamLeft:
				spriteRenderer.sprite = GameField.jamSprites[9];
				break;
			case Jams.JamAngleLeftTop:
				spriteRenderer.sprite = GameField.jamSprites[10];
				break;
			case Jams.JamBottom:
				spriteRenderer.sprite = GameField.jamSprites[11];
				break;
			case Jams.JamSideLeftRight:
				spriteRenderer.sprite = GameField.jamSprites[13];
				break;
			case Jams.JamAngleLeftBottom:
				spriteRenderer.sprite = GameField.jamSprites[14];
				break;
			case Jams.JamSideLeft:
				spriteRenderer.sprite = GameField.jamSprites[7];
				break;
			case Jams.JamRight:
				spriteRenderer.sprite = GameField.jamSprites[2];
				break;
			case Jams.JamAngleRightTop:
				spriteRenderer.sprite = GameField.jamSprites[1];
				break;
			case Jams.JamSideTopBottom:
				spriteRenderer.sprite = GameField.jamSprites[8];
				break;
			case Jams.JamSideTop:
				spriteRenderer.sprite = GameField.jamSprites[5];
				break;
			case Jams.JamAngleRightBottom:
				spriteRenderer.sprite = GameField.jamSprites[4];
				break;
			case Jams.JamSideRight:
				spriteRenderer.sprite = GameField.jamSprites[6];
				break;
			case Jams.JamSideBottom:
				spriteRenderer.sprite = GameField.jamSprites[12];
				break;
			case Jams.JamBlock:
				spriteRenderer.sprite = GameField.jamSprites[3];
				break;
		}

	}

	#region IJam implementation
	public void PrepareDelete ()
	{
		GameData.manager.DeleteObject (property);
		GamePlay.AddTaskValue (Task.ClearJam, 1);
		property.AddPoints ();
	//	property.AnimationScore ();
//		if(!GamePlay.oneShotJam)
//		{
//			GamePlay.soundManager.CreateSoundType(SoundsManager.SoundType.Jam);
//			GamePlay.oneShotJam = true;
//		}

		DestroyImmediate (gameObject);
	}
	#endregion
}
