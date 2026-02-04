using System.Collections.Generic;
using _Game.Scripts.MatchStrategies;

namespace _Game.Scripts.Board
{
    public class BoardContext
    {
        public BoardManager Board;
        public List<MatchResult> Matches;
        public HashSet<int> EffectedRows;
        public bool IsRunning;
    }
}