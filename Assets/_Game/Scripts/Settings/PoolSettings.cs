using _Game.Scripts.Controllers;
using UnityEngine;

namespace _Game.Settings
{
    [CreateAssetMenu(fileName = nameof(PoolSettings), menuName = Const.SOPath.SO_SETTINGS_MENU_PATH + nameof(PoolSettings), order = 1)]
    public class PoolSettings : ScriptableObject
    {
        [System.Serializable]
        public class PoolElement<T> where T : MonoBehaviour 
        {
            public int Size;
            public T Prefab;
        }
    }
}