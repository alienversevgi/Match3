using _Game.Scripts.Factories;
using _Game.Scripts.Handlers;
using _Game.Scripts.Pipeline;
using _Game.Settings;
using _Game.Signals;
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

            Container.Bind<BoardPipelineController>().AsSingle();
            Container.BindInterfacesAndSelfTo<SwipeHandler>().AsSingle();
            Container.Bind<MatchCheckHandler>().AsSingle();
            Container.Bind<EntityFactory>().AsSingle();
            
            BindSignals();
            BindPools();

            Debug.Log("InstallBindings");
        }

        private void BindPools()
        {
        }

        private void BindSignals()
        {
            BindBoardSignals();
        }

        private void BindBoardSignals()
        {
            Container.DeclareSignal<BoardSignals.Swipe>().RequireSubscriber();
            Container.DeclareSignal<BoardSignals.Click>().OptionalSubscriber();
        }
    }
}