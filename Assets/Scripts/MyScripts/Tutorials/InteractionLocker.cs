using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Map.Tutorials
{
    public class InteractionLocker : MonoBehaviour, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            
        }

        public bool IsActive {
            get { return gameObject.activeSelf; }
            set
            {
                gameObject.SetActive(value);
            }
        }
    }
    
}
