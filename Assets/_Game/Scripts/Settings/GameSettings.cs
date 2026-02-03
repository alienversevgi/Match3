using UnityEngine;

namespace _Game.Settings
{
    [CreateAssetMenu(fileName = nameof(GameSettings), menuName = Const.SOPath.SO_SETTINGS_MENU_PATH + nameof(GameSettings), order = 1)]
    public class GameSettings : ScriptableObject
    {
    }
}