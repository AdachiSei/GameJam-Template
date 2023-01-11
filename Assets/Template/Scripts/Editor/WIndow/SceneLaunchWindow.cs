using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.Linq;
using System.Collections.Generic;
using System.IO;

/// <summary>
/// https://www.urablog.xyz/entry/2021/10/13/070000
/// </summary>
public class SceneLaunchWindow : EditorWindow
{
    private string[] _buildScenePaths = null;
    private string[] _othersScenePaths = null;
    private Vector2 _scrollPosition = Vector2.zero;

    [MenuItem("AllScene/Scene Launcher")]
    private static void ShowWindow()
    {
        // ウィンドウを表示！
        GetWindow<SceneLaunchWindow>("Scene Launcher");
    }

    private void OnFocus() =>
        Reload();  

    private void Reload()
    {
        _buildScenePaths =
            EditorBuildSettings
                .scenes
                .Select(scene => scene.path)
                .ToArray();

        // コードを分かりやすくするため、一度 ToArray() を使ってローカル変数化してるけど、
        // 別にまとめちゃってもいいよ(……というか処理的にはそっちのほうがいいはず),
        var guids =
            AssetDatabase
                .FindAssets("t:Scene", new string[] { "Assets" });
        var paths =
            guids
                .Select(guid => AssetDatabase.GUIDToAssetPath(guid));
        _othersScenePaths =
            paths
                .Where(path =>
                        !_buildScenePaths
                            .Any(buildPath =>
                                    buildPath == path))
                .ToArray();
    }

    private void OnGUI()
    {
        // OnFocus() より前に呼ばれる対策(あるのかな？)
        var isBuildScene = _buildScenePaths == null;
        var isOthersScene = _othersScenePaths == null;
        if (isBuildScene && isOthersScene)
            Reload();

        // この波括弧は見やすくするためだけにあるよ.
        EditorGUI.BeginDisabledGroup(EditorApplication.isPlaying);
        {
            // この波括弧も見やすくするためだけにあるよ.
            _scrollPosition =
                EditorGUILayout.BeginScrollView(_scrollPosition);
            {
                EditorGUILayout.LabelField("Scenes in build");
                GenerateButtons(_buildScenePaths);

                GUILayout.Space(10f);

                EditorGUILayout.LabelField("Others");
                GenerateButtons(_othersScenePaths);
            }
            EditorGUILayout.EndScrollView();

        }
        EditorGUI.EndDisabledGroup();
    }

    private void GenerateButtons(string[] scenePaths)
    {
        if (scenePaths != null && scenePaths.Length > 0)
        {
            foreach (var path in scenePaths)
            {
                var name =
                    Path.GetFileNameWithoutExtension(path);
                if (GUILayout.Button(name))
                {
                    var isSaving =
                        EditorSceneManager
                            .SaveModifiedScenesIfUserWantsTo
                                (GetDirtyScenes());
                    if (isSaving)
                    {
                        EditorSceneManager.OpenScene(path);
                    }
                }
                GUILayout.Space(5.0f);
            }
        }
        else EditorGUILayout.LabelField("シーンがありません");
    }

    private Scene[] GetDirtyScenes()
    {
        var scenes = new List<Scene>();
        for (int i = 0; i < SceneManager.sceneCount; ++i)
        {
            var scene = SceneManager.GetSceneAt(i);
            if (scene.isDirty) scenes.Add(scene);
        }
        return scenes.ToArray();
    }
} 