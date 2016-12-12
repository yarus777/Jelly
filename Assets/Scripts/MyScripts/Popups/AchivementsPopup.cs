using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.MyScripts.Popups
{
    class AchivementsPopup : Popup
    {
        public override void Close()
        {
            GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.WindowClose, false);
            base.Close();

        }

        public override void OnShow()
        {
            base.OnShow();

        }
    }
}
