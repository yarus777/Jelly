namespace Assets.Scripts.MyScripts.Lives {
    using System;
    using System.Collections;
    using UnityEngine;

    internal class Timer : MonoBehaviour {
        private float _timeLeft;
        private bool _isStarted;

        public virtual void StartTimer(float initialValue) {
            if (IsStarted) {
                return;
            }
            _timeLeft = initialValue;
            IsStarted = true;
            StartCoroutine(WaitForSeconds());
        }

        private IEnumerator WaitForSeconds() {
            while (_timeLeft > 0) {
                _timeLeft -= Time.deltaTime;
                yield return null;
            }
            _timeLeft = 0;
            OnTick(true);
        }

        public TimeSpan TimeLeft {
            get { return TimeSpan.FromSeconds(_timeLeft); }
        }

        public bool IsStarted {
            get { return _isStarted; }
            private set {
                _isStarted = value;
                OnActiveChanged(_isStarted);
            }
        }

        private void OnTick(bool isRealtime) {
            StopTimer();
            if (Tick != null) {
                Tick.Invoke(isRealtime);
            }
        }

        public void StopTimer() {
            IsStarted = false;
        }

        protected virtual void OnActiveChanged(bool isActive) {
        }

        public event Action<bool> Tick;

        private void OnDestroy() {
            if (Destroyed != null) {
                Destroyed();
            }
        }

        public event Action Destroyed;
    }
}