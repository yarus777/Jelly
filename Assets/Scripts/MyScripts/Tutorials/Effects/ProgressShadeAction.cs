using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Map.Tutorials.Effects
{
    class ProgressShadeAction : EnableShadeActionBase
    {
        protected override InteractionLocker Locker
        {
            get { return ProgressController.instance.Locker; }
        }
    }
}
