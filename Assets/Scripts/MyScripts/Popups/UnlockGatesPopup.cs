using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.MyScripts.Gates;
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
            StopAllCoroutines();
        }

        public override void OnShow()
        {
            StartCoroutine(TimeUpdatingCoroutine(GatesStorage.Instance.CurrentGates));
            GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.WindowOpen, false);
            base.OnShow();

        }

        private IEnumerator TimeUpdatingCoroutine(Gates.Gate currentGates)
        {
            while (true)
            {
                var timeLeft = currentGates.TimeLeft;
                timerTxt.text = string.Format("{0:00}:{1:00}:{2:00}", timeLeft.Hours, timeLeft.Minutes, timeLeft.Seconds);
                yield return new WaitForSeconds(1);
            }
        }

        public void OnVideoBtnClick()
        {
            //GamePlay.mapLocker.activeGate.Unlocker();
            //Close();
        }
    }
}
