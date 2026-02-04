using _Game.Scripts.Board;
using Cysharp.Threading.Tasks;

namespace _Game.Scripts.Pipeline.Processors
{
    public class CheckStep : IBoardStep
    {
        public async UniTask Execute(BoardContext context)
        {
            foreach (var position in context.NeedToChecks)
            {
                context.MatchCheck.Check(position);
            }
            
            context.NeedToChecks.Clear();
            context.IsRunning = context.Matches.Count > 0;
        }
    }
}