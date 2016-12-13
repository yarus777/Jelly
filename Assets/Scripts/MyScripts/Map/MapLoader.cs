namespace Assets.Scripts.MyScripts.Map {
    using System.Collections.Generic;
    using System.Linq;
    using Gates;
    using Lives;
    using Popups;
    using UnityEngine;

    internal class MapLoader : MonoBehaviour {
        private const string ACTIVE_LEVEL_ANIMATION_NAME = "active_level";

        [SerializeField]
        private GatesController _gatesController;

        [SerializeField]
        private LevelButton _levelButtonPrefab;

        [SerializeField]
        private AnimationClip _currentLevelButtonAnimation;

        private List<LevelButton> _buttons;

        private void Awake() {
            // загружаем ворота
            _gatesController.Load();
            GatesStorage.Instance.OnCurrentLevelChanged(GamePlay.maxCompleteLevel);

            // загружаем уровни
            _buttons = Load();
            var currentLevelButton = _buttons.First(x => x.LevelNumber == PlayerPrefs.GetInt("lastOpenLevel", 1));
            var anim = currentLevelButton.gameObject.AddComponent<Animation>();
            anim.AddClip(_currentLevelButtonAnimation, ACTIVE_LEVEL_ANIMATION_NAME);
            anim.Play(ACTIVE_LEVEL_ANIMATION_NAME);

            
        }

        private List<LevelButton> Load() {
            var positions = GetComponentsInChildren<LevelButtonPosition>();
            var buttons = new List<LevelButton>();
            foreach (var position in positions) {
                var button = Instantiate(_levelButtonPrefab.gameObject).GetComponent<LevelButton>();
                position.SetLevelButton(button);
                button.IsActive = IsLevelOpened(position.Number);
                button.Clicked += OnLevelClicked;
                buttons.Add(button);
            }
            return buttons;
        }

        private bool IsLevelOpened(int number) {
            if (number > GamePlay.maxCompleteLevel) {
                return false;
            }
            var currentGates = GatesStorage.Instance.CurrentGates;
            if (currentGates == null) {
                return true;
            }

            return number < currentGates.Level;
        }

        private void OnLevelClicked(int levelNumber) {
            Debug.Log("levelNumber " + levelNumber);
            if (LivesManager.Instance.LivesCount > 0) {
                PlayerPrefs.SetInt("lastOpenLevel", GameData.numberLoadLevel);
                PlayerPrefs.Save();
                GameData.numberLoadLevel = levelNumber;
                PopupsController.Instance.Show(PopupType.StartLevel);
                GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.ButtonPush1, false);
            }
            else {
                PopupsController.Instance.Show(PopupType.NoLifes);
                GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.WindowNotMoves, false);
            }
        }
    }
}