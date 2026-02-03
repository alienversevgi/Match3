using UnityEngine;

namespace _Game.Settings
{
    [CreateAssetMenu(fileName = nameof(PlayerSettings), menuName = Const.SOPath.SO_SETTINGS_MENU_PATH + nameof(PlayerSettings), order = 1)]
    public class PlayerSettings : ScriptableObject
    {
    }
}