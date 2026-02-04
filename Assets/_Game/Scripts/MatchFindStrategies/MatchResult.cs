using System.Collections.Generic;
using _Game.Scripts.Enums;
using UnityEngine;

namespace _Game.Scripts.MatchStrategies
{
    public struct MatchResult
    {
        public MatchType Type;
        public HashSet<Vector2Int> Positions;
        public bool HasMatch;
    }
}