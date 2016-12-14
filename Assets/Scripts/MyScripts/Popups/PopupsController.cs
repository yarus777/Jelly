using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.MyScripts.Popups
{
    public enum PopupType
    {
        Settings,
        Exit,
        StartLevel,
        RateUs,
        NoLives,
        UnlockGates,
        Pause,
        Win,
        Lose,
        NoMoves
    }

    public class PopupsController : UnitySingleton<PopupsController>
    {

        [SerializeField]
        private PopupItem[] _popups;

        [SerializeField]
        private Shade _shade;

        private Stack<Popup> _popupsStack;

        protected override void LateAwake()
        {
            base.LateAwake();
            _popupsStack = new Stack<Popup>();
        }

        public Popup Show(PopupType type)
        {
            var popup = _popups.First(x => x.Type == type);
            popup.Popup.gameObject.SetActive(true);
            popup.Popup.transform.SetAsLastSibling();
            popup.Popup.OnShow();
            _popupsStack.Push(popup.Popup);
            CorrectShade();
            return popup.Popup;
        }

        public void Close()
        {
            var popup = _popupsStack.Pop();
            popup.gameObject.SetActive(false);
            CorrectShade();
        }

        public void OnLevelWasLoaded()
        {
            Debug.Log("Popups clearing...");
            while (_popupsStack.Count > 0)
            {
                Close();
            }

        }

        private void CorrectShade()
        {
            if (_popupsStack.Count > 0)
            {
                // _shade.transform.SetSiblingIndex(transform.childCount - 2); 
                _shade.transform.SetSiblingIndex(_popupsStack.Peek().transform.GetSiblingIndex() - 1);
            }
            _shade.gameObject.SetActive(_popupsStack.Count > 0);
        }

        private void Update()
        {
            if ((Input.GetKeyDown(KeyCode.Escape)) || Input.GetKeyDown(KeyCode.Backspace))
            {
                if (_popupsStack.Count > 0)
                {
                    var popup = _popupsStack.Peek();
                    popup.OnBackClick();
                }
                else
                {
                    Show(PopupType.Exit);
                }

            }
        }
    }

    [Serializable]
    internal class PopupItem
    {
        public PopupType Type;
        public Popup Popup;
    }
}
