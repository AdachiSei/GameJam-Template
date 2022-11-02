using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SceneLoader))]
public class SceneLoaderEditor : Editor
{
    #region Private Static Member

    private static bool _isOpening;

    #endregion

    #region Override Method

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var sceneLoader = target as SceneLoader;
        var style = new GUIStyle(EditorStyles.label);
        style.richText = true;
        _isOpening = EditorGUILayout.Foldout(_isOpening, "Settings");
        if (_isOpening)
        {
            EditorGUILayout.Space();

            EditorGUILayout.LabelField("<b>Scene‚Ì–¼‘O‚ð‘S‚Ä‚Æ‚Á‚Ä‚­‚é</b>",style);
            if (GUILayout.Button("GetSceneName"))
            {
                sceneLoader.GetSceneName();
            }
        }
    }

    #endregion
}
