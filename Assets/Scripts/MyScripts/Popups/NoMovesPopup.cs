namespace Assets.Scripts.MyScripts.Popups {
    using UnityEngine;
    using UnityEngine.UI;

    internal class NoMovesPopup : Popup {
        [SerializeField]
        private Sprite[] sprites;

        [SerializeField]
        private Image img;

        public override void Close() {
            GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.WindowClose, false);
            base.Close();
        }

        public override void OnShow() {
            ChangeImg();
            GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.WindowNotMoves, false);
            base.OnShow();
        }

        public void OnGiveUpBtnClick() {
            GamePlay.lvlManager.Capitulate();
            Close();
        }

        public void OnBtnUp() {
            GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.ButtonPush2, false);
        }

        public void OnBtnDown() {
            GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.ButtonPush1, false);
        }

        private void ChangeImg() {
            switch (Application.systemLanguage) {
                case SystemLanguage.Russian:
                    img.sprite = sprites[(int) (StringConstants.Language.Russian)];
                    break;
                default:
                    img.sprite = sprites[(int) (StringConstants.Language.English)];
                    break;
            }
        }
    }
}