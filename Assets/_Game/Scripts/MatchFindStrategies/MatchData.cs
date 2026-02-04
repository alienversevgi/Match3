using UnityEngine;

namespace _Game.Scripts.MatchStrategies
{
    public struct MatchData
    {
        public Vector2Int Position;
        public bool IsMatch;

        public void Clear()
        {
            Position = Vector2Int.zero;
            IsMatch = false;
        }
    }
}