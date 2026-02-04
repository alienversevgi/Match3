using System.Collections.Generic;
using _Game.Scripts.Board;
using _Game.Scripts.MatchStrategies;
using UnityEngine;
using Zenject;

namespace _Game.Scripts.Handlers
{
    public class MatchCheckHandler
    {
        [Inject] private BoardManager _board;
        private List<IMatchFindStrategy> _findStrategies;
        
        public void Initialize()
        {
            _findStrategies = new List<IMatchFindStrategy>()
            {
                new LineMatchFindStrategy(_board)
            };
        }

        public bool Check(Vector2Int position)
        {
            for (int i = 0; i < _findStrategies.Count; i++)
            {
                var result = _findStrategies[i].Find(position);
                if (result.HasMatch)
                {
                    _board.Context.Matches.Add(result);
                    return true;
                }
            }

            return false;
        }
    }
}