using _Game.Scripts.Board;
using _Game.Scripts.Pipeline;
using Cysharp.Threading.Tasks;

namespace _Game.Scripts.MatchStrategies
{

    public enum MatchType
    {
        Line
    }
    
    public interface IMatch
    {
        MatchType Type { get; }
        UniTask Execute(BoardContext context, MatchResult result);
    }
}