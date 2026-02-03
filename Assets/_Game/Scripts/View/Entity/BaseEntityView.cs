using _Game.Scripts.Data;
using UnityEngine;

namespace _Game.Scripts.View.Entity
{
    public class BaseEntityView : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        private EntityData _data;

        public void Initialize(EntityData data)
        {
            _data = data;
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteRenderer.sprite = _data.Sprite;
        }
    }
}