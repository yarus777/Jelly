namespace Assets.Scripts.MyScripts.Map {
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    [RequireComponent(typeof (Button))]
    internal class LevelButton : MonoBehaviour {
        [SerializeField]
        private Text _levelNumberText;

        [SerializeField]
        private List<GameObject> stars;

        private int _levelNumber;
        private Button _button;
        private bool _isActive;

        private void Awake() {
            _button = GetComponent<Button>();
        }

        public int LevelNumber {
            get { return _levelNumber; }
            set {
                _levelNumber = value;
                _levelNumberText.text = "" + _levelNumber;
            }
        }

        public bool IsActive {
            get { return _isActive; }
            set {
                _isActive = value;
                _button.interactable = _isActive;
                _levelNumberText.color = _isActive ? new Color(0.5f, 0.04f, 0.04f) : new Color(0.4f, 0.4f, 0.4f);
                InitStars();
            }
        }

        public void OnClick() {
            if (Clicked != null) {
                Clicked.Invoke(_levelNumber);
            }
        }

        public void OnBtnUp()
        {
            GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.ButtonPush2, false);
        }

        public void OnBtnDown()
        {
            GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.ButtonPush1, false);
        }

        public event Action<int> Clicked;

        private void InitStars() {
            var countStars = PlayerPrefs.GetInt("starsLevel" + LevelNumber);
            for (var i = 0; i < countStars; i++) {
                stars[i].SetActive(true);
            }
        }
    }
}