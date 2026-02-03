using System.Collections.Generic;
using GamePlay.Components.Randomizer;
using Sirenix.OdinInspector;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace _Game.Scripts.Data
{
    public enum TestGridType
    {
        A,
        B,
        C,
        D,
        E
    }
    [CreateAssetMenu(fileName = nameof(BoardData), menuName = Const.SOPath.SO_DATA_MENU_PATH + nameof(BoardData),
        order = 1)]
    public class BoardData : SerializedScriptableObject
    {
        [OnValueChanged(nameof(UpdateGrid))] public int Width;
        [OnValueChanged(nameof(UpdateGrid))] public int Height;

        public List<EntityData> Entities;

        [Space(10)] [HideLabel,BoxGroup("Selected Cell")] [InlineProperty]
        public CellData SelectedCell;

        [Space(10)] [TableMatrix(DrawElementMethod = nameof(DrawCell), SquareCells = true)]
        public CellData[,] Grid;
        
        [FoldoutGroup("Test Grid Filler")]
        public TestGridType[,] TestGridFiller;
        
        [FoldoutGroup("Test Grid Filler")]
        public SerializedDictionary<TestGridType,EntityData> TestGridDatas;
        
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

        [Button]
        private void ApplyTestGrid()
        {
            if (TestGridFiller == null || TestGridFiller.Length == 0)
            {
                TestGridFiller = new TestGridType[8, 10]
                {
                    { TestGridType.A, TestGridType.D, TestGridType.B, TestGridType.E, TestGridType.C, TestGridType.A, TestGridType.D, TestGridType.B, TestGridType.C, TestGridType.A },
                    { TestGridType.C, TestGridType.A, TestGridType.E, TestGridType.B, TestGridType.D, TestGridType.C, TestGridType.A, TestGridType.E, TestGridType.B, TestGridType.C },
                    { TestGridType.B, TestGridType.E, TestGridType.C, TestGridType.D, TestGridType.A, TestGridType.B, TestGridType.E, TestGridType.C, TestGridType.D, TestGridType.A },
                    { TestGridType.D, TestGridType.B, TestGridType.A, TestGridType.C, TestGridType.E, TestGridType.D, TestGridType.B, TestGridType.A, TestGridType.C, TestGridType.E },
                    { TestGridType.E, TestGridType.C, TestGridType.D, TestGridType.A, TestGridType.B, TestGridType.E, TestGridType.C, TestGridType.D, TestGridType.A, TestGridType.B },
                    { TestGridType.A, TestGridType.D, TestGridType.B, TestGridType.E, TestGridType.C, TestGridType.A, TestGridType.D, TestGridType.B, TestGridType.E, TestGridType.C },
                    { TestGridType.C, TestGridType.A, TestGridType.E, TestGridType.B, TestGridType.D, TestGridType.C, TestGridType.A, TestGridType.E, TestGridType.B, TestGridType.D },
                    { TestGridType.B, TestGridType.E, TestGridType.C, TestGridType.D, TestGridType.A, TestGridType.B, TestGridType.E, TestGridType.C, TestGridType.D, TestGridType.A }
                };
            }

            for (int y = Height - 1; y >= 0; y--)
            {
                for (int x = 0; x < Width; x++)
                {
                    Grid[x, y].Entity = TestGridDatas[TestGridFiller[x, y]];
                }
            }
        }
#endif
    }
}