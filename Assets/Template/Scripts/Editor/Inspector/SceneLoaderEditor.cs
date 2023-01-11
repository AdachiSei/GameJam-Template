using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Linq;

[CustomEditor(typeof(SceneLoader))]
public class SceneLoaderEditor : Editor
{
    #region Private Member

    bool _isSetting;
    bool _isRemoving;

    #endregion

    #region Const Member

    private const int OFFSET = 1;

    #endregion

    #region Override Method

    public override void OnInspectorGUI()
    {
        EditorGUI.BeginDisabledGroup(true);
        EditorGUILayout.ObjectField("Editor", MonoScript.FromScriptableObject(this), typeof(MonoScript), false);
        EditorGUI.EndDisabledGroup();

        base.OnInspectorGUI();

        var sceneLoader = target as SceneLoader;
        var style = new GUIStyle(EditorStyles.label);
        style.richText = true;

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("<b>Sceneの名前を全てとってくる</b>", style);
        if (GUILayout.Button("GetSceneName"))
        {
            GetSceneName();
            _isSetting = true;
            _isRemoving = sceneLoader.SceneNames.Contains("RemoveThis");
        }

        if (_isSetting && _isRemoving)
        {
            _isRemoving = sceneLoader.SceneNames.Contains("RemoveThis");
            EditorGUILayout.HelpBox("This is a warning-help message.", MessageType.Warning);
        }
    }

    #endregion

    #region Private Method

    /// <summary>
    /// Assetフォルダの中にあるSceneの名前を全てとってくる関数
    /// </summary>
    private void GetSceneName()
    {
        var sceneLoader = target as SceneLoader;
        var offset = 0;
        var isPlaying = EditorApplication.isPlaying;

        if (isPlaying == false)offset = 1;

        sceneLoader.ResizeSceneNames(EditorBuildSettings.scenes.Length + offset);
        List<string> sceneNames = new();

        //BuildSetingsに入っているSceneの名前を全てとってくる
        foreach (var scene in EditorBuildSettings.scenes)
        {
            var name = Path.GetFileNameWithoutExtension(scene.path);
            sceneNames.Add(name);
        }

        //重複している要素を消してから並び替え
        sceneNames = new(sceneNames.Distinct());
        sceneNames = new(sceneNames.OrderBy(name =>
        {
            var sceneNum = name.Split("Scene");
            if (sceneNum[OFFSET] == "")
            {
                sceneNum = new[] { sceneNum[0], "0" };
            }
            return int.Parse(sceneNum[OFFSET]);
        }));

        for (int i = 0; i < sceneNames.Count; i++)
        {
            sceneLoader.AddSceneName(i, sceneNames[i]);
        }
        if (isPlaying == false)
        {
            sceneLoader.AddSceneName(sceneLoader.SceneNames.Length - offset, "RemoveThis");
        }
    }

    #endregion
}
