using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Map.Tutorials
{
    class MapTutorial : MonoBehaviour
    {

        private const string KEY = "tutorial_map";

        [SerializeField] 
        private TutorialStep[] _steps;

        private bool _isTutorialShown;

        private int _currentStepIndex;

        private void Awake()
        {
            _isTutorialShown = PlayerPrefs.GetInt(KEY, 0) == 1;
        }

        private void Start()
        {
            if (GamePlay.maxCompleteLevel == 6 && !_isTutorialShown)
            {
                Show();
            }
        }

        private void Show()
        {
            if (_steps.Length == 0)
            {
                return;
            }
            ShowStep(0);
        }

        private void ShowStep(int index)
        {
            _currentStepIndex = index;
            var step = _steps[index];
            step.Shown += OnStepShown;
            step.Show();
        }

        private void OnStepShown(TutorialStep step)
        {
            step.Close();
            step.Shown -= OnStepShown;
            if (_currentStepIndex + 1 < _steps.Length)
            {
                StartCoroutine(DelayCoroutine(_steps[_currentStepIndex+1].Delay, onPassed));
            }
            else
            {
                OnFinished();
            }
        }

        private void onPassed()
        {
            ShowStep(_currentStepIndex + 1);
        }

        public event Action Finished;

        private void OnFinished()
        {
            PlayerPrefs.SetInt(KEY, 1);
            if (Finished != null)
            {
                Finished();
            }
        }

        private IEnumerator DelayCoroutine(float delay, Action onPassed)
        {
            yield return new WaitForSeconds(delay);
            onPassed();
        }
    }
}
