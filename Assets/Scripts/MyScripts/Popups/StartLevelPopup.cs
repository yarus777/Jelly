namespace Assets.Scripts.MyScripts.Popups {
    using System.Collections.Generic;
    using Lives;
    using UnityEngine;
    using UnityEngine.UI;

    internal class StartLevelPopup : Popup {
        [SerializeField]
        private Text levelTxt;

        [SerializeField]
        private Text recordTxt;

        [SerializeField]
        private Text targetTitleTxt;

        [SerializeField]
        private Text targetCountTxt;

        [SerializeField]
        private Image targetImg;

        [SerializeField]
        private List<Sprite> iconsList;

        [SerializeField]
        private List<GameObject> stars;

        private int _countStars;
        private int _currentLvl;

        public override void Close() {
            HideStars();
            GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.WindowClose, false);
            base.Close();
        }

        public void OnBtnUp() {
            GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.ButtonPush2, false);
        }

        public void OnBtnDown() {
            GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.ButtonPush1, false);
        }

        public override void OnShow() {
            GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.WindowOpenLevel, false);
            _currentLvl = GameData.numberLoadLevel;
            levelTxt.text = Texts.GetText(WhatText.LevelTxt) + " " + _currentLvl;
            UpdateRecordTxt();
            InitTarget();
            InitStars();
            base.OnShow();
        }

        private void HideStars() {
            for (var i = 0; i < 3; i++) {
                stars[i].SetActive(false);
            }
        }

        private void InitStars() {
            Debug.Log("_currentLvl " + _currentLvl + "_countStars" + _countStars);
            _countStars = PlayerPrefs.GetInt("starsLevel" + _currentLvl);
            for (var i = 0; i < _countStars; i++) {
                stars[i].SetActive(true);
            }
        }

        public void OnPlayBtnClick() {
            SendEvents();
            GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.ButtonPlay, false);
            LivesManager.Instance.SpendLife(1);
            UnityEngine.SceneManagement.SceneManager.LoadScene("SplashScreen");
        }

        private void SendEvents() {
            if (GamePlay.maxCompleteLevel == GameData.numberLoadLevel &&
                PlayerPrefs.GetInt("starsLevel" + GameData.numberLoadLevel, -1) == -1) {
                Debug.Log("AdSDK Send Event - UNIQUE_START_LEVEL_" + GameData.numberLoadLevel.ToString("000"));
                AdSDK.SendEvent("UNIQUE_START_LEVEL_" + GameData.numberLoadLevel.ToString("000"));
                PlayerPrefs.SetInt("starsLevel" + GameData.numberLoadLevel, 0);
            } else {
                Debug.Log("AdSDK Send Event - REPLAY_LEVEL_" + GameData.numberLoadLevel.ToString("000"));
                AdSDK.SendEvent("REPLAY_LEVEL_" + GameData.numberLoadLevel.ToString("000"));
            }
        }

        private void UpdateRecordTxt() {
            var record = PlayerPrefs.GetInt("recordLevel" + _currentLvl);
            if (record == 0) {
                recordTxt.text = Texts.GetText(WhatText.NoRecordTxt);
            } else {
                recordTxt.text = Texts.GetText(WhatText.RecordTxt) + ": " + record;
            }
        }

        private void InitTarget() {
            GameData.parser.ParseLevel(GameData.numberLoadLevel);
            targetTitleTxt.text = GetTargetName(GameData.parser.levelType);
            targetCountTxt.text = GetTargetCount(GameData.parser.levelGoal) +
                                  GetLimit(GameData.parser.limitType, GameData.parser.countLimit);
            SetIcon(GameData.parser.levelType);
        }

        public void SetIcon(Task task) {
            targetImg.sprite = iconsList[((int) task) - 1];
        }

        public string GetTargetCount(int count) {
            var targetCount = Texts.GetText(WhatText.Count) + ": " + count;
            return targetCount;
        }

        public string GetLimit(Limit name, int count) {
            var limit = "";
            switch (name) {
                case Limit.Moves:
                    limit = "\n" + Texts.GetText(WhatText.Moves) + ": " + count;
                    break;
            }
            return limit;
        }

        public string GetTargetName(Task task) {
            var targetName = "";
            switch (task) {
                case Task.Points:
                    targetName = Texts.GetText(WhatText.ReachGetPoints) + ":";
                    break;
                case Task.Save:
                    targetName = Texts.GetText(WhatText.ReachSaveJelly) + ":";
                    break;
                case Task.ClearJam:
                    targetName = Texts.GetText(WhatText.ReachWaterOut) + ":";
                    break;
                case Task.Diamond:
                    targetName = Texts.GetText(WhatText.ReachPotsOut) + ":";
                    break;
                case Task.Feed1:
                    targetName = Texts.GetText(WhatText.ReachFillBags) + ":";
                    break;
                case Task.Feed2:
                    targetName = Texts.GetText(WhatText.ReachFillCups) + ":";
                    break;
                case Task.Dig:
                    targetName = Texts.GetText(WhatText.ReachDestroyIce) + ":";
                    break;
            }

            return targetName;
        }
    }
}