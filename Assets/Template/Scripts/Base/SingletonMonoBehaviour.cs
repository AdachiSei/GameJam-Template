using System;
using UnityEngine;

/// <summary>
/// シングルトンパターンを実装したい時に継承するジェネリックな基底クラス
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
                    Debug.LogWarning($"{t}をアタッチしているオブジェクトがありません");
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
    /// 他のゲームオブジェクトにアタッチされているか調べる
    /// アタッチされている場合は破棄する。
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
