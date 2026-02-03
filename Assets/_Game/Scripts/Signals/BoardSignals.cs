using UnityEngine;

namespace _Game.Signals
{
    public static class BoardSignals
    {
        public struct Swipe
        {
            public Vector3 Position;
            public Vector2Int Direction;
        }

        public struct Click
        {
            public Vector3 Position;
        }
    }
}