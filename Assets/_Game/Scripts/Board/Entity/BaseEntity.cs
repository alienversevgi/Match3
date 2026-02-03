using System;
using _Game.Scripts.Data;
using UnityEngine;

namespace _Game.Scripts.Board.Entity
{
    public abstract class BaseEntity : MonoBehaviour
    {
        protected EntityData Data { get; private set; }
        [field: SerializeField] public Guid ID { get; private set; }
        [field: SerializeField] public Vector2Int Position { get; private set; }

        public virtual void Initialize(EntityData itemData, Vector2Int position)
        {
            Data = itemData;
            Position = position;
            ID = Data.ID;
            SetPosition(position);
        }

        public abstract void Merge();

        public void SetPosition(Vector2Int position) => Position = position;
    }
}