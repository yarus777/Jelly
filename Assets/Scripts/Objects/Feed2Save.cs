using UnityEngine;
using System.Collections;

public class Feed2Save : MonoBehaviour {
	private float timeAnimationSave = 0.5f;	
	private Properties saveProperty;

	public void PrepareDelete(Properties property)
	{
		saveProperty = property;

		if(saveProperty.iFeed2.GetCurHp()>1)
		{
			if(!GamePlay.oneShotFillTheBucket)
			{
				GamePlay.soundManager.CreateSoundType (SoundsManager.SoundType.FillTheBucket);
				GamePlay.oneShotFillTheBucket = true;
			}
		}
		else
		{
			if(!GamePlay.oneShotFilledBucket)
			{
				GamePlay.soundManager.CreateSoundType (SoundsManager.SoundType.FilledBucket);
				GamePlay.oneShotFilledBucket = true;
			}
		}

		Invoke ("Delete", timeAnimationSave);
	}

	private void Delete()
	{
		if(saveProperty!=null)
		{
			saveProperty.iFeed2.Attack();
		}
		DestroyImmediate (gameObject);
	}
}
