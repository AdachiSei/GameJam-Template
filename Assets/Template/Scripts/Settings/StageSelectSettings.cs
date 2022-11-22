using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ステージセレクト画面を設定してくれるScript
/// </summary>
public class StageSelectSettings : MonoBehaviour
{
    #region Properties

    public int Count => _count;
    public float Range => _range;
    public IReadOnlyList<HorizontalLayoutGroup> StageParents => _stageParents;  

    #endregion

    #region Inspector Member

    [SerializeField]
    [Header("設定したいもの")]
    private GameObject[] _stage;

    [SerializeField]
    [Header("Context")]
    private VerticalLayoutGroup _context;

    [SerializeField]
    [Header("設定したいものを格納するゲームオブジェクトのPrefab")]
    private HorizontalLayoutGroup _stageParent;

    [SerializeField]
    [Header("設定したいものを格納するゲームオブジェクトのPrefabs")]
    private List<HorizontalLayoutGroup> _stageParents = new();

    #endregion

    #region Private Member

    private int _count = 3;
    private float _range;

    #endregion

    #region Const Member

    private const int OFFSET = 1;

    #endregion

    #region Public Methods

    public void ChangeCount(int count)
    {
        _count = count;
    }

    public void ChangeRange(float range)
    {
        _range = range;
    }
    public void ChangeSpace(float range)
    {
        foreach (var i in _stageParents)
        {
            i.spacing = range;
        }
    }

    #endregion

    #region Inspector Method

    public void Setting(int count)
    {
        foreach (var i in _stageParents)
        {
            i.transform.DetachChildren();
        }
        _stageParents = new();     
        while (true)
        {
            var children = _context.transform;
            var empty = children.childCount == 0;
            if (empty) break;
            var DestroyGO = children.GetChild(0).gameObject;
            DestroyImmediate(DestroyGO);
        }

        var stageCount = _stage.Length - OFFSET;
        var stageParentCount = stageCount / count + OFFSET;
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
                _stage[setCount].transform.SetParent(parent.transform);
                setCount++;
            }
        }
    }

    #endregion
}
