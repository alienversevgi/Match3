using _Game.Scripts.Board;
using UnityEngine;

namespace _Game.Scripts.MatchStrategies
{
    public interface IMatchFindStrategy
    {
        MatchType Type { get; }
        BoardManager Board { get; set; }
        MatchResult Find(Vector2Int position);
    }
}