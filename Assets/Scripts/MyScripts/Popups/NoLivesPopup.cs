namespace Assets.Scripts.MyScripts.Popups {
    using System.Collections;
    using Lives;
    using UnityEngine;
    using UnityEngine.UI;

    internal class NoLivesPopup : Popup {
        [SerializeField]
        private Text timerTxt;

        public override void Close() {
            GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.WindowClose, false);
            base.Close();
        }

        public override void OnShow() {
            if (LivesManager.Instance.LivesCount < 10) {
                StartCoroutine(UpdateTimer());
            }
            GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.WindowNotMoves, false);
            base.OnShow();
        }

        public void OnBtnUp() {
            GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.ButtonPush2, false);
        }

        public void OnBtnDown() {
            GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.ButtonPush1, false);
        }

        private IEnumerator UpdateTimer() {
            while (true) {
                var timeLeft = LivesManager.Instance.TimeLeftToRefill;
                timerTxt.text = string.Format("{0:00}:{1:00}", timeLeft.Minutes, timeLeft.Seconds);
                yield return new WaitForSeconds(1);
            }
        }

        public void OnShowVideoBtnClick() {
            Close();
        }
    }
}