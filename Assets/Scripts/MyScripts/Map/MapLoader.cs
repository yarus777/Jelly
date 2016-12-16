using System.Collections;
using UnityEngine.UI;

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
        private LevelButton _alternativeLevelButtonPrefab;

        [SerializeField]
        private AnimationClip _currentLevelButtonAnimation;

        private List<LevelButton> _buttons;

        [SerializeField]
        private ScrollRect scroll;

        [SerializeField]
        private int _levelsToChangeColor;

        private GatesStorage _gateStorage;
        private Animation _anim;


        private void Awake()
        {
            _gateStorage = GatesStorage.Instance;
            // загружаем ворота
            _gatesController.Load();
            GatesStorage.Instance.OnCurrentLevelChanged(GamePlay.maxCompleteLevel);

            // загружаем уровни
            _buttons = Load();
            UpdateLevelsState();
            Debug.Log("lastOpenLevel load " + GamePlay.LastOpenedLvl);
            //var currentLevelButton = _buttons.First(x => x.LevelNumber == GamePlay.LastOpenedLvl);
            
            _gateStorage.StateChanged += OnGatesStateChanged;

            Invoke("LoadLvlPopup", 1f);
        }

        private void OnGatesStateChanged(Gate gate, GateState state)
        {
            if (state == GateState.Opened)
            {
                UpdateLevelsState();
            }
        }

        private void UpdateLevelsState()
        {
            foreach (var button in _buttons)
            {
                button.IsActive = IsLevelOpened(button.LevelNumber);
            }
            if (_anim != null)
            {
                _anim.Stop();
                Destroy(_anim);
            }
            var currentLevelButton = _buttons.First(x => x.LevelNumber == MaxAvailableLevel);
            _anim = currentLevelButton.gameObject.AddComponent<Animation>();
            _anim.AddClip(_currentLevelButtonAnimation, ACTIVE_LEVEL_ANIMATION_NAME);
            _anim.Play(ACTIVE_LEVEL_ANIMATION_NAME);
        }

        private void LoadLvlPopup()
        {
            if (LivesManager.Instance.LivesCount > 0)
            {
                GameData.numberLoadLevel = MaxAvailableLevel;
                PopupsController.Instance.Show(PopupType.StartLevel);
            }
        }

        private List<LevelButton> Load() {
            var positions = GetComponentsInChildren<LevelButtonPosition>();
            var buttons = new List<LevelButton>();
            foreach (var position in positions)
            {
                var buttonPrefab = ((position.Number - 1)/_levelsToChangeColor)%2 == 0 ? _levelButtonPrefab.gameObject : _alternativeLevelButtonPrefab.gameObject;
                var button = Instantiate(buttonPrefab).GetComponent<LevelButton>();
                position.SetLevelButton(button);
                button.Clicked += OnLevelClicked;
                buttons.Add(button);
            }
            return buttons;
        }

        private bool IsLevelOpened(int number)
        {
            return number <= MaxAvailableLevel;
        }

        private int MaxAvailableLevel
        {
            get
            {
                var currentGates = GatesStorage.Instance.CurrentGates;
                if (currentGates == null)
                {
                    return GamePlay.maxCompleteLevel;
                }
                return Mathf.Min(GamePlay.maxCompleteLevel, currentGates.Level - 1);
            }
        }

        private void OnLevelClicked(int levelNumber) {
            Debug.Log("levelNumber " + levelNumber);
            if (LivesManager.Instance.LivesCount > 0) {
                GameData.numberLoadLevel = levelNumber;
                GamePlay.LastOpenedLvl = GameData.numberLoadLevel;
                Debug.Log("lastOpenLevel clicked " + GamePlay.LastOpenedLvl);
                PopupsController.Instance.Show(PopupType.StartLevel);
            }
            else {
                PopupsController.Instance.Show(PopupType.NoLives);
            }
        }

    

        void OnDestroy()
        {
            _gateStorage.StateChanged -= OnGatesStateChanged;
            SavePos();
        }
        private void LoadPos()
        {

            scroll.verticalScrollbar.value = PlayerPrefs.GetFloat("ScrollPos", 0f);
        }

        private void SavePos()
        {
            PlayerPrefs.SetFloat("ScrollPos", scroll.verticalScrollbar.value);
            PlayerPrefs.Save();
        }

        public IEnumerator Start()
        {
            yield return null;
            LoadPos();
        }
    }
}