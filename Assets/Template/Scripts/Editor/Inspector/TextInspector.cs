using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[CustomEditor(typeof(Text))]
public class TextInspector : Editor
{
    #region Unity Methods

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var text = target as Text;
        text.horizontalOverflow = HorizontalWrapMode.Overflow;
        text.verticalOverflow = VerticalWrapMode.Overflow;
    }

    #endregion
}
