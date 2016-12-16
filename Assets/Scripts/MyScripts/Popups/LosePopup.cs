namespace Assets.Scripts.MyScripts.Popups {
    using Lives;
    using UnityEngine;
    using UnityEngine.UI;

    internal class LosePopup : Popup {
        [SerializeField]
        private Text levelTxt;

        private int _currentLvl;

        public override void Close() {
            GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.WindowClose, false);
            base.Close();
        }

        public override void OnShow() {
            _currentLvl = GameData.numberLoadLevel;
            levelTxt.text = Texts.GetText(WhatText.LevelTxt) + " " + _currentLvl;
            GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.WindowLevelLose, false);
            base.OnShow();
        }

        public void OnHomeBtnClick() {
            GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.ButtonToMap, false);
            UnityEngine.SceneManagement.SceneManager.LoadScene("ToMapScene");
        }

        public void OnReplayBtnClick() {
            GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.ButtonReplay, false);
            if (LivesManager.Instance.LivesCount < 1) {
                PopupsController.Instance.Show(PopupType.NoLives);
            } else {
                LivesManager.Instance.SpendLife(1);
                Debug.Log("AdSDK Send Event - REPLAY_LEVEL_" + GameData.numberLoadLevel.ToString("000"));
                AdSDK.SendEvent("REPLAY_LEVEL_" + GameData.numberLoadLevel.ToString("000"));
                UnityEngine.SceneManagement.SceneManager.LoadScene("SplashScreen");
            }
        }

        public void OnCrossPromo1BtnClick() {
            Application.OpenURL("http://adeco.adecosystems.com:1628/click?id=2033");
        }

        public void OnCrossPromo2BtnClick() {
            Application.OpenURL("http://adeco.adecosystems.com:1628/click?id=2034");
        }

        public void OnBtnUp() {
            GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.ButtonPush2, false);
        }

        public void OnBtnDown() {
            GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.ButtonPush1, false);
        }
    }
}