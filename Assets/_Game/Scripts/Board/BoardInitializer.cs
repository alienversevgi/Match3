using _Game.Scripts.Components;
using _Game.Scripts.Handlers;
using _Game.Scripts.Managers;
using _Game.Scripts.Pipeline;
using Zenject;

namespace _Game.Scripts.Board
{
    public class BoardInitializer : GameBehaviour
    {
        [Inject] private BoardManager _board;
        [Inject] private BoardPipelineController _pipelineController;
        [Inject] private MatchCheckHandler _matchCheckHandler;
        [Inject] private LevelManager _levelManager;

        public void Initialize()
        {
            _levelManager.Initialize();
            _board.Initialize(_levelManager.CurrentLevel.Board);
            _matchCheckHandler.Initialize();
            _pipelineController.Initialize(_board.Context);
        }
    }
}