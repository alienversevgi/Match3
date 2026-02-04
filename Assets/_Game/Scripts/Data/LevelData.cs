using Sirenix.OdinInspector;
using UnityEngine;

namespace _Game.Scripts.Data
{
    [CreateAssetMenu(fileName = nameof(LevelData), menuName = Const.SOPath.SO_DATA_MENU_PATH + nameof(LevelData),
        order = 1)]
    public class LevelData : SerializedScriptableObject
    {
        public BoardData Board;
        // LevelObjective
    }
}