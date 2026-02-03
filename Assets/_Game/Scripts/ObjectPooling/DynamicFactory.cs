using _Game.Misc;
using UnityEngine;
using Zenject;

namespace _Game.Factories
{
    public class DynamicFactory<TPool, KElement> where TPool : CustomMonoMemoryPool<KElement> where KElement : MonoBehaviour
    {
        [Inject] private DiContainer _diContainer;

        public KElement Spawn(Vector3 position, Quaternion rotation, Transform parent = null)
        {
            var pool = _diContainer.Resolve<TPool>();
            var element = pool.Spawn();
            element.transform.position = position;
            element.transform.rotation = rotation;

            if (parent != null)
                element.transform.SetParent(parent);

            return element;
        }
        
        public void Dispose(KElement item)
        {
            var pool = _diContainer.Resolve<TPool>();
            pool.Despawn(item);
        }
        
        public KElement Spawn(KElement item, string id, Vector3 position, Quaternion rotation, Transform parent = null)
        {
            var pool = _diContainer.TryResolveId<TPool>(id);
            if (!_diContainer.HasBindingId<TPool>(id))
                Bind(item, id);
            
            var element = pool.Spawn();
            element.transform.position = position;
            element.transform.rotation = rotation;

            if (parent != null)
                element.transform.SetParent(parent);

            return element;
        }

        public void Dispose(KElement item, string id)
        {
            var pool = _diContainer.TryResolveId<TPool>(id);
            pool.Despawn(item);
        }

        public void Bind(KElement item, string id)
        {
            _diContainer.BindMemoryPool<KElement, TPool>()
                .WithId(id)
                .WithInitialSize(10)
                .FromComponentInNewPrefab(item)
                .WithGameObjectName(id)
                .UnderTransformGroup($"{id}_PoolHolder");

            _diContainer.TryResolveId<TPool>(id);
        }
    }
}