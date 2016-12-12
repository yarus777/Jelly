namespace Assets.Scripts.MyScripts.Gates {
    using Lives;
    using UnityEngine;
    using UnityEngine.UI;

    internal class GatesTimer : Timer {
        [SerializeField]
        private Text _text;

        protected override void OnActiveChanged(bool isActive) {
            base.OnActiveChanged(isActive);
            _text.enabled = isActive;
        }
    }
}