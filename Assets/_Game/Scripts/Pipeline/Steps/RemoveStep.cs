using System.Collections.Generic;
using _Game.Scripts.Board;
using _Game.Scripts.MatchStrategies;
using Cysharp.Threading.Tasks;

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
                    context.Board.GetEntity(position).Explode().Forget();
                    context.Board.GetCell(position).ClearEntity();
                }
            }

            context.IsRunning = false;
            await UniTask.CompletedTask;
        }
    }
}