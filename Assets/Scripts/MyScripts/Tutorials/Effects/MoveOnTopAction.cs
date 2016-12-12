using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Map.Tutorials.Effects
{
    class MoveOnTopAction : SideEffect
    {

        [SerializeField] 
        private Transform _target;

        private int _savedPosition;
        public override void ApplyEffect()
        {
            _savedPosition = _target.GetSiblingIndex();
            _target.SetAsLastSibling();
        }

        public override void UndoEffect()
        {
            _target.SetSiblingIndex(_savedPosition);
        }
    }
}
