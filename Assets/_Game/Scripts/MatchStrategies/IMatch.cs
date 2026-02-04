using _Game.Scripts.Board;
using _Game.Scripts.Enums;
using Cysharp.Threading.Tasks;

namespace _Game.Scripts.MatchStrategies
{
    public interface IMatch
    {
        MatchType Type { get; }
        float Duration { get; }
        UniTask<bool> Execute(BoardContext context, MatchResult result);
    }
}