using System.Collections.Generic;
using Sirenix.OdinInspector;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace _Game.Scripts.Data
{
    [CreateAssetMenu(fileName = nameof(BoardData), menuName = Const.SOPath.SO_DATA_MENU_PATH + nameof(BoardData), order = 1)]
    public class BoardData : SerializedScriptableObject
    {
        [OnValueChanged(nameof(UpdateGrid))] public int Width;
        [OnValueChanged(nameof(UpdateGrid))] public int Height;

        public List<EntityData> Entities;

        [Space, HideLabel, Title("Selected Cell"), BoxGroup()]
        public CellData SelectedCell;

        [TableMatrix(DrawElementMethod = nameof(DrawCell), SquareCells = true)]
        public CellData[,] Grid;

#if UNITY_EDITOR
        [OnInspectorInit]
        private void Init()
        {
            if (Grid == null || Grid.Length == 0)
            {
                UpdateGrid();
            }
        }

        private void UpdateGrid()
        {
            Grid = new CellData[Width, Height];
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    Grid[x, y] = new CellData();
                }
            }
        }
        
        private CellData DrawCell(Rect rect, CellData value)
        {
            var size = 64f;
            var cellRect = new Rect(
                rect.x + (rect.width - size) * 0.5f,
                rect.y + (rect.height - size) * 0.5f,
                size,
                size
            );

            if (value == SelectedCell)
                EditorGUI.DrawRect(rect, new Color(0.2f, 0.6f, 1f, 0.4f));

            if (value.Entity != null && value.Entity.Sprite != null)
                GUI.DrawTexture(cellRect, value.Entity.Sprite.texture, ScaleMode.ScaleToFit);

            if (Event.current.type == EventType.MouseDown && cellRect.Contains(Event.current.mousePosition))
            {
                SelectedCell = value;
                GUI.changed = true;
                Event.current.Use();
            }

            return value;
        }
#endif
    }
}