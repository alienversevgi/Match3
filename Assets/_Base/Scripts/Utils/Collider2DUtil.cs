using UnityEngine;

namespace _Base.Scripts.Utils
{
    public static class Collider2DUtil
    {
        public static Vector3 GetRandomPoint(this BoxCollider2D collider)
        {
            Vector2 size = collider.size;
            Vector2 offset = collider.offset;

            float randomX = Random.Range(-size.x / 2f, size.x / 2f);
            float randomY = Random.Range(-size.y / 2f, size.y / 2f);

            Vector2 localPoint = new Vector2(randomX, randomY) + offset;
            return collider.transform.TransformPoint(localPoint);
        }
    }
}