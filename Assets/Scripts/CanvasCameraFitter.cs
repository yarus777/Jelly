using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(Canvas))]
    class CanvasCameraFitter : MonoBehaviour
    {

        private Canvas _canvas;

        private void Awake()
        {
            _canvas = GetComponent<Canvas>();
        }
        private void OnLevelWasLoaded()
        {
            _canvas.worldCamera = Camera.main;
        }
    }
}
