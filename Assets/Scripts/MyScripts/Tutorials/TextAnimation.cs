using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Map.Tutorials
{
    class TextAnimation : MonoBehaviour
    {
        [SerializeField]
        public Text tutorialText;

        private float speed = 0.04f;
        private int simbol;
        private string textUpdate;

        public void Awake()
        {
            var text = tutorialText.GetComponent<LangItem>().whatIsThis;
            UpdateText(Texts.GetText(text));
            //UpdateText(Texts.GetText(WhatText.TutorialText1));
        }
        public void UpdateText(string text)
        {
            simbol = 1;
            textUpdate = text;
            SpeedText();
        }

        private void SpeedText()
        {
            if (simbol > textUpdate.Length)
            {
                return;
            }
            tutorialText.text = textUpdate.Substring(0, simbol);
            simbol++;
            Invoke("SpeedText", speed);
        }
    }
}
