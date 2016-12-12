using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Map.Tutorials.Effects;
using UnityEngine;

namespace Assets.Scripts.Map.Tutorials
{
    class TutorialStep : MonoBehaviour
    {
        [SerializeField] 
        private float _delay;

        [SerializeField] 
        private SideEffect[] _additionalEffects;

        private void Awake()
        {
            foreach (var action in _additionalEffects)
            {
                action.StepShownTriggered += OnShown;
            }
        }

        public float Delay {
            get { return _delay; }
        }

        public event Action<TutorialStep> Shown;

        public bool IsActive {
            get { return gameObject.activeSelf; }
            private set { gameObject.SetActive(value);}
        }

        public void MarkShown()
        {
            if (!IsActive)
            {
                return;
            }
            OnShown();
        }

        private void OnShown()
        {
            if (Shown != null)
            {
                Shown(this);
            }
        }

        public void Show()
        {
            IsActive = true;
            foreach (var action in _additionalEffects)
            {
                action.ApplyEffect();
            }
        }

        public void Close()
        {
            IsActive = false;
            foreach (var action in _additionalEffects)
            {
                action.UndoEffect();
            }
        }
    }
}
