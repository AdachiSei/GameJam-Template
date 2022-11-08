using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(StageSelectSettings))]
public class StageSelectSettingsEditor : Editor
{
    #region Private Static Member

    private static bool _isOpening;

    #endregion

    #region Private Member

    private int _minValue = 1;
    private int _maxValue = 10;

    #endregion

    #region Override Method

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var settings = target as StageSelectSettings;
        var style = new GUIStyle(EditorStyles.label);
        style.richText = true;

        EditorGUILayout.Space();

        _isOpening = EditorGUILayout.Foldout(_isOpening, "Settings");
        if (_isOpening)
        {
            EditorGUILayout.Space();

            EditorGUILayout.LabelField("<b>Ç«ÇÍÇ≠ÇÁÇ¢ó£Ç∑Ç©</b>", style);
            var floatField = EditorGUILayout.FloatField("ÉXÉyÅ[ÉX", settings.Range);
            settings.ChangeRange(floatField);
            bool isChangingValue = settings.StageParents[0].spacing != settings.Range;
            if (isChangingValue) settings.ChangeSpace(floatField);

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("<b>äiî[Ç∑ÇÈ</b>", style);
            var intField = EditorGUILayout.IntField("Ç¢Ç≠Ç¬Ç∏Ç¬Ç©", settings.Count);
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

    #endregion
}
