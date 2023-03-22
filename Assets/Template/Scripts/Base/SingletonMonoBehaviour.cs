using System;
using UnityEngine;

/// <summary>
/// �V���O���g���p�^�[�����������������Ɍp������W�F�l���b�N�Ȋ��N���X
/// </summary>
public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    #region Properties

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                Type t = typeof(T);

                _instance = (T)FindObjectOfType(t);
                if (_instance == null)
                {
                    Debug.LogWarning($"{t}���A�^�b�`���Ă���I�u�W�F�N�g������܂���");
                }
            }

            return _instance;
        }
    }

    #endregion

    #region Member Variables

    private static T _instance;

    #endregion

    #region Unity Methods

    protected virtual void Awake()
    {
        CheckInstance();
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// ���̃Q�[���I�u�W�F�N�g�ɃA�^�b�`����Ă��邩���ׂ�
    /// �A�^�b�`����Ă���ꍇ�͔j������B
    /// </summary>
    protected bool CheckInstance()
    {
        if (_instance == null)
        {
            _instance = this as T;
            return true;
        }
        else if (_instance == this)
        {
            return true;
        }
        Destroy(this);
        return false;
    }

    #endregion
}
