using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(StageSelectSettings))]
public class StageSelectSettingsEditor : Editor
{
    const int _minValue = 1;
    const int _maxValue = 10;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var settings = target as StageSelectSettings;
        var style = new GUIStyle(EditorStyles.label);
        style.richText = true;

        var floatField = EditorGUILayout.FloatField("スペース", settings.Range);
        settings.ChangeRange(floatField);
        settings.ChangeSpace(floatField);

        EditorGUILayout.LabelField("<b>格納する</b>", style);
        var intField = EditorGUILayout.IntField("いくつずつか", settings.Count);
        var lessThanZero = settings.Count < _minValue;
        var overHundred = settings.Count > _maxValue;
        if (lessThanZero) intField = _minValue;
        else if (overHundred) intField = _maxValue;
        settings.ChangeCount(intField);
        if (GUILayout.Button("Set"))
        {
            settings.Setting(intField);
        }
    }
}
