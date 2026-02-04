using System;
using _Game.Misc;
using _Game.Scripts.Data;
using _Game.Scripts.View.Entity;
using UnityEngine;

namespace _Game.Scripts.Board.Entity
{
    public abstract class BaseEntity : MonoBehaviour
    {
        public class Pool : CustomMonoMemoryPool<BaseEntity>
        {
        }
        
        protected EntityData BaseData { get; private set; }
        protected BaseEntityView BaseView { get; private set; }
        [field: SerializeField] public Guid ID { get; private set; }
        [field: SerializeField] public Vector2Int Position { get; private set; }
        [field: SerializeField] public string PoolID { get; private set; }

        public virtual void Initialize(EntityData entityData, Vector2Int position)
        {
            BaseData = entityData;
            BaseView = this.GetComponent<BaseEntityView>();
            Position = position;
            ID = BaseData.ID;
            SetPosition(position);
            BaseView.Initialize(BaseData);
        }

        public abstract void Merge();

        public void SetPosition(Vector2Int position) => Position = position;
    }

    public abstract class BaseEntity<D, V> : BaseEntity
        where D : EntityData, new()
        where V : BaseEntityView
    {
        public D Data { get; private set; }
        public V View { get; private set; }

        public override void Initialize(EntityData entityData, Vector2Int position)
        {
            base.Initialize(entityData, position);
            Data = BaseData as D;
            View = BaseView as V;
        }
    }
}