using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Game.Scripts.Data
{
    [CreateAssetMenu(fileName = nameof(EntityData), menuName = Const.SOPath.SO_DATA_MENU_PATH + nameof(EntityData), order = 1)]
    public class EntityData : SerializedScriptableObject
    {
        public Guid ID;
        public Sprite Sprite;
        // public BaseEntity Prefab;

        private void OnEnable()
        {
            if (ID == Guid.Empty)
                ID = Guid.NewGuid();
        }
    }
}