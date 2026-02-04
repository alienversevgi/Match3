using System.Collections.Generic;
using _Game.Scripts.Handlers;
using _Game.Scripts.MatchStrategies;
using UnityEngine;
using Zenject;

namespace _Game.Scripts.Board
{
    public class BoardContext
    {
        [Inject] public BoardManager Board { get; }
        [Inject] public MatchCheckHandler MatchCheck { get; }

        public GravityHandler GravityHandler;
        public bool IsRunning;
        public List<MatchResult> Matches;
        public HashSet<Vector2Int> NeedToChecks;
        public HashSet<int> EffectedRows;
        public Dictionary<int, Queue<int>> EmptyCells;

        public BoardContext()
        {
            GravityHandler = new GravityHandler();
            EffectedRows = new HashSet<int>();
            NeedToChecks = new HashSet<Vector2Int>();
            Matches = new List<MatchResult>();
            IsRunning = false;
        }

        public void Clear()
        {
            EffectedRows.Clear();
            Matches.Clear();
        }
    }
}