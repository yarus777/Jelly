using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.MyScripts.Lives;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.MyScripts.Popups
{
    class NoLifesPopup : Popup
    {

        [SerializeField] private Text timerTxt;
        public override void Close()
        {
            GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.WindowClose, false);
            base.Close();

        }

        public override void OnShow()
        {
            if (LivesManager.Instance.LivesCount < 10)
            {
                StartCoroutine(UpdateTimer());
            }
         base.OnShow();
        }

        private IEnumerator UpdateTimer()
        {
            while (true)
            {
                var timeLeft = LivesManager.Instance.TimeLeftToRefill;
                timerTxt.text = string.Format("{0:00}:{1:00}", timeLeft.Minutes, timeLeft.Seconds);
                yield return new WaitForSeconds(1);
            }
        }

        public void OnShowVideoBtnClick()
        {
            GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.ButtonPush1, false);
            Close();
        }
    }
}
