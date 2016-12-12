using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Map.Tutorials.Effects
{
    abstract class SideEffect : MonoBehaviour
    {

        public abstract void ApplyEffect();

        public abstract void UndoEffect();

        public event Action StepShownTriggered;

        protected void OnStepShown()
        {
            if (StepShownTriggered != null)
            {
                StepShownTriggered();
            }
        }
    }
}
