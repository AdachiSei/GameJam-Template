using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using DisturbMagic;

[CustomEditor(typeof(SoundManager))]
public class SoundManagerEditor : Editor
{
    #region Private Static Member

    private static bool _isOpening;

    #endregion

    #region Override Method

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var soundM = target as SoundManager;
        var _style = new GUIStyle(EditorStyles.label);
        _style.richText = true;

        EditorGUILayout.Space();

        _isOpening = EditorGUILayout.Foldout(_isOpening, "Settings");
        if (_isOpening)
        {
            EditorGUILayout.Space();

            //BGM用のPrefabを作成
            EditorGUILayout.LabelField("<b>BGM用のPrefabを生成</b>", _style);

            if (GUILayout.Button("CreateBGM"))
            {
                soundM.CreateBGM();
            }

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("<b>SFX用のPrefabを生成</b>", _style);
            var intField =　EditorGUILayout.IntField("生成数", soundM.AudioCount);
            var lessThanZero = soundM.AudioCount < DMInt.ZERO;
            var overHundred = soundM.AudioCount > DMInt.THOUSAND;
            if (lessThanZero) intField = DMInt.ZERO;
            else if (overHundred)intField = DMInt.THOUSAND;

            soundM.ChangeAudioCount(intField);

            if (GUILayout.Button("CreateSFX"))
            {
                soundM.CreateSFX();
            }

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("<b>BGM&SFX用のPrefabを全削除</b>", _style);
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
