using _Game.Scripts.Board;
using _Game.Scripts.Enums;
using Cysharp.Threading.Tasks;

namespace _Game.Scripts.MatchStrategies
{
    public class LineMatch : IMatch
    {
        public MatchType Type => MatchType.Line;
        public float Duration => .2f;

        public async UniTask<bool> Execute(BoardContext context, MatchResult result)
        {
            if (result.Type != MatchType.Line)
                return false;
            
            foreach (var match in result.Positions)
            {
                context.EffectedRows.Add(match.x);
                context.Board.GetEntity(match).Merge();
            }

            return true;
        }
    }
}