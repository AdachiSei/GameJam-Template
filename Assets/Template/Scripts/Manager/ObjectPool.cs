using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// オブジェクトプール
/// </summary>
public class ObjectPool : SingletonMonoBehaviour<ObjectPool>
{
    #region Inspector Member

    [SerializeField]
    [Header("生成数の初期値")]
    int _poolCount = 10;

    [SerializeField]
    [Header("プールオブジェクト")]
    List<PoolObjectData> _pools = new List<PoolObjectData>();

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
        foreach (var pool in _pools)
        {
            foreach (var poolObject in pool.CreatedPool)
            {
                DestroyImmediate(poolObject.gameObject);
            }
        }

        foreach (Transform child in transform)
        {
            Debug.Log(child.name);
            DestroyImmediate(child.gameObject);
        }

        _pools = new();
    }

    #endregion
}