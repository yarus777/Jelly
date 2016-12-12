using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Sounds
{
    public class MusicManager : UnitySingleton<MusicManager>
    {
        [SerializeField]
        private AudioSource _backgroundMusic;

        private bool _isMusic;
        private string MUSIC_KEY = "music";
        private string MUSIC_VOLUME_KEY = "music_volume";
        private string SOUND_VOLUME_KEY = "sound_volume";

        private float _soundVolume;
        private float _musicVolume;

        public bool IsMusic
        {
            get { return _isMusic; }
            set
            {
                _isMusic = value;
                _backgroundMusic.mute = !value;
                SaveMusic();
            }
        }

        public float MusicVolume
        {
            get { return _musicVolume; }
            set
            {
                _musicVolume = value;
                _backgroundMusic.volume = value;
                SaveMusicVolume();
            }
        }

        public float SoundVolume
        {
            get { return _soundVolume; }
            set
            {
                _soundVolume = value;
                SaveSoundVolume();
            }
        }

        private void SaveMusicVolume()
        {
            PlayerPrefs.SetFloat(MUSIC_VOLUME_KEY, MusicVolume);
            PlayerPrefs.Save();
        }

        private void SaveSoundVolume()
        {
            PlayerPrefs.SetFloat(SOUND_VOLUME_KEY, SoundVolume);
            PlayerPrefs.Save();
        }

        private void SaveMusic()
        {
            PlayerPrefs.SetInt(MUSIC_KEY, IsMusic ? 1 : 0);
        }

        private void Load()
        {
            IsMusic = PlayerPrefs.GetInt(MUSIC_KEY, 1) == 1;
            MusicVolume = PlayerPrefs.GetFloat(MUSIC_VOLUME_KEY, 0.2f);
            SoundVolume = PlayerPrefs.GetFloat(SOUND_VOLUME_KEY, 1);
        }

        protected override void LateAwake()
        {
            base.LateAwake();
            Load();
            _backgroundMusic.Play();
        }
    }
}
