using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SoundManager))]
public class SoundManagerEditor : Editor
{
    #region Private Member

    private int _maxValue = 1000;

    #endregion

    #region Private Static Member

    private static bool _isOpening;

    #endregion

    #region Override Method

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var soundM = target as SoundManager;
        var style = new GUIStyle(EditorStyles.label);
        style.richText = true;

        EditorGUILayout.Space();

        _isOpening = EditorGUILayout.Foldout(_isOpening, "Settings");
        if (_isOpening)
        {
            EditorGUILayout.Space();

            //BGM用のPrefabを作成
            EditorGUILayout.LabelField("<b>BGM用のPrefabを生成</b>", style);

            if (GUILayout.Button("CreateBGM"))
            {
                soundM.CreateBGM();
            }

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("<b>SFX用のPrefabを生成</b>", style);
            var intField =　EditorGUILayout.IntField("生成数", soundM.AudioCount);
            var lessThanZero = soundM.AudioCount < 0;
            var overHundred = soundM.AudioCount > _maxValue;
            if (lessThanZero) intField = 0;
            else if (overHundred)intField = _maxValue;

            soundM.ChangeAudioCount(intField);

            if (GUILayout.Button("CreateSFX"))
            {
                soundM.CreateSFX();
            }

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("<b>BGM&SFX用のPrefabを全削除</b>", style);
            EditorGUI.BeginDisabledGroup(soundM.IsStopCreate);

            if (GUILayout.Button("Init"))
            {
                soundM.Init();
            }

            EditorGUI.EndDisabledGroup();
        }
    }

    #endregion
}
