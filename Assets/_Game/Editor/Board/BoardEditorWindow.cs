using System.Linq;
using _Game.Scripts.Data;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace _Game.Editor.Board
{
    public class BoardEditorWindow : OdinMenuEditorWindow
    {
        public string Version;

        [MenuItem("Tools/Board Editor")]
        public static void Open()
        {
            var window = GetWindow<BoardEditorWindow>();
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(1000, 500);
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree(true);
            tree.DefaultMenuStyle.IconSize = 28.00f;
            tree.Config.DrawSearchToolbar = true;

            tree.AddAllAssetsAtPath(nameof(BoardEditorPath.Board), BoardEditorPath.Board, typeof(BoardData), true, true);
            tree.AddAllAssetsAtPath(nameof(BoardEditorPath.Entity), BoardEditorPath.Entity, typeof(EntityData), true, true);
            tree.SortMenuItemsByName();

            HandleEntityDataTab(tree);

            return tree;
        }

        private void HandleEntityDataTab(OdinMenuTree tree)
        {
            tree.EnumerateTree()
                .Where(menuItem => menuItem.Value is EntityData)
                .ForEach(item =>
                {
                    AddSpriteIconForEntityData(item);
                    AddDraggableForEntityData(item);
                });
        }

        private static void AddSpriteIconForEntityData(OdinMenuItem item)
        {
            var data = item.Value as EntityData;
            if (data == null || data.Sprite == null)
                return;

            item.Icon = data.Sprite.texture;
        }

        private static void AddDraggableForEntityData(OdinMenuItem item)
        {
            item.OnDrawItem += rect =>
            {
                var e = Event.current;
                if (e.type == EventType.MouseDrag && rect.Rect.Contains(e.mousePosition))
                {
                    DragAndDrop.PrepareStartDrag();
                    DragAndDrop.objectReferences = new Object[] { item.Value as EntityData };
                    DragAndDrop.StartDrag(item.Name);
                    e.Use();
                }
            };
        }

        protected override void OnBeginDrawEditors()
        {
            var selected = this.MenuTree.Selection.FirstOrDefault();
            var toolbarHeight = this.MenuTree.Config.SearchToolbarHeight;
            SirenixEditorGUI.Title("Board Editor", Version, TextAlignment.Center, true);

            SirenixEditorGUI.BeginHorizontalToolbar(toolbarHeight);
            {
                if (selected != null)
                {
                    GUILayout.Label(selected.Name);
                }
            }
            SirenixEditorGUI.EndHorizontalToolbar();
        }
    }
}