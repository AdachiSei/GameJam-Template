using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SceneLoader))]
public class SceneLoaderEditor : Editor
{
    static bool _isOpening;

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

            EditorGUILayout.LabelField("<b>Scene�̖��O��S�ĂƂ��Ă���</b>", style, GUILayout.ExpandHeight(true));
            if (GUILayout.Button("GetSceneName"))
            {
                sceneLoader.GetSceneName();
            }
            EditorGUI.EndDisabledGroup();

        }
    }
}