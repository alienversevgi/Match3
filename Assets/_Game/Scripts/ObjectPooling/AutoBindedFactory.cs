using System.Linq;
using _Game.Misc;
using UnityEngine;
using Zenject;

namespace _Game.Factories
{
    public class AutoBindedFactory<TPool, KElement> where TPool : CustomMonoMemoryPool<KElement>
        where KElement : MonoBehaviour
    {
        [Inject] private DiContainer _diContainer;
        [Inject] private TPool _pool;

        public KElement Spawn()
        {
            var element = _pool.Spawn();
            element.transform.position = Vector3.zero;
            element.transform.rotation = Quaternion.identity;

            return element;
        }

        public KElement Spawn(Vector3 position, Quaternion rotation, Transform parent = null)
        {
            var element = _pool.Spawn();
            element.transform.position = position;
            element.transform.rotation = rotation;

            if (parent != null)
                element.transform.SetParent(parent);

            return element;
        }

        public void Dispose(KElement item)
        {
            if(_pool.InactiveItems.Contains(item))
                return;
            
            _pool.Despawn(item);
        }
    }
}