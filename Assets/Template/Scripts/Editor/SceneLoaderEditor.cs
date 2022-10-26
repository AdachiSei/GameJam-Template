using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SceneLoader))]
public class SceneLoaderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var sceneLoader = target as SceneLoader;
        if (GUILayout.Button("GetSceneName"))
        {
            sceneLoader.GetSceneName();
        }
        EditorGUI.EndDisabledGroup();
    }
}
