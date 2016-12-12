using UnityEngine;

namespace Assets.Scripts.MyScripts.Map
{
    class LevelButtonPosition : MonoBehaviour
    {
        [SerializeField]
        private int _levelNumber;

        private LevelButton _button;
        public int Number { get {return _levelNumber;} }

        public void SetLevelButton(LevelButton button)
        {
            _button = button;
            _button.transform.SetParent(transform);
            _button.transform.localPosition = Vector2.zero;
            _button.transform.localScale = new Vector3(0.9f,0.9f);//Vector3.one;
            _button.LevelNumber = _levelNumber;
        }
    }
}
