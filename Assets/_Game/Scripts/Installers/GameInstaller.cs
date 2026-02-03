using _Game.Settings;
using UnityEngine;
using Zenject;

namespace _Game.Installers
{
    public class GameInstaller : MonoInstaller
    {
        [Inject] private PrefabSettings _prefabSettings;

        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);

            BindSignals();
            BindPools();

            Debug.Log("InstallBindings");
        }

        private void BindPools()
        {
        }

        private void BindSignals()
        {

        }
    }
}