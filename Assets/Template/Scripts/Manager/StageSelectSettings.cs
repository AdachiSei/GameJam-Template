using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DisturbMagic;

/// <summary>
/// ステージセレクト画面を設定してくれるScript
/// </summary>
public class StageSelectSettings : MonoBehaviour
{
    public int Count => _count;

    [SerializeField]
    [Header("設定したい奴")]
    GameObject[] _stage;

    [SerializeField]
    [Header("Context")]
    VerticalLayoutGroup _context;

    [SerializeField]
    [Header("設定したい奴を格納するゲームオブジェクトのPrefab")]
    RectTransform _stageParent;

    [SerializeField]
    List<RectTransform> _stageParents = new();
    int _count = 3;

    public void Setting(int count)
    {
        foreach (var i in _stageParents)
        {
            i.DetachChildren();
        }
        _stageParents = new();     
        while (true)
        {
            var children = _context.transform;
            var empty = children.childCount == DMInt.ZERO;
            if (empty) break;
            var DestroyGO = children.GetChild(DMInt.ZERO).gameObject;
            DestroyImmediate(DestroyGO);
        }

        var stageCount = _stage.Length - DMInt.ONE;
        var stageParentCount = stageCount / count + 1;
        for (int i = 0; i < stageParentCount; i++)
        {
            var newStageParent = Instantiate(_stageParent);
            newStageParent.transform.SetParent(_context.transform);
            _stageParents.Add(newStageParent);
        }
        var setCount = 0;
        foreach (var parent in _stageParents)
        {
            for (int i = 0; i < count; i++)
            {
                if (setCount > stageCount) return;
                _stage[setCount].transform.SetParent(parent);
                setCount++;
            }
        }
    }

    public void ChangeCount(int count)
    {
        _count = count;
    }


    [ContextMenu("Test")]
    public void Test()
    {
        Debug.Log((_stage.Length - DMInt.ONE)/_count + 1);
    }
}
