using UnityEngine;
using Zenject;

namespace _Game.Installers
{
    [CreateAssetMenu(fileName = nameof(GameSettingInstaller), menuName = Const.SOPath.SO_INSTALLER_MENU_PATH, order = 1)]
    public class GameSettingInstaller : ScriptableObjectInstaller<GameSettingInstaller>
    {
        [SerializeField] private ScriptableObject[] settings;

        public override void InstallBindings()
        {
            foreach (ScriptableObject setting in settings)
            {
                Container.BindInterfacesAndSelfTo(setting.GetType()).FromInstance(setting);
            }
        }
    }
}