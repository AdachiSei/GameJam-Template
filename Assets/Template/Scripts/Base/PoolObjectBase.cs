using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// オブジェクトプール用オブジェクトの基底クラス
/// </summary>
public abstract class PoolObjectBase : MonoBehaviour
{
    protected abstract void OnEnable();

    protected virtual void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }
}
