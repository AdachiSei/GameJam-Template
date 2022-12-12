using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// オブジェクトプール用のScript
/// </summary>
public class ObjectPool : MonoBehaviour
{
    #region Pooling Methods

    /// <summary>
    /// オブジェクトを生成してList格納する関数
    /// </summary>
    /// <typeparam name="T">プーリングしたいオブジェクトの型</typeparam>
    /// <param name="pools">オブジェクトを格納しているList</param>
    /// <param name="prefab">オブジェクトのプレファブ</param>
    /// <param name="count">生成するオブジェクトの数</param>
    /// <param name="parent">オブジェクトを格納する親オブジェクト</param>
    public static void CreatePool<T>(IReadOnlyList<T> pools,T prefab, int count,Transform parent = null) where T : class
    {
        var castedList = pools as List<T>;
        for (int i = 0; i < count; i++)
        {
            var newPool = Instantiate(prefab as GameObject);
            newPool.gameObject.SetActive(false);
            newPool.transform.SetParent(parent?.transform);
            var pool = newPool as T;
            castedList.Add(pool);
        }
    }

    /// <summary>
    /// 非アクティブのオブジェクトを探してアクティブ化してくれる関数
    /// </summary>
    /// <typeparam name="T">プーリングしたいオブジェクトの型</typeparam>
    /// <param name="pools">オブジェクトを格納しているList</param>
    /// <param name="prefab">オブジェクトのプレファブ</param>
    /// <param name="parent">オブジェクトを格納する親オブジェクト</param>
    /// <returns>非アクティブだったオブジェクト</returns>
    public static T UseObject<T>(IReadOnlyList<T> pools,T prefab ,Transform parent = null) where T : class
    {
        var poolObjects = pools as List<GameObject>;
        for (int index = 0; index < poolObjects.Count; index++)
        {
            if(poolObjects[index].activeSelf == false)
            {
                poolObjects[index].SetActive(true);
                return pools[index];
            }
        }
        var newPool = Instantiate(prefab as GameObject);
        newPool.transform.SetParent(parent?.transform);
        var pool = newPool as T;
        var castedList = pools as List<T>;
        castedList.Add(pool);
        return pool;
    }

    #endregion
}