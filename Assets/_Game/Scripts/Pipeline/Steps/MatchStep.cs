using System.Collections.Generic;
using _Game.Scripts.Board;
using _Game.Scripts.MatchStrategies;
using Cysharp.Threading.Tasks;

namespace _Game.Scripts.Pipeline.Processors
{
    public class MatchStep : IBoardStep
    {
        private readonly Dictionary<MatchType, IMatch> _handlers = new()
        {
            { MatchType.Line, new LineMatch() }
        };

        public async UniTask Execute(BoardContext context)
        {
            for (int i = 0; i < context.Matches.Count; i++)
            {
                var result = context.Matches[i];
                _handlers[result.Type].Execute(context, result);
            }

            context.IsRunning = false;
            await UniTask.CompletedTask;
        }
    }
}