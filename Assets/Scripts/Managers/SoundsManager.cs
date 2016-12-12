using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundsManager : MonoBehaviour {

	public List<AudioClip> select;
	public List<AudioClip> deselect;

	public AudioClip bee;
	public AudioClip bomb;
	public AudioClip chocolate;
	public AudioClip cookie;
	public AudioClip cookie2;
	public AudioClip feed;
	public AudioClip feed_boom;
	public AudioClip fill_the_bucket;
	public AudioClip filled_bucket;
	public AudioClip lightning;
	public AudioClip powerup;
	public AudioClip prison;
	public AudioClip slime;
	public AudioClip destroy;

	private int curSelect = -1;
	private int curDeSelect = -1;

	public SoundObject soundObject;

	public AudioClip buttonOff;
	public AudioClip buttonOn;
	public AudioClip buttonPlay;
	public AudioClip buttonPush1;
	public AudioClip buttonPush2;
	public AudioClip buttonReplay;
	public AudioClip buttonToMap;
	public AudioClip popupLevelComplete;
	public AudioClip popupLevelFailed;
	public AudioClip popupTask;
	public AudioClip windowClose;
	public AudioClip windowLevelLose;
	public AudioClip windowLevelWin;
	public AudioClip windowNotMove;
	public AudioClip windowOpenLevel;
	public AudioClip windowOpen;
	public AudioClip starOpen;
	public AudioClip bonusTime;
	public AudioClip prism_boom;
	public List<AudioClip> prism_select;


	public enum Duration
	{
		Up,
		Down
	}

	public enum SoundType
	{
		Arrow,
		Bomb,
		Pot,
		Ice,
		Jam,
		FillTheBucket,
		FilledBucket,
		Puddle,
		PuddleBoom,
		PowerUp,
		SlimeOpen,
		SlimeDestroy,
		DigAttack,
		DigDrop,
		PrismBoom,
		PrismSelect01,
		PrismSelect02,
		PrismSelect03,
		PrismSelect04,
		PrismSelect05
	}

	public enum UISoundType
	{
		ButtonOff,
		ButtonOn,
		ButtonPlay,
		ButtonPush1,
		ButtonPush2,
		ButtonReplay,
		ButtonToMap,
		PopupLevelComplete,
		PopupLevelFailed,
		PopupTask,
		WindowClose,
		WindowLevelLose,
		WindowLevelWin,
		WindowNotMoves,
		WindowOpenLevel,
		WindowOpen,
		StarOpen,
		BonusTime
	}

	void Awake()
	{
		GamePlay.soundManager = this;
	}

	public void CreateSelect(Duration duration)
	{
		int loadSelect = 0;
		switch(duration)
		{
			case Duration.Up:
				loadSelect = curSelect;
				loadSelect++;
				curSelect++;
				break;
			case Duration.Down:
				loadSelect = curSelect;
				loadSelect--;
				curSelect--;
				break;
		}
		if(curSelect<0)
		{
			loadSelect = 0;
		}
		if(curSelect>select.Count-1)
		{
			loadSelect = select.Count-1;
		}
		SoundObject obj = Instantiate(soundObject) as SoundObject;
		obj.StartSound(select[loadSelect]);
	}

	public void CreateDeSelect(Duration duration)
	{
		int loadSelect = 0;
		switch(duration)
		{
			case Duration.Up:
				loadSelect = curDeSelect;
				loadSelect++;
				curDeSelect++;
				break;
			case Duration.Down:
				loadSelect = curDeSelect;
				loadSelect--;
				curDeSelect--;
				break;
		}
		if(curDeSelect<0)
		{
			loadSelect = 0;
		}
		if(curDeSelect>deselect.Count-1)
		{
			loadSelect = deselect.Count-1;
		}
		SoundObject obj = Instantiate(soundObject) as SoundObject;
		obj.StartSound(deselect[loadSelect]);
	}

	public void ResetSelect()
	{
		curSelect = -1;
	}

	public void ResetDeSelect()
	{
		curDeSelect = -1;
	}

	public void CreateSoundType(SoundType type)
	{
		AudioClip clip = null;
		switch(type)
		{
			case SoundType.Arrow:
				clip = lightning;
				break;
			case SoundType.Bomb:
				clip = bomb;
				break;
			case SoundType.Pot:
				clip = bee;
				break;
			case SoundType.Ice:
				clip = prison;
				break;
			case SoundType.Jam:
				clip = chocolate;
				break;
			case SoundType.FillTheBucket:
				clip = fill_the_bucket;
				break;
			case SoundType.FilledBucket:
				clip = filled_bucket;
				break;
			case SoundType.Puddle:
				clip = feed;
				break;
			case SoundType.PuddleBoom:
				clip = feed_boom;
			break;
			case SoundType.PowerUp:
				clip = powerup;
				break;
			case SoundType.SlimeOpen:
				clip = slime;
				break;
			case SoundType.SlimeDestroy:
				clip = destroy;
				break;
			case SoundType.DigAttack:
				clip = cookie;
				break;
			case SoundType.DigDrop:
				clip = cookie2;
				break;
			case SoundType.PrismBoom:
				clip = prism_boom;
				break;
			case SoundType.PrismSelect01:
				clip = prism_select[0];
				break;
			case SoundType.PrismSelect02:
				clip = prism_select[1];
				break;
			case SoundType.PrismSelect03:
				clip = prism_select[2];
				break;
			case SoundType.PrismSelect04:
				clip = prism_select[3];
				break;
			case SoundType.PrismSelect05:
				clip = prism_select[4];
				break;

		}
		if(clip!=null)
		{
			SoundObject obj = Instantiate(soundObject) as SoundObject;
			obj.StartSound(clip);
		}
	}

	public void CreateSoundTypeUI(UISoundType type, bool maxSound)
	{
		AudioClip clip = null;
		switch(type)
		{
			case UISoundType.ButtonOff:
				clip = buttonOff;
				break;
			case UISoundType.ButtonOn:
				clip = buttonOn;
				break;
			case UISoundType.ButtonPlay:
				clip = buttonPlay;
				break;
			case UISoundType.ButtonReplay:
				clip = buttonReplay;
				break;
			case UISoundType.ButtonToMap:
				clip = buttonToMap;
				break;
			case UISoundType.PopupLevelComplete:
				clip = popupLevelComplete;
				break;
			case UISoundType.PopupLevelFailed:
				clip = popupLevelFailed;
				break;
			case UISoundType.PopupTask:
				clip = popupTask;
				break;
			case UISoundType.WindowClose:
				clip = windowClose;
				break;
			case UISoundType.WindowLevelWin:
				clip = windowLevelWin;
				break;
			case UISoundType.WindowLevelLose:
				clip = windowLevelLose;
				break;
			case UISoundType.WindowNotMoves:
				clip = windowNotMove;
				break;
			case UISoundType.WindowOpen:
				clip = windowOpen;
				break;
			case UISoundType.ButtonPush1:
				clip = buttonPush1;
				break;
			case UISoundType.ButtonPush2:
				clip = buttonPush2;
				break;
			case UISoundType.WindowOpenLevel:
				clip = windowOpenLevel;
				break;
			case UISoundType.StarOpen:
				clip = starOpen;
				break;
			case UISoundType.BonusTime:
				clip = bonusTime;
				break;
		}
		if(clip!=null)
		{
			SoundObject obj = Instantiate(soundObject) as SoundObject;
			if(maxSound)
			{
				obj.StartMaxSound(clip);
			}
			else
			{
				obj.StartSound(clip);
			}
		}
	}

}
