using System.Collections.Generic;
using _Game.Scripts.Board;
using _Game.Scripts.Enums;
using UnityEngine;

namespace _Game.Scripts.MatchStrategies
{
    public class LineMatchFindStrategy : IMatchFindStrategy
    {
        public MatchType Type => MatchType.Line;
        public BoardManager Board { get; set; }

        private MatchData[] _horizontalMatches;
        private MatchData[] _verticalMatches;

        public LineMatchFindStrategy(BoardManager board)
        {
            Board = board;
            _horizontalMatches = new MatchData[Board.Width];
            _verticalMatches = new MatchData[Board.Height];
        }

        public MatchResult Find(Vector2Int position)
        {
            MatchResult result = new MatchResult()
            {
                Type = Type,
                Positions = new HashSet<Vector2Int>()
            };

            int horizontalMatch = CheckMatchInLine(_horizontalMatches, position, new Vector2Int(1, 0));
            int verticalMatch = CheckMatchInLine(_verticalMatches, position, new Vector2Int(0, 1));

            result.Positions.Add(position);

            if (horizontalMatch >= 2)
            {
                for (int i = 0; i < _horizontalMatches.Length; i++)
                {
                    if (!_horizontalMatches[i].IsMatch)
                        break;

                    result.Positions.Add(_horizontalMatches[i].Position);
                }
            }

            if (verticalMatch >= 2)
            {
                for (int i = 0; i < _verticalMatches.Length; i++)
                {
                    if (!_verticalMatches[i].IsMatch)
                        break;
                    result.Positions.Add(_verticalMatches[i].Position);
                }
            }

            result.HasMatch = result.Positions.Count >= 3;

            ClearMatchData();
            return result;
        }

        private void ClearMatchData()
        {
            for (int i = 0; i < _horizontalMatches.Length; i++)
                _horizontalMatches[i].Clear();

            for (int i = 0; i < _verticalMatches.Length; i++)
                _verticalMatches[i].Clear();
        }

        private int CheckMatchInLine(MatchData[] matchArray, Vector2Int position, Vector2Int direction)
        {
            int matchCount = CheckMatchInLineSide(matchArray, position, direction, 1, 0);
            matchCount = CheckMatchInLineSide(matchArray, position, direction, -1, matchCount);

            return matchCount;
        }

        private int CheckMatchInLineSide(MatchData[] matchArray, Vector2Int position, Vector2Int direction, int start,
            int matchCount)
        {
            var index = start;
            var itemID = Board.GetEntity(position).ID;

            while (Board.IsInBounds(position + direction * index))
            {
                Vector2Int cellPosition = position + direction * index;
                var cell = Board.GetCell(cellPosition);
                if (itemID == cell.Entity.ID)
                {
                    matchArray[matchCount].Position = position + direction * index;
                    matchArray[matchCount].IsMatch = true;
                    matchCount++;
                }
                else
                {
                    break;
                }

                index += start;
            }

            return matchCount;
        }
    }
}