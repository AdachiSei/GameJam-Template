using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// シングルトンを継承したスクリプト
/// </summary>
public class ObjectPool : SingletonMonoBehaviour<ObjectPool>
{
    #region Inspector Member

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

    #endregion

    #region PrivateMethods

    private void CreatePool()
    {
        foreach (var pool in _pools)
        {
            var parent = new GameObject();
            parent.transform.SetParent(transform);
            parent.name = pool.PoolPrefab.name;
            pool.GetPoolParent(parent.transform);

            for (int i = 0; i < pool.PoolCount; i++)
            {
                var newPool = Instantiate(pool.PoolPrefab);
                newPool.transform.SetParent(parent.transform);
                pool.AddCreatedPool(newPool);
            }
        }
    }

    #endregion
}