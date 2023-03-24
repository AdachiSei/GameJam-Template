using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Linq;

[CustomEditor(typeof(SceneLoader))]
public class SceneLoaderInspector : Editor
{
    #region Member Variables

    private bool _isSetting = false;
    private bool _isRemoving = false;

    #endregion

    #region Constants

    private const int OFFSET = 1;

    #endregion

    #region Unity Methods

    public override void OnInspectorGUI()
    {
        EditorGUI.BeginDisabledGroup(true);
        EditorGUILayout.ObjectField("Editor", MonoScript.FromScriptableObject(this), typeof(MonoScript), false);
        EditorGUI.EndDisabledGroup();

        base.OnInspectorGUI();

        var sceneLoader = target as SceneLoader;
        var style = new GUIStyle(EditorStyles.label);
        style.richText = true;

        //�x����\��
        {
            if (_isSetting && _isRemoving)
            {
                _isRemoving = sceneLoader.AllSceneName.Contains("RemoveThis");
                EditorGUILayout.HelpBox("Please Delete Array Element 'RemoveThis'.", MessageType.Warning);
            }
        }

        EditorGUILayout.Space();

        //Scene�̖��O��S�ĂƂ��Ă���
        {
            EditorGUILayout.LabelField("<b>Scene�̖��O��S�ĂƂ��Ă���</b>", style);
            if (GUILayout.Button("GetSceneName"))
            {
                GetAllSceneName();
                _isSetting = true;
                _isRemoving = sceneLoader.AllSceneName.Contains("RemoveThis");
            }
        }
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Asset�t�H���_�̒��ɂ���Scene�̖��O��S�ĂƂ��Ă���֐�
    /// </summary>
    private void GetAllSceneName()
    {
        var sceneLoader = target as SceneLoader;
        List<string> allSceneName = new();

        //BuildSetings�ɓ����Ă���Scene�̖��O��S�ĂƂ��Ă���
        foreach (var scene in EditorBuildSettings.scenes)
        {
            var name = Path.GetFileNameWithoutExtension(scene.path);
            allSceneName.Add(name);
        }

        //�d�����Ă���v�f�������Ă�����ёւ�
        allSceneName = new(allSceneName.Distinct().OrderBy(name =>
        {
            var sceneNum = name.Split("Scene");
            if (sceneNum[OFFSET] == "")sceneNum = new[] { sceneNum[0], "0" };
            return int.Parse(sceneNum[OFFSET]);
        }));
        allSceneName.Add("RemoveThis");

        sceneLoader.ResizeAllSceneName(EditorBuildSettings.scenes.Length + OFFSET);
        sceneLoader.SetAllSceneName(allSceneName);
    }

    #endregion
}
