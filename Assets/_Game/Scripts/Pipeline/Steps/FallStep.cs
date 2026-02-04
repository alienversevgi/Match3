using System.Collections.Generic;
using _Game.Scripts.Board;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Game.Scripts.Pipeline.Processors
{
    public class FallStep : IBoardStep
    {
        private BoardContext _context;
        private BoardManager _board => _context.Board;

        public async UniTask Execute(BoardContext context)
        {
            _context = context;
            
            await FallAllRow();
        }

        private async UniTask FallAllRow()
        {
            foreach (int effectedRow in _context.EffectedRows)
            {
                FallRow(effectedRow).Forget();
            }
        }

        private async UniTask FallRow(int index)
        {
            foreach (var fallItem in CalculateFallItems(index))
            {
                _context.GravityHandler.AddItem(fallItem);
            }
        }

        private Queue<FallItem> CalculateFallItems(int x)
        {
            Queue<int> emptyCellIndex = new Queue<int>();
            Queue<FallItem> fallItems = new Queue<FallItem>();
            for (int y = 0; y < _context.Board.Height; y++)
            {
                var cell = _board.GetCell(x, y);
                if (!cell.HasEntity)
                {
                    emptyCellIndex.Enqueue(y);
                    continue;
                }

                if (emptyCellIndex.Count == 0)
                    continue;

                var targetPosition = new Vector2Int(x,  emptyCellIndex.Dequeue());
                var targetCell = _board.GetCell(targetPosition);
                fallItems.Enqueue(new FallItem()
                {
                    Entity = cell.Entity,
                    StartY = _board.CellToWorld(cell).y,
                    TargetY = _board.CellToWorld(targetCell).y,
                });

                _context.NeedToChecks.Add(targetCell.Position);
                targetCell.SetEntity(cell.Entity);
                cell.ClearEntity();
                emptyCellIndex.Enqueue(y);
            }
            
            emptyCellIndex.Clear();

            return fallItems;
        }
    }
}