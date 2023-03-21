using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

[CustomEditor(typeof(CanvasScaler))]
public class CanvasScalerInspector : Editor
{
    #region Unity Methods

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var canvasScaler = target as CanvasScaler;
        canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
    }

    #endregion
}
