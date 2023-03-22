using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SoundManager))]
public class SoundManagerInspector : Editor
{
    #region Member Variables

    private float _maxBGMLength = 20f;
    private int _maxValue = 100;
    private static bool _isOpening = false;

    #endregion

    #region Unity Methods

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

            //�S�t�H���_���特���Ƃ��Ă���
            {
                EditorGUILayout.LabelField("<b>�S�t�H���_���特���Ƃ��Ă���</b>", style);
                var floatField = EditorGUILayout.FloatField("BGM�̒���(?�b�ȏ�)", BGMLengthData.BGMLength);
                if (floatField < 0) floatField = 0f;
                else if (floatField > _maxBGMLength) floatField = _maxBGMLength;
                BGMLengthData.SetBGMLength(floatField);
                if (GUILayout.Button("GetAudioClips"))
                {
                    GetAudioClips();
                }
            }

            EditorGUILayout.Space();

            //BGM�p��Prefab���쐬
            {    
                EditorGUILayout.LabelField("<b>BGM�p��Prefab�𐶐�</b>", style);

                if (GUILayout.Button("CreateBGM"))
                {
                    soundManager.CreateBGM();
                }
            }

            EditorGUILayout.Space();

            //SFX�p��Prefab�𐶐�
            {
                EditorGUILayout.LabelField("<b>SFX�p��Prefab�𐶐�</b>", style);
                var intField =
                    EditorGUILayout
                        .IntField
                            ("������", soundManager.AudioSourceCount);

                var lessThanZero = intField < 0;
                var overHundred = intField > _maxValue;
                if (lessThanZero) intField = 0;
                else if (overHundred) intField = _maxValue;

                soundManager.SetAudioCount(intField);

                if (GUILayout.Button("CreateSFX"))
                {
                    soundManager.CreateSFX();
                }
            }

            EditorGUILayout.Space();

            //BGM&SFX�p��Prefab��S�폜
            {
                EditorGUILayout.LabelField("<b>BGM&SFX�p��Prefab��S�폜</b>", style);
                EditorGUI.BeginDisabledGroup(soundManager.IsStopingToCreate);

                if (GUILayout.Button("Init"))
                {
                    soundManager.Init();
                }

                EditorGUI.EndDisabledGroup();
            }
        }
    }

    #endregion

    #region Private Methods

    private void GetAudioClips()
    {
        var soundManager = target as SoundManager;
        List<AudioClip> bgmClips = new ();
        List<AudioClip> sfxClips = new ();

        foreach (var guid in AssetDatabase.FindAssets("t:AudioClip"))
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            var asset = AssetDatabase.LoadMainAssetAtPath(path);
            var audioClip = asset as AudioClip;
            var isLong = audioClip.length >= BGMLengthData.BGMLength;
            if (isLong) bgmClips.Add(audioClip);
            else sfxClips.Add(audioClip);
        }
        soundManager.SetAudioClips(bgmClips);
        soundManager.SetAudioClips(sfxClips);
    }

    #endregion
}
