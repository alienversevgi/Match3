using _Game.Scripts.Board;
using _Game.Scripts.Components;
using Zenject;

namespace _Game.Scripts.Managers
{
    public class GameManager : GameBehaviour
    {
        [Inject] private BoardInitializer _boardInitializer;
        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            _boardInitializer.Initialize();
        }
    }
}