namespace Assets.Scripts.MyScripts.Gates {
    using System;
    using System.Collections;
    using Popups;
    using UnityEngine;
    using UnityEngine.UI;

    internal class GateUI : MonoBehaviour {
        [SerializeField]
        private int _levelNumber;

        [Header("Visual State")]
        [SerializeField]
        private Button _unlockButton;

        [SerializeField]
        private Text _timerText;

        private Gate _gate;
        public int Level { get {return _levelNumber;} }

        public event Action<GateUI, GateState> StateChanged;

        public void Init(Gate gate) {
            _gate = gate;
            _gate.StateChanged += OnStateChanged;
            UpdateVisualState();
        }

        private void OnDestroy() {
            if (_gate != null) {
                _gate.StateChanged -= OnStateChanged;
            }
        }

        private void UpdateVisualState() {
            var isWaiting = _gate.Status == GateState.Waiting;
            _unlockButton.interactable = isWaiting;
            _timerText.enabled = isWaiting;
            if (isWaiting) {
                StartCoroutine(TimerUpdatingCoroutine());
            }
            else {
                StopAllCoroutines();
            }
        }

        private IEnumerator TimerUpdatingCoroutine() {
            while (true) {
                var timeLeft = _gate.TimeLeft;
                _timerText.text = string.Format("{0:00}:{1:00}:{2:00}", timeLeft.Hours + timeLeft.Days * 24, timeLeft.Minutes, timeLeft.Seconds);
                yield return new WaitForSeconds(1);
            }
        }

        private void OnStateChanged(Gate gate, GateState gateState) {
            UpdateVisualState();
            if (StateChanged != null) {
                StateChanged.Invoke(this, gateState);
            }
        }

        public void OnClick() {
            if (_gate.Status != GateState.Waiting) {
                return;
            }
            PopupsController.Instance.Show(PopupType.UnlockGates);
        }
    }
}