using System.Collections.Generic;
using _Game.Scripts.Board;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Game.Scripts.Pipeline.Processors
{
    public class RemoveStep : IBoardStep
    {
        public async UniTask Execute(BoardContext context)
        {
            for (int i = 0; i < context.Matches.Count; i++)
            {
                var result = context.Matches[i];
                foreach (var position in result.Positions)
                {
                    var cell = context.Board.GetCell(position);
                    if(cell.Entity is null)
                        continue;
              
                    cell.Entity.Explode().Forget();
                    cell.ClearEntity();
                }
            }

            await UniTask.CompletedTask;
        }
    }
}