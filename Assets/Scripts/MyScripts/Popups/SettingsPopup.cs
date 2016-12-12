using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Sounds;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.MyScripts.Popups
{
    class SettingsPopup : Popup
    {

        [SerializeField]
        private Slider _musicSlider;
        [SerializeField]
        private Slider _soundsSlider;

        private float _musicRange = 0.2f;

        public override void Close()
        {
            GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.ButtonPush1, false);
            base.Close();

        }

        public void OnDisable()
        {
            GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.WindowClose, false);
        }

        public override void OnShow()
        {
            GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.WindowOpen, false);
            _musicSlider.value = MusicManager.Instance.MusicVolume / _musicRange;
            _soundsSlider.value = MusicManager.Instance.SoundVolume;
            base.OnShow();

        }

        public void SetMusicVolume(float volume)
        {
            MusicManager.Instance.MusicVolume = volume * _musicRange;
        }

        public void SetSoundsVolume(float volume)
        {
            MusicManager.Instance.SoundVolume = volume;
        }
    }
}
