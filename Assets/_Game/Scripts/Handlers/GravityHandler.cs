using System.Collections.Generic;
using _Game.Scripts.Board;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace _Game.Scripts.Handlers
{
    public class GravityHandler
    {
        private const float GRAVITY = 20f;

        public bool IsFinished => _items.Count == 0;

        private HashSet<FallItem> _items;

        public GravityHandler()
        {
            _items = new HashSet<FallItem>();
        }

        public void AddItem(FallItem item)
        {
            _items.Add(item);
            Fall(item).Forget();
        } 
        private void RemoveItem(FallItem item) => _items.Remove(item);

        private async UniTask Fall(FallItem item)
        {
            float h = Mathf.Abs(item.StartY - item.TargetY);
            float duration = Mathf.Sqrt(2f * h / GRAVITY);

            await item.Entity.transform.DOMoveY(item.TargetY, duration).SetEase(Ease.InCubic);
            RemoveItem(item);
        }
    }
}