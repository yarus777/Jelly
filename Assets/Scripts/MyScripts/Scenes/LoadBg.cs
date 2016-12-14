using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.MyScripts.Scenes
{
    class LoadBg : MonoBehaviour
    {
        [SerializeField]
        private List<Sprite> bgsList;
        [SerializeField]
        private Image bg;
        void Start()
        {
            if (GameData.numberLoadLevel < 21)
            {
                bg.sprite = bgsList[0];
            }
            else if (GameData.numberLoadLevel >= 21 && GameData.numberLoadLevel < 41)
            {
                bg.sprite = bgsList[1];
            }
            else if (GameData.numberLoadLevel >= 41 && GameData.numberLoadLevel < 61)
            {
                bg.sprite = bgsList[2];
            }
            else if (GameData.numberLoadLevel >= 61 && GameData.numberLoadLevel < 81)
            {
                bg.sprite = bgsList[3];
            }
            else if (GameData.numberLoadLevel >= 81 && GameData.numberLoadLevel < 101)
            {
                bg.sprite = bgsList[4];
            }
        }

        void Awake()
        {
            if (GamePlay.maxCompleteLevel >= 3)
            {
                AdSDK.SetBannerVisible(true);
            }
        }
    }
}
