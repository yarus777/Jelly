using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.MyScripts.Popups
{
    class UnlockGatesPopup : Popup
    {

        [SerializeField] private Text timerTxt;
        public override void Close()
        {
            GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.WindowClose, false);
            base.Close();

        }

        public override void OnShow()
        {
            timerTxt.text = GamePlay.mapLocker.activeGate.SecondsToHHMMSS((int)GamePlay.mapLocker.activeGate.ostTime);
            GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.WindowOpen, false);
            base.OnShow();

        }

        public void OnVideoBtnClick()
        {
            //GamePlay.mapLocker.activeGate.Unlocker();
            Close();
        }
    }
}
