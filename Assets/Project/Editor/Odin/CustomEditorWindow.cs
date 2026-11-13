using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;
using System.Linq;
using System;
using Sirenix.Utilities.Editor;
using Sirenix.Utilities;

public class CustomEditorWindow : OdinMenuEditorWindow
{
    [MenuItem("Tools/EnemyTable")]
    private static void Open()
    {
        var window = GetWindow<CustomEditorWindow>();
        window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 500);
    }
    protected override OdinMenuTree BuildMenuTree()
    {
        var tree = new OdinMenuTree(true);
        tree.DefaultMenuStyle.IconSize = 28.00f;
        tree.Config.DrawSearchToolbar = true;

        // Adds the character overview table.
        //CharacterOverview.Instance.UpdateCharacterOverview();
        //tree.Add("Characters", new CharacterTable(CharacterOverview.Instance.AllCharacters));

        EnemyOverview.Instance.UpdateEnemyOverview();
        tree.Add("Enemies", new EnemyTable(EnemyOverview.Instance.AllEnemies));

        // Adds all characters.
        tree.AddAllAssetsAtPath("Enemies", "Assets/Project/Data/Enemies", typeof(EnemyDefinition), true, true)
            .ForEach(this.AddDragHandles);

        // Add drag handles to items, so they can be easily dragged into the inventory if characters etc...
        tree.EnumerateTree().Where(x => x.Value as EnemyDefinition).ForEach(AddDragHandles);

        return tree;
    }

    private void AddDragHandles(OdinMenuItem menuItem)
    {
        menuItem.OnDrawItem += x => DragAndDropUtilities.DragZone(menuItem.Rect, menuItem.Value, false, false);
    }

    protected override void OnBeginDrawEditors()
    {
        var selected = this.MenuTree.Selection.FirstOrDefault();
        var toolbarHeight = this.MenuTree.Config.SearchToolbarHeight;

        // Draws a toolbar with the name of the currently selected menu item.
        SirenixEditorGUI.BeginHorizontalToolbar(toolbarHeight);
        {
            if (selected != null)
            {
                GUILayout.Label(selected.Name);
            }

            if (SirenixEditorGUI.ToolbarButton(new GUIContent("Create Enemy")))
            {
                ScriptableObjectCreator.ShowDialog<EnemyDefinition>("Assets/Project/Data/Enemies", obj =>
                {
                    obj.name = obj.name;
                    base.TrySelectMenuItemWithObject(obj); // Selects the newly created item in the editor
                });
            }
        }
        SirenixEditorGUI.EndHorizontalToolbar();
    }
}

/*
private const string ItemsFolder = "Assets/Project/Data";

    [MenuItem("Tools/Item Editor")]
    private static void OpenWindow()
    {
        GetWindow<CustomEditorWindow>().Show();
    }

    protected override OdinMenuTree BuildMenuTree()
    {
        var tree = new OdinMenuTree();

        // Find all ScriptableObject assets in the folder
        string[] guids = AssetDatabase.FindAssets(
            "t:ScriptableObject",
            new[] { ItemsFolder }
        );

        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);

            ScriptableObject asset =
                AssetDatabase.LoadAssetAtPath<ScriptableObject>(path);

            // Include ItemDataSOScript and all derived classes
            if (asset is EnemyDefinition)
            {
                tree.Add($"{asset.GetType().Name}/{asset.name}", asset);
            }
        }

        return tree;
    }
*/
