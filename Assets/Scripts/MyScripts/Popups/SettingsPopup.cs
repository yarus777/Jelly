namespace Assets.Scripts.MyScripts.Popups {
    using Sounds;
    using UnityEngine;
    using UnityEngine.UI;

    internal class SettingsPopup : Popup {
        [SerializeField]
        private Slider _musicSlider;

        [SerializeField]
        private Slider _soundsSlider;

        private float _musicRange = 0.2f;

        public override void Close() {
            GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.WindowClose, false);
            base.Close();
        }

        public override void OnShow() {
            GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.WindowOpen, false);
            _musicSlider.value = MusicManager.Instance.MusicVolume/_musicRange;
            _soundsSlider.value = MusicManager.Instance.SoundVolume;
            base.OnShow();
        }

        public void SetMusicVolume(float volume) {
            MusicManager.Instance.MusicVolume = volume*_musicRange;
        }

        public void SetSoundsVolume(float volume) {
            MusicManager.Instance.SoundVolume = volume;
        }

        public void OnBtnUp() {
            GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.ButtonPush2, false);
        }

        public void OnBtnDown() {
            GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.ButtonPush1, false);
        }
    }
}