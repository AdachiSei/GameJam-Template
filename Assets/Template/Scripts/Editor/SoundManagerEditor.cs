using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SoundManager))]
public class SoundManagerEditor : Editor
{
    int _num;
    string _numStr;
    bool _first = true;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var soundManager = target as SoundManager;
        GUILayout.Box("BGM�p��AudioSorce���쐬");
        if (GUILayout.Button("CreateBGM"))
        {
            soundManager.CreateBGM();
        }
        GUILayout.Box("SFX�p��AudioSorce���쐬");

        try
        {
            if (_first)
            {
                _numStr = "0";
                _first = false;
            }
            _numStr = GUILayout.TextField(_numStr, 3);
            _num = int.Parse(_numStr);
        }
        catch{}

        if (GUILayout.Button("CreateSFX"))
        {
            soundManager.CreateSFX(_num);
        }
        GUILayout.Box("AudioSorce��S�폜");
        EditorGUI.BeginDisabledGroup(soundManager.IsStopCreate);
        if (GUILayout.Button("Init"))
        {
            soundManager.Init();
        }
        EditorGUI.EndDisabledGroup();
    }
}
