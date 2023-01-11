using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SoundManager))]
public class SoundManagerEditor : Editor
{
    #region Private Member

    private float _maxLength = 20f;
    private int _maxValue = 100;

    #endregion

    #region Private Static Member

    private static bool _isOpening;

    #endregion

    #region Override Method

    public override void OnInspectorGUI()
    {
        EditorGUI.BeginDisabledGroup(true);
        EditorGUILayout.ObjectField("Editor", MonoScript.FromScriptableObject(this), typeof(MonoScript), false);
        EditorGUI.EndDisabledGroup();

        base.OnInspectorGUI();

        var soundManager = target as SoundManager;
        var style = new GUIStyle(EditorStyles.label);
        style.richText = true;

        EditorGUILayout.Space();

        _isOpening = EditorGUILayout.Foldout(_isOpening, "Settings");
        if (_isOpening)
        {
            EditorGUILayout.Space();

            //全フォルダから音をとってくる
            EditorGUILayout.LabelField("<b>全フォルダから音をとってくる</b>", style);
            var floatField = EditorGUILayout.FloatField("BGMの長さ(?秒以上)", soundManager.BGMLength);
            if (floatField < 0) floatField = 0f;
            else if (floatField > _maxLength) floatField = _maxLength;
            soundManager.ChangeAudioLength(floatField);
            if (GUILayout.Button("GetAudioClips"))
            {
                GetAudioClips(floatField);
            }

            EditorGUILayout.Space();

            //BGM用のPrefabを作成
            EditorGUILayout.LabelField("<b>BGM用のPrefabを生成</b>", style);

            if (GUILayout.Button("CreateBGM"))
            {
                soundManager.CreateBGM();
            }

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("<b>SFX用のPrefabを生成</b>", style);
            var intField =
                EditorGUILayout
                    .IntField
                        ("生成数", soundManager.AudioSourceNumber);

            var lessThanZero = intField < 0;
            var overHundred = intField > _maxValue;
            if (lessThanZero) intField = 0;
            else if (overHundred) intField = _maxValue;

            soundManager.ChangeAudioCount(intField);

            if (GUILayout.Button("CreateSFX"))
            {
                soundManager.CreateSFX();
            }

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("<b>BGM&SFX用のPrefabを全削除</b>", style);
            EditorGUI.BeginDisabledGroup(soundManager.IsStopToCreate);

            if (GUILayout.Button("Init"))
            {
                soundManager.Init();
            }

            EditorGUI.EndDisabledGroup();
        }
    }

    #endregion

    #region Private Methods

    private void GetAudioClips(float audioLength)
    {
        var soundManager = target as SoundManager;
        var bgmList = new List<AudioClip>();
        var sfxList = new List<AudioClip>();

        foreach (var guid in AssetDatabase.FindAssets("t:AudioClip"))
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            var pathName = AssetDatabase.LoadMainAssetAtPath(path);
            var audioClip = pathName as AudioClip;
            var isLong = audioClip.length >= audioLength;
            if (isLong) bgmList.Add(audioClip);
            else sfxList.Add(audioClip);
        }
        soundManager.ResizeBGMClips(bgmList.Count);
        soundManager.ResizeSFXClips(sfxList.Count);
        for (int i = 0; i < bgmList.Count; i++)
        {
            soundManager.AddBGMClip(i, bgmList[i]);
        }
        for (int i = 0; i < sfxList.Count; i++)
        {
            soundManager.AddSFXClip(i, sfxList[i]);
        }
    }

    #endregion
}
