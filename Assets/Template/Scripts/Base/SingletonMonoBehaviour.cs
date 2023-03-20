using System;
using UnityEngine;

/// <summary>
/// シングルトンパターンを実装したい時に継承するジェネリックな基底クラス
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
                    Debug.LogWarning($"{t}をアタッチしているオブジェクトがありません");
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
    /// 他のゲームオブジェクトにアタッチされているか調べる
    /// アタッチされている場合は破棄する。
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
