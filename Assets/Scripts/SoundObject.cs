using UnityEngine;
using System.Collections;
using Assets.Scripts.Sounds;

public class SoundObject : MonoBehaviour {
	private AudioSource audioSource;

	public void StartSound(AudioClip clip)
	{
		if(GamePlay.soundOn)
		{
			audioSource = GetComponent<AudioSource> ();
			audioSource.clip = clip;
            audioSource.volume = MusicManager.Instance.SoundVolume;
			audioSource.Play ();
			Destroy (gameObject, audioSource.clip.length);
		}
		else
		{
			Destroy(gameObject);
		}
	}

	public void StartMaxSound(AudioClip clip)
	{
		audioSource = GetComponent<AudioSource> ();
		audioSource.clip = clip;
        audioSource.volume = MusicManager.Instance.SoundVolume;
		audioSource.Play ();
		Destroy (gameObject, audioSource.clip.length);
	}
}
