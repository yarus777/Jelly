using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.MyScripts.Popups
{
    class RateUsPopup : Popup
    {
        public override void Close()
        {
            GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.WindowClose, false);
            base.Close();

        }

        public override void OnShow()
        {
            GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.WindowOpen, false);
            base.OnShow();

        }

        public void OnYesBtnClick()
        {
            GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.ButtonPush1, false);
            Application.OpenURL("https://play.google.com/store/apps/details?id=jelly.monster.adventure");
            PlayerPrefs.SetInt("noRateUs", 1);
            PlayerPrefs.Save();
#if UNITY_ANDROID
            AdSDK.SendEvent("RATE_US_YES");
#endif
            Close();
        }

        public void OnNoBtnClick()
        {
            GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.ButtonPush1, false);
            PlayerPrefs.SetInt("countRateUs", 0);
#if UNITY_ANDROID
            AdSDK.SendEvent("RATE_US_LATER");
#endif
            Close();
        }
    }
}
