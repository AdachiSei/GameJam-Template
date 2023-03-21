using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ObjectPool))]
public class ObjectPoolInspector : Editor
{
    #region Member Variables

    private static bool _isOpening;

    #endregion

    #region Unity Methods

    public override void OnInspectorGUI()
    {
        EditorGUI.BeginDisabledGroup(true);
        EditorGUILayout.ObjectField("Editor", MonoScript.FromScriptableObject(this), typeof(MonoScript), false);
        EditorGUI.EndDisabledGroup();

        base.OnInspectorGUI();

        var objectPool = target as ObjectPool;
        var style = new GUIStyle(EditorStyles.label);
        style.richText = true;

        EditorGUILayout.Space();

        _isOpening = EditorGUILayout.Foldout(_isOpening, "Settings");
        if (_isOpening)
        {
            EditorGUILayout.Space();

            EditorGUILayout.LabelField("<b>�v�[���I�u�W�F�N�g�𐶐�����</b>", style);
            if (GUILayout.Button("CreatePool"))
            {
                objectPool.CreatePool();
            }

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("<b>�v�[���I�u�W�F�N�g�̃v���t�@�u��S�ĂƂ��Ă���</b>", style);
            var intField =
                EditorGUILayout
                    .IntField
                        ("������(�����l)", objectPool.PoolCount);

            var lessThanZero = intField < 0;
            var overHundred = intField > 100;
            if (lessThanZero) intField = 0;
            else if (overHundred) intField = 100;

            objectPool.SetPoolCount(intField);

            if (GUILayout.Button("GetPoolObject"))
            {
                foreach (var guid in AssetDatabase.FindAssets("t:Prefab"))
                {
                    var path = AssetDatabase.GUIDToAssetPath(guid);
                    var pathObject = AssetDatabase.LoadMainAssetAtPath(path);
                    var poolPrefab = pathObject as GameObject;

                    if (poolPrefab.TryGetComponent(out PoolObjectBase poolObject))
                    {
                        objectPool.GetPoolObject(poolObject);
                    }
                }
            }

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("<b>�S�폜</b>", style);
            if (GUILayout.Button("Init"))
            {
                objectPool.Init();
            }
        }
    }

    #endregion
}
