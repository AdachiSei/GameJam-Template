using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// オブジェクトプール用オブジェクトの基底クラス
/// </summary>
public abstract class PoolObjectBase : MonoBehaviour
{
    #region Unity Methods

    protected abstract void OnEnable();

    protected virtual void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }

    #endregion
}
