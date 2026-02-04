using System.Collections.Generic;
using _Game.Scripts.Board;
using _Game.Scripts.Enums;
using _Game.Scripts.MatchStrategies;
using Cysharp.Threading.Tasks;

namespace _Game.Scripts.Pipeline.Processors
{
    public class MatchStep : IBoardStep
    {
        private readonly Dictionary<MatchType, IMatch> _handlers = new()
        {
            { MatchType.Line, new LineMatch() }
            // T,L
        };

        public async UniTask Execute(BoardContext context)
        {
            float waitDuration = 0;
            for (int i = 0; i < context.Matches.Count; i++)
            {
                var result = context.Matches[i];
                var handler = _handlers[result.Type];
                var hasResult = await _handlers[result.Type].Execute(context, result);
                
                if (hasResult && waitDuration < handler.Duration)
                    waitDuration = handler.Duration;
            }

            await UniTask.WaitForSeconds(waitDuration);
        }
    }
}