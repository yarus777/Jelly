using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Map.Tutorials.Effects
{
    abstract class EnableShadeActionBase : SideEffect
    {


        protected abstract InteractionLocker Locker { get; }

        public override void ApplyEffect()
        {
            Locker.IsActive = true;
        }

        public override void UndoEffect()
        {
            Locker.IsActive = false;
        }
    }
}
