namespace Assets.Scripts.MyScripts.Scenes {
    using Popups;
    using UnityEngine;
    using UnityEngine.UI;

    public class GameFieldScene : MonoBehaviour {
        [SerializeField]
        private Text targetTitleTxt;

        [SerializeField]
        private Text movesTitleTxt;

        [SerializeField]
        private Text movesTxt;

        [SerializeField]
        private Text scoreTxt;

        [SerializeField]
        private Text targetTxt;

        void Start()
        {
            //GamePlay.gameUI = this;
            GameData.ScoreChanged += UpdateScore;
            GamePlay.TaskValueUpdated += OnTaskScoreUpdated;
            InitTargetTitle();
            InitMovesTitle();
            UpdateScore(GameData.Score);
            OnTaskScoreUpdated();
        }

        private void UpdateScore(int score) {
            scoreTxt.text = "" + score;
            movesTxt.text = "" + GameData.limit.GetLimitCount();
        }

        private void OnTaskScoreUpdated()
        {
            targetTxt.text = "" + string.Format("{0}/{1}", GameData.taskLevel[0].GetCurrent(), GameData.taskLevel[0].GetGoal());
        }


        private void InitTargetTitle() {
            targetTitleTxt.text = GameData.taskLevel[0].NameTask();
        }

        private void InitMovesTitle() {
            if (GameData.limit.GetTypeLimit() != Limit.Moves) {
                movesTitleTxt.text = GameData.limit.GetLimitName().ToLower();
            }
        }

        public void OnPauseBtnClick() {
            Debug.Log("GamePlay.isTutorialActive " + GamePlay.isTutorialActive);
            if (!GamePlay.isTutorialActive)
            {
                PopupsController.Instance.Show(PopupType.Pause);
            }
            
        }

        void OnDestroy() {
            GameData.ScoreChanged -= UpdateScore;
            GamePlay.TaskValueUpdated -= OnTaskScoreUpdated;
        }

        #region Sounds

        public void OnBtnUp() {
            GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.ButtonPush2, false);
        }

        public void OnBtnDown() {
            GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.ButtonPush1, false);
        }

        #endregion
    }
}