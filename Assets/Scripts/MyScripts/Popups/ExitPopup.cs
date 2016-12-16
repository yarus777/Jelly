namespace Assets.Scripts.MyScripts.Popups {
    using UnityEngine;

    internal class ExitPopup : Popup {
        public override void Close() {
            GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.WindowClose, false);
            base.Close();
        }

        public override void OnShow() {
            GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.WindowOpen, false);
            base.OnShow();
        }

        public void OnYesBtnClick() {
            GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.ButtonPlay, false);
            Application.Quit();
        }

        public void OnBtnUp() {
            GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.ButtonPush2, false);
        }

        public void OnBtnDown() {
            GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.ButtonPush1, false);
        }
    }
}