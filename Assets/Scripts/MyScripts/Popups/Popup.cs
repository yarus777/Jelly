using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.MyScripts.Popups
{
    public class Popup : MonoBehaviour
    {

        public virtual void Close()
        {
            PopupsController.Instance.Close();
        }

        public virtual void OnShow()
        {
            
        }

        public virtual void OnBackClick()
        {
            Close();
        }
    }
}
