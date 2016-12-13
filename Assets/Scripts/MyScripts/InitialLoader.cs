using Assets.Scripts.MyScripts.Gates;
using UnityEngine;

namespace Assets.Scripts.MyScripts
{
    class InitialLoader : MonoBehaviour
    {
        private void Awake()
        {
            GatesStorage.Instance.Init();
        }
    }
}
