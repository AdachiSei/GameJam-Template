using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// ヒエラルキー上のゲームオブジェクトの横に
/// つけているコンポーネントのアイコンを表示するエディター拡張
/// </summary>
public static class HierarchyGUIShowComponent
{
    #region Const Member

    /// <summary>
    /// アイコンのサイズ
    /// </summary>
    private const int ICON_SIZE = 16;

    private const int OFFSET = 1;

    #endregion

    #region Unity Methods

    [InitializeOnLoadMethod]
    private static void Initialize()
    {
        EditorApplication.hierarchyWindowItemOnGUI += OnGUI;
    }

    private static void OnGUI(int instanceID, Rect selectionRect)
    {
        // instanceID をオブジェクト参照に変換
        var go = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
        if (go == null) return;

        // オブジェクトが所持しているコンポーネント一覧を取得
        var components = go.GetComponents<Component>();
        List<Component> componentsList = new();
        foreach (var component in components)
        {
            bool[] isType =
            {
                //false
                component?.GetType() == typeof(Transform),
                component?.GetType() == typeof(RectTransform),
                component?.GetType() == typeof(CanvasRenderer),
                component?.GetType() == typeof(CanvasScaler),
                component?.GetType() == typeof(StandaloneInputModule),
                component?.GetType() == typeof(GraphicRaycaster),
                component?.GetType() == typeof(AudioListener),
            };

            for (int index = 0; index < isType.Length; index++)
            {
                if (isType[index]) break;
                if (index == isType.Length - OFFSET) componentsList.Add(component);
                continue;
            }
        }
        //if (componentsList[0] == null) return;
        if (componentsList.Count == 0) return;

        selectionRect.x = selectionRect.xMax - ICON_SIZE * componentsList.Count;
        selectionRect.width = ICON_SIZE;

        foreach (var component in componentsList)
        {
            // コンポーネントのアイコン画像を取得
            var texture2D = AssetPreview.GetMiniThumbnail(component);
            //texture2D = AssetDatabase.LoadAssetAtPath("Assets/AssetStoreTools/unknown.png", typeof(Texture2D)) as Texture2D;
            GUI.DrawTexture(selectionRect, texture2D);
            selectionRect.x += ICON_SIZE;
        }
    }

    #endregion
}
