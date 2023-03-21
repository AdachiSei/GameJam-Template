using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プールのデータ
/// </summary>
[Serializable]
public class PoolObjectData
{
    #region Properties

    public PoolObjectBase PoolPrefab => _poolPrefab;
    public IReadOnlyList<PoolObjectBase> CreatedPool => _createdPool;
    public Transform PoolParent => _poolParent;
    public int PoolCount => _poolCount;

    #endregion

    #region Inspector Variables

    [SerializeField]
    [Header("プールオブジェクト")]
    private PoolObjectBase _poolPrefab;

    [SerializeField]
    [Header("生成したプール")]
    private List<PoolObjectBase> _createdPool = new List<PoolObjectBase>();

    [SerializeField]
    [Header("生成したプールを格納する親オブジェクト")]
    private Transform _poolParent;

    [SerializeField]
    [Header("生成数")]
    private int _poolCount;

    #endregion

    #region Public Methods

    public void GetPoolPrefab(PoolObjectBase prefab)
    {
        _poolPrefab = prefab;
    }

    public void AddCreatedPool(PoolObjectBase pool)
    {
        _createdPool.Add(pool);
    }

    public void GetPoolParent(Transform transform)
    {
        _poolParent = transform;
    }

    public void ChangePoolCount(int count)
    {
        _poolCount = count;
    }

    #endregion
}