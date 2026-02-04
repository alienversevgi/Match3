using _Game.Scripts.Board.Entity;
using UnityEngine;

namespace _Game.Scripts.Board
{
    public class Cell
    {
        public Vector2Int Position { get; set; }
        public BaseEntity Entity { get; set; }

        public void Initialize(Vector2Int position)
        {
            Position = position;
        }

        public void SetEntity(BaseEntity entity)
        {
            Entity = entity;
            Entity.name = $"E_{Position}";
            Entity.SetPosition(Position);
        }

        public void ClearEntity()
        {
            Entity = null;
        }
        
        public bool HasEntity => Entity != null;
    }
}