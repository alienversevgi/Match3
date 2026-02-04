using System.Collections.Generic;
using _Game.Scripts.Board;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Game.Scripts.Pipeline.Processors
{
    public class FillStep : IBoardStep
    {
        private int _pendingActions;

        private BoardContext _context;
        private BoardManager _board => _context.Board;

        public async UniTask Execute(BoardContext context)
        {
            _context = context;

            await FallAllRow();
            await UniTask.WaitUntil(() => _context.GravityHandler.IsFinished);
            context.EffectedRows.Clear();
            context.Matches.Clear();
            context.IsRunning = false;
        }

        private async UniTask FallAllRow()
        {
            foreach (int effectedRow in _context.EffectedRows)
            {
                FillRow(effectedRow).Forget();
            }
        }

        private async UniTask FillRow(int index)
        {
            foreach (var fallItem in CalculateFallItems(index))
            {
                _context.GravityHandler.AddItem(fallItem);
            }
        }

        private Stack<FallItem> CalculateFallItems(int x)
        {
            Stack<FallItem> fallItems = new Stack<FallItem>();
            int emptyCount = 0;
            for (int y = 0; y < _context.Board.Height; y++)
            {
                Vector2Int targetPosition = new Vector2Int(x, y);
             
                var cell = _board.GetCell(targetPosition);
                if (cell.HasEntity)
                    continue;

                if (emptyCount == 0)
                    emptyCount = _board.Height - y;

                Vector2Int initialPosition = new Vector2Int(x, y + (emptyCount));

                var fallItem = new FallItem()
                {
                    Entity = _board.SpawnRandomEntity(initialPosition),
                    StartY = _board.CellPositionToWorld(initialPosition).y,
                    TargetY = _board.CellPositionToWorld(targetPosition).y,
                };

                fallItems.Push(fallItem);
                cell.SetEntity(fallItem.Entity);
                _context.NeedToChecks.Add(targetPosition);
            }

            return fallItems;
        }
    }
}