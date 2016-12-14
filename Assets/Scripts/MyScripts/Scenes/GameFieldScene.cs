using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.MyScripts.Popups;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.MyScripts.Scenes
{
    class GameFieldScene : MonoBehaviour
    {
        [SerializeField]
        private Text targetTitleTxt;
        [SerializeField]
        private Text movesTitleTxt;
        [SerializeField]
        private Text movesTxt;
        void Start()
        {
            targetTitleTxt.text = GameData.taskLevel[0].NameTask();
            if (GameData.limit.GetTypeLimit() != Limit.Moves)
            {
                movesTitleTxt.text = GameData.limit.GetLimitName().ToLower();
            }
            movesTxt.text = GameData.limit.GetLimitCount();

        }



        void Update()
        {

        }

        public void OnPauseBtnClick()
        {
            GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.ButtonPush1, false);
            PopupsController.Instance.Show(PopupType.Pause);
        }
    }
}
