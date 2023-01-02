using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �I�u�W�F�N�g�v�[���p�I�u�W�F�N�g�̊��N���X
/// </summary>
public abstract class PoolObjectBase : MonoBehaviour
{
    protected abstract void OnEnable();

    protected virtual void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }
}
