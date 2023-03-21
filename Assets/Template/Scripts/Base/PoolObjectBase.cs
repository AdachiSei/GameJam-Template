using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �I�u�W�F�N�g�v�[���p�I�u�W�F�N�g�̊��N���X
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
