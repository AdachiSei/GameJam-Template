using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// オブジェクトプール
/// </summary>
public class ObjectPool : SingletonMonoBehaviour<ObjectPool>
{
    #region Public Property

    public int PoolCount => _poolCount;

    #endregion

    #region Inspector Member

    [SerializeField]
    [Header("プールオブジェクト")]
    private List<PoolObjectData> _pools = new List<PoolObjectData>();

    #endregion

    #region Private Member

    private int _poolCount = 10;

    #endregion

    #region Unity Methods

    protected override void Awake()
    {
        base.Awake();
        CreatePool();
    }
    
    #endregion

    #region Public Methods

    public void CreatePool()
    {
        foreach (var pool in _pools)
        {
            if(pool.PoolParent == null)
            pool.GetPoolParent(new GameObject().transform);
            
            pool.PoolParent.transform.SetParent(transform);
            pool.PoolParent.name = pool.PoolPrefab.name;
            pool.GetPoolParent(pool.PoolParent.transform);

            for (int i = 0; i < pool.PoolCount; i++)
            {
                var newPool = Instantiate(pool.PoolPrefab);
                newPool.transform.SetParent(pool.PoolParent.transform);
                pool.AddCreatedPool(newPool);
            }
        }
    }

    public PoolObjectBase UseObject(string poolObjectName, Vector3 position = default)
    {
        foreach (var pool in _pools)
        {
            if(pool.PoolPrefab.name == poolObjectName)
            {
                foreach (var createdPool in pool.CreatedPool)
                {
                    if(!createdPool.gameObject.activeSelf)
                    {
                        createdPool.transform.position = position;
                        createdPool.gameObject.SetActive(true);
                        return createdPool;
                    }
                }
                var newPool = Instantiate(pool.PoolPrefab);
                newPool.transform.position = position;
                newPool.transform.SetParent(pool.PoolParent);
                pool.AddCreatedPool(newPool);
                return newPool;
            }
        }
        Debug.LogError("その名前のオブジェクトは存在しません");
        return null;
    }

    public void ChangePoolCount(int count)
    {
        _poolCount = count;
    }

    public void GetPoolObject(PoolObjectBase poolPrefab)
    {
        Init();
        var pool = new PoolObjectData();
        pool.GetPoolPrefab(poolPrefab);
        _pools.Add(pool);
        pool.ChangePoolCount(_poolCount);
    }

    public void Init()
    {
        foreach (Transform child in transform)
        {
            DestroyImmediate(child.gameObject);
        }

        _pools = new();
    }

    #endregion
}