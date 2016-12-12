using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Map.Tutorials.Effects
{
    class EnableShadeAction : EnableShadeActionBase
    {

        [SerializeField] private InteractionLocker _target;
        protected override InteractionLocker Locker
        {
            get { return _target; }
        }
    }
}
