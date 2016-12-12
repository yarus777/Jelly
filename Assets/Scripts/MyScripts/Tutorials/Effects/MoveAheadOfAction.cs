using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Map.Tutorials.Effects
{
    class MoveAheadOfAction : SideEffect
    {
        private Transform _savedParent;
        private int _savedIndex;
        private Button _target;
        public override void ApplyEffect()
        {
            var locker = ProgressController.instance.Locker.transform;
            _target = GetTarget();
            _target.onClick.AddListener(OnStepShown);
            _savedIndex = _target.transform.GetSiblingIndex();
            _savedParent = _target.transform.parent;
            _target.transform.parent = locker.parent;
            _target.transform.SetSiblingIndex(locker.GetSiblingIndex() + 1);
        }

        public override void UndoEffect()
        {
            _target.transform.parent = _savedParent;
            _target.transform.SetSiblingIndex(_savedIndex);
            _target.onClick.RemoveListener(OnStepShown);
        }

        private Button GetTarget()
        {
            var targetPanel = FindObjectsOfType<ProgressView>().OrderBy(x => x.transform.localPosition.x).First();
            var button = targetPanel.GetComponentInChildren<Button>();
            return button;
        }

    }
}
