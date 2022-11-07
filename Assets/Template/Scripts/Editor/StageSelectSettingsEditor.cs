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

        EditorGUILayout.LabelField("<b>Ši”[‚·‚é</b>", style);
        var intField = EditorGUILayout.IntField("‚¢‚­‚Â‚¸‚Â‚©", settings.Count);
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
