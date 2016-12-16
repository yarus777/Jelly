namespace Assets.Scripts.MyScripts.Popups {
    using System.Collections;
    using Gates;
    using UnityEngine;
    using UnityEngine.UI;

    internal class UnlockGatesPopup : Popup {
        [SerializeField]
        private Text timerTxt;

        public override void Close() {
            GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.WindowClose, false);
            base.Close();
            StopAllCoroutines();
        }

        public override void OnShow() {
            StartCoroutine(TimeUpdatingCoroutine(GatesStorage.Instance.CurrentGates));
            GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.WindowOpen, false);
            base.OnShow();
        }

        private IEnumerator TimeUpdatingCoroutine(Gate currentGates) {
            while (true) {
                var timeLeft = currentGates.TimeLeft;
                timerTxt.text = string.Format("{0:00}:{1:00}:{2:00}", timeLeft.Hours, timeLeft.Minutes, timeLeft.Seconds);
                yield return new WaitForSeconds(1);
            }
        }

        public void OnBtnUp() {
            GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.ButtonPush2, false);
        }

        public void OnBtnDown() {
            GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.ButtonPush1, false);
        }

        public void OnVideoBtnClick() {
            Close();
        }
    }
}