using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �I�u�W�F�N�g�v�[���p��Script
/// </summary>
public class ObjectPool : MonoBehaviour
{
    #region Pooling Methods

    /// <summary>
    /// �I�u�W�F�N�g�𐶐�����List�i�[����֐�
    /// </summary>
    /// <typeparam name="T">�v�[�����O�������I�u�W�F�N�g�̌^</typeparam>
    /// <param name="pools">�I�u�W�F�N�g���i�[���Ă���List</param>
    /// <param name="prefab">�I�u�W�F�N�g�̃v���t�@�u</param>
    /// <param name="count">��������I�u�W�F�N�g�̐�</param>
    /// <param name="parent">�I�u�W�F�N�g���i�[����e�I�u�W�F�N�g</param>
    public static void CreatePool<T>(IReadOnlyList<T> pools,T prefab, int count,Transform parent = null) where T : class
    {
        var castedList = pools as List<T>;
        for (int i = 0; i < count; i++)
        {
            var newPool = Instantiate(prefab as GameObject);
            newPool.transform.SetParent(parent?.transform);
            var pool = newPool as T;
            castedList.Add(pool);
        }
    }

    /// <summary>
    /// ��A�N�e�B�u�̃I�u�W�F�N�g��T���ăA�N�e�B�u�����Ă����֐�
    /// </summary>
    /// <typeparam name="T">�v�[�����O�������I�u�W�F�N�g�̌^</typeparam>
    /// <param name="pools">�I�u�W�F�N�g���i�[���Ă���List</param>
    /// <param name="prefab">�I�u�W�F�N�g�̃v���t�@�u</param>
    /// <param name="parent">�I�u�W�F�N�g���i�[����e�I�u�W�F�N�g</param>
    /// <returns>��A�N�e�B�u�������I�u�W�F�N�g</returns>
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