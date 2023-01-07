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
    List<PoolObjectBase> _poolObjects = new List<PoolObjectBase>();

    #endregion

    #region Unity Methods

    protected override void Awake()
    {
        base.Awake();
        CreatePool();
    }

    #endregion

    #region Public Methods

    public void UseObject()
    {

    }

    #endregion

    #region PrivateMethods

    private void CreatePool()
    {

    }

    #endregion
}