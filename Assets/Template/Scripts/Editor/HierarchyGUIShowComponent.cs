using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public static class HierarchyGUIShowComponent
{
    /// <summary>
    /// アイコンのサイズ
    /// </summary>
    private const int ICON_SIZE = 16;

    [InitializeOnLoadMethod]
    private static void Initialize()
    {
        EditorApplication.hierarchyWindowItemOnGUI += OnGUI;
    }
    
    private static void OnGUI(int instanceID, Rect selectionRect)
    {
        // instanceID をオブジェクト参照に変換
        var go = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
        if (go == null)
        {
            return;
        }

        // オブジェクトが所持しているコンポーネント一覧を取得
        var components = go.GetComponents<Component>();
        List<Component> componentsList = new();
        foreach (var i in components)
        {
            bool[] isType =
            {
                i.GetType() == typeof(Transform),
                i.GetType() == typeof(RectTransform),
                i.GetType() == typeof(CanvasRenderer),
                i.GetType() == typeof(CanvasScaler),
                i.GetType() == typeof(StandaloneInputModule),
                i.GetType() == typeof(GraphicRaycaster)
            };

            for (int j = 0; j < isType.Length; j++)
            {
                if (isType[j])
                {
                    break;
                }
                if(j == isType.Length - 1)componentsList.Add(i);
                continue;
            }
        }
        if (componentsList.Count == 0)
        {
            return;
        }

        selectionRect.x = selectionRect.xMax - ICON_SIZE * componentsList.Count;
        selectionRect.width = ICON_SIZE;

        foreach (var component in componentsList)
        {
            // コンポーネントのアイコン画像を取得
            var texture2D = AssetPreview.GetMiniThumbnail(component);
            GUI.DrawTexture(selectionRect, texture2D);
            selectionRect.x += ICON_SIZE;
        }
    }
}
