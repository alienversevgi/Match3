using System.Linq;
using UnityEngine;
using Zenject;

namespace _Game.Misc
{
    public class CustomMonoMemoryPool<TValue> : MonoMemoryPool<TValue> where TValue : Component
    {
        protected override void OnCreated(TValue item)
        {
            item.gameObject.name += $"_{NumTotal}";
            base.OnCreated(item);
        }
    }
}