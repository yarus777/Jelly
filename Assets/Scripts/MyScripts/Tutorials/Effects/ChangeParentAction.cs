using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Map.Tutorials.Effects
{
    [RequireComponent(typeof(Button))]
    class ChangeParentAction : SideEffect
    {
        [SerializeField]
        private InteractionLocker _targetLocker;

        private Button _target;

        private int _savedIndex;
        private Transform _savedParent;

        public override void ApplyEffect()
        {
            _target = GetComponent<Button>();
            _target.onClick.AddListener(OnStepShown);
            _savedIndex = _target.transform.GetSiblingIndex();
            _savedParent = _target.transform.parent;
            _target.transform.parent = _targetLocker.transform.parent;
            _target.transform.SetSiblingIndex(_targetLocker.transform.GetSiblingIndex() + 1);
        }

        public override void UndoEffect()
        {
            _target.transform.parent = _savedParent;
            _target.transform.SetSiblingIndex(_savedIndex);
            _target.onClick.RemoveListener(OnStepShown);
        }
    }
}
