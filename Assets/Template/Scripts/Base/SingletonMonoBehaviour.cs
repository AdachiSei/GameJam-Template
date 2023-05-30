using UnityEngine;

/// <summary>
/// シングルトンパターンを実装したい時に継承するジェネリックな基底クラス
/// </summary>
public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : SingletonMonoBehaviour<T>
{
    public static T I
    {
        get
        {
            if (_instance == null)
                Debug.LogWarning($"{typeof(T)}{LOG}");

            return _instance;
        }
    }

    protected virtual bool IsRemoveComponent => false;

    private static T _instance = null;

    private const string LOG = "をアタッチしているオブジェクトがありません";

    protected virtual void Awake()
    {
        if (_instance == null)
            _instance = this as T;

        else if (IsRemoveComponent)
            Destroy(this);

        else
            Destroy(gameObject);
    }

    protected virtual void OnDestroy()
    {
        if (_instance == this)
            _instance = null;
    }
}
