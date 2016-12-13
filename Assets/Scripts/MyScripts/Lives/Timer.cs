namespace Assets.Scripts.MyScripts.Lives {
    using System;
    using System.Collections;
    using UnityEngine;

    internal class Timer : MonoBehaviour {
        private float _timeLeft;
        private bool _isStarted;

        public float Interval { get; set; }

        public void StartTimer() {
            if (IsStarted) {
                return;
            }
            _timeLeft = Interval;
            IsStarted = true;
            StartCoroutine(WaitForSeconds());
        }

        private IEnumerator WaitForSeconds() {
            while (_timeLeft > 0) {
                _timeLeft -= Time.deltaTime;
                yield return null;
            }
            _timeLeft = 0;
            OnTick();
        }

        public TimeSpan TimeLeft {
            get {
                return TimeSpan.FromSeconds(IsStarted ? _timeLeft : Interval);
            }
        }

        public bool IsStarted {
            get { return _isStarted; }
            private set {
                _isStarted = value;
                OnActiveChanged(_isStarted);
            }
        }

        private void OnTick() {
            StopTimer();
            if (Tick != null) {
                Tick.Invoke();
            }
        }

        public void StopTimer() {
            IsStarted = false;
        }

        protected virtual void OnActiveChanged(bool isActive) {
        }

        public event Action Tick;

        private void OnDestroy() {
            if (Destroyed != null) {
                Destroyed();
            }
        }

        public event Action Destroyed;
    }
}