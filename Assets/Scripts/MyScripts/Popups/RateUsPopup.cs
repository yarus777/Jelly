namespace Assets.Scripts.MyScripts.Popups {
    using UnityEngine;

    internal class RateUsPopup : Popup {
        public override void Close() {
            GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.WindowClose, false);
            base.Close();
        }

        public override void OnShow() {
            GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.WindowOpen, false);
            base.OnShow();
        }

        public void OnBtnUp() {
            GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.ButtonPush2, false);
        }

        public void OnBtnDown() {
            GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.ButtonPush1, false);
        }

        public void OnYesBtnClick() {
            Application.OpenURL("https://play.google.com/store/apps/details?id=jelly.monster.adventure");
            PlayerPrefs.SetInt("noRateUs", 1);
            PlayerPrefs.Save();
#if UNITY_ANDROID
            AdSDK.SendEvent("RATE_US_YES");
#endif
            Close();
        }

        public void OnNoBtnClick() {
            PlayerPrefs.SetInt("countRateUs", 0);
#if UNITY_ANDROID
            AdSDK.SendEvent("RATE_US_LATER");
#endif
            Close();
        }
    }
}