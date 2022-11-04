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

            //BGM�p��Prefab���쐬
            EditorGUILayout.LabelField("<b>BGM�p��Prefab�𐶐�</b>", _style);

            if (GUILayout.Button("CreateBGM"))
            {
                soundM.CreateBGM();
            }

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("<b>SFX�p��Prefab�𐶐�</b>", _style);
            var intField =�@EditorGUILayout.IntField("������", soundM.AudioCount);
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

            EditorGUILayout.LabelField("<b>BGM&SFX�p��Prefab��S�폜</b>", _style);
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
