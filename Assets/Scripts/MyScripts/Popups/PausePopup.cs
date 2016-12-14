using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.MyScripts.Lives;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.MyScripts.Popups
{
    class PausePopup : Popup
    {
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

        private int _currentLvl;
        public override void Close()
        {
            GamePlay.SetInput(true);
            GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.WindowClose, false);
            base.Close();

        }

        public override void OnShow()
        {
            _currentLvl = GameData.numberLoadLevel;
            UpdateRecordTxt();
            InitTarget();
            GamePlay.SetInput(false);
            GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.WindowOpen, false);
            base.OnShow();

        }

        public void OnSettingBtnClick()
        {
            GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.ButtonPush1, false);
            PopupsController.Instance.Show(PopupType.Settings);
        }

        public void OnReplayBtnClick()
        {
            GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.ButtonReplay, false);
            if (LivesManager.Instance.LivesCount < 1)
            {
                PopupsController.Instance.Show(PopupType.NoLives);
            }
            else
            {
                LivesManager.Instance.SpendLife(1);
                Debug.Log("AdSDK Send Event - REPLAY_LEVEL_" + GameData.numberLoadLevel.ToString("000"));
                AdSDK.SendEvent("REPLAY_LEVEL_" + GameData.numberLoadLevel.ToString("000"));
                UnityEngine.SceneManagement.SceneManager.LoadScene("SplashScreen");
            }
        }

        public void OnHomeBtnClick()
        {
            GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.ButtonToMap, false);
            UnityEngine.SceneManagement.SceneManager.LoadScene("ToMapScene");
        }

        public void OnPlayBtnClick()
        {
            GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.ButtonPlay, false);
        }

        private void UpdateRecordTxt()
        {
            var record = PlayerPrefs.GetInt("recordLevel" + _currentLvl);
            if (record == 0)
            {
                recordTxt.text = Texts.GetText(WhatText.NoRecordTxt);
            }
            else
            {
                recordTxt.text = Texts.GetText(WhatText.RecordTxt) + ": " + record;
            }
        }

        private void InitTarget()
        {
            GameData.parser.ParseLevel(GameData.numberLoadLevel);
            targetTitleTxt.text = GetTargetName(GameData.parser.levelType);
            targetCountTxt.text = GetTargetCount(GameData.parser.levelGoal) + GetLimit(GameData.parser.limitType, GameData.parser.countLimit);
            SetIcon(GameData.parser.levelType);
        }

        public void SetIcon(Task task)
        {
            targetImg.sprite = iconsList[((int)task) - 1];
        }

        public string GetTargetCount(int count)
        {
            var targetCount = Texts.GetText(WhatText.Count) + ": " + count;
            return targetCount;
        }

        public string GetLimit(Limit name, int count)
        {
            var limit = "";
            switch (name)
            {
                case Limit.Moves:
                    limit = "\n" + Texts.GetText(WhatText.Moves) + ": " + count;
                    break;
            }
            return limit;
        }

        public string GetTargetName(Task task)
        {
            string targetName = "";
            switch (task)
            {
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
