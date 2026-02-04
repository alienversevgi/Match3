using _Game.Scripts.Board;
using _Game.Scripts.Managers;
using _Game.Scripts.Pipeline;
using _Game.Signals;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace _Game.Scripts.Handlers
{
    public class SwipeHandler : IInitializable
    {
        [Inject] private BoardManager _board;
        [Inject] private MatchCheckHandler _matchCheckHandler;
        [Inject] private InputManager _inputManager;
        [Inject] private SignalBus _signalBus;
        [Inject] private BoardPipelineController _pipelineController;

        public void Initialize()
        {
            _signalBus.Subscribe<BoardSignals.Swipe>(OnSwipe);
            _signalBus.Subscribe<BoardSignals.Click>(OnClick);
        }

        private void OnClick(BoardSignals.Click signalData)
        {
        }

        private void OnSwipe(BoardSignals.Swipe signalData)
        {
            var from = _board.WorldToCell(signalData.Position);
            var to = from + signalData.Direction;

            HandleSwap(from, to).Forget();
        }

        private async UniTask HandleSwap(Vector2Int fromPoint, Vector2Int toPoint)
        {
            if (!_board.IsInBounds(fromPoint) || !_board.IsInBounds(toPoint))
                return;
            
            _inputManager.Disable();
            await SwapItems(fromPoint, toPoint);
            var hasFromMatch = _matchCheckHandler.Check(fromPoint);
            var hasToMatch = _matchCheckHandler.Check(toPoint);

            Debug.Log($"CheckMatch : {hasFromMatch} : {hasToMatch}");

            if (!hasFromMatch && !hasToMatch)
            {
                await SwapItems(toPoint, fromPoint);
                _inputManager.Enable();
            }
            else
            {
                _pipelineController.RunMatch().Forget();
            }
        }

        private async UniTask SwapItems(Vector2Int fromPoint, Vector2Int toPoint)
        {
            var from = _board.GetEntity(fromPoint);
            var to = _board.GetEntity(toPoint);

            await UniTask.WhenAll(
                MoveItem(from.transform, to.transform.position),
                MoveItem(to.transform, from.transform.position));

            SwapCellItem(fromPoint, toPoint);
            await UniTask.CompletedTask;
        }

        private void SwapCellItem(Vector2Int fromPoint, Vector2Int toPoint)
        {
            var fromCell = _board.GetCell(fromPoint);
            var toCell = _board.GetCell(toPoint);
            var fromItem = fromCell.Entity;
            fromCell.SetEntity(toCell.Entity);
            toCell.SetEntity(fromItem);
        }

        private async UniTask MoveItem(Transform owner, Vector3 target)
        {
            await owner.DOMove(target, .2f);
        }
    }
}