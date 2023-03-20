using System;
using UnityEngine;

/// <summary>
/// �V���O���g���p�^�[�����������������Ɍp������W�F�l���b�N�Ȋ��N���X
/// </summary>
public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance
    {
        get
        {
            if (Instance == null)
            {
                Type t = typeof(T);

                Instance = (T)FindObjectOfType(t);
                if (Instance == null)
                {
                    Debug.LogWarning($"{t}���A�^�b�`���Ă���I�u�W�F�N�g������܂���");
                }
            }

            return Instance;
        }
        private set => Instance = value;
    }


    protected virtual void Awake()
    {
        CheckInstance();
    }

    /// <summary>
    /// ���̃Q�[���I�u�W�F�N�g�ɃA�^�b�`����Ă��邩���ׂ�
    /// �A�^�b�`����Ă���ꍇ�͔j������B
    /// </summary>
    protected bool CheckInstance()
    {
        if (Instance == null)
        {
            Instance = this as T;
            return true;
        }
        else if (Instance == this)
        {
            return true;
        }
        Destroy(this);
        return false;
    }
}
