using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.MyScripts.Popups
{
    class NoMovesPopup : Popup
    {
        [SerializeField]
        private Sprite[] sprites;
        [SerializeField]
        private Image img;
        public override void Close()
        {
            GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.WindowClose, false);
            base.Close();

        }

        public override void OnShow()
        {
            ChangeImg();
            GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.WindowOpen, false);
            base.OnShow();

        }

        public void OnGiveUpBtnClick()
        {
            GamePlay.lvlManager.Capitulate();
        }

        private void ChangeImg()
        {
            switch (Application.systemLanguage)
            {
                case SystemLanguage.Russian:
                    img.sprite = sprites[(int)(StringConstants.Language.Russian)];
                    break;
                default:
                    img.sprite = sprites[(int)(StringConstants.Language.English)];
                    break;
            }
        }
    }
}
