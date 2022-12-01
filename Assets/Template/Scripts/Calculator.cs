using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �v�Z�@
/// </summary>
public static class Calculator
{
    #region Const Members

    const int MAX_VALUE = 100;

    const float MAX_VALUE_F = 100f;

    #endregion

    #region RandomIndex Methods

    /// <summary>
    /// �K�`���̂悤�Ȋ֐�
    /// </summary>
    /// <param name="num">�m��</param>
    /// <returns>Index</returns>
    public static int RandomIndex(float[] num)
    {
        float[] probability = null;
        var sum = num.Sum();
        var limitCount = 1;
        System.Array.Resize(ref probability, num.Length);
        for (int index = 0; index < num.Length; index++)
        {
            for (int count = 0; count < limitCount; count++)
            {
                probability[index] += num[count] * MAX_VALUE_F / sum;
            }
            Debug.Log(index + "�Ԗ� " + probability[index]);
            limitCount++;
        }
        var randomValue = Random.Range(0f, MAX_VALUE_F);
        Debug.Log("���� " + randomValue);
        for (int i = 0; i < probability.Length; i++)
        {
            if (probability[i] > randomValue)
            {
                Debug.Log("���ʂ�" + i);
                return i;
            }
        }
        return 0;
    }

    /// <summary>
    /// �K�`���̂悤�Ȋ֐�
    /// </summary>
    /// <param name="num">�m��</param>
    /// <returns>Index</returns>
    public static int RandomIndex(int[] num)
    {
        float[] probability = null;
        float sum = num.Sum();
        var limitCount = 1;
        System.Array.Resize(ref probability, num.Length);
        for (int index = 0; index < num.Length; index++)
        {
            for (int count = 0; count < limitCount; count++)
            {
                probability[index] += num[count] * MAX_VALUE_F / sum;
            }
            Debug.Log(index + "�Ԗ� " + probability[index]);
            limitCount++;
        }
        var randomValue = Random.Range(0f, MAX_VALUE_F);
        Debug.Log("���� " + randomValue);
        for (int i = 0; i < probability.Length; i++)
        {
            if (probability[i] > randomValue)
            {
                Debug.Log("���ʂ�" + i);
                return i;
            }
        }
        return 0;
    }

    /// <summary>
    /// �K�`���̂悤�Ȋ֐�
    /// </summary>
    /// <param name="num">�m��</param>
    /// <returns>Index</returns>
    public static int RandomIndex(IReadOnlyList<float> num)
    {
        var newNum = num.ToArray();
        return RandomIndex(newNum);
    }

    /// <summary>
    /// �K�`���̂悤�Ȋ֐�
    /// </summary>
    /// <param name="num">�m��</param>
    /// <returns>Index</returns>
    public static int RandomIndex(IReadOnlyList<int> num)
    {
        var newNum = num.ToArray();
        return RandomIndex(newNum);
    }

    /// <summary>
    /// �K�`���̂悤�Ȋ֐�
    /// </summary>
    /// <param name="num">�m��</param>
    /// <returns>Index</returns>
    public static int RandomIndex<T>(T[] num) where T : IProbability<T,int>,new()
    {
        return RandomIndex(new T().AllValue(num));
    }

    /// <summary>
    /// �K�`���̂悤�Ȋ֐�
    /// </summary>
    /// <param name="num">�m��</param>
    /// <returns>Index</returns>
    public static int RandomIndex<T>(T[] num,int isInt = 0) where T : IProbability<T, float>, new()
    {
        return RandomIndex(new T().AllValue(num));
    }

    /// <summary>
    /// �K�`���̂悤�Ȋ֐�
    /// </summary>
    /// <param name="num">�m��</param>
    /// <returns>Index</returns>
    public static int RandomIndex<T>(IReadOnlyList<T> num) where T : IProbability<T, int>, new()
    {
        return RandomIndex(new T().AllValue(num.ToArray()));
    }

    /// <summary>
    /// �K�`���̂悤�Ȋ֐�
    /// </summary>
    /// <param name="num">�m��</param>
    /// <returns>Index</returns>
    public static int RandomIndex<T>(IReadOnlyList<T> num, bool isInt = false) where T : IProbability<T, float>, new()
    {
        return RandomIndex(new T().AllValue(num.ToArray()));
    }

    /// <summary>
    /// �K�`���̂悤�Ȋ֐�
    /// </summary>
    /// <param name="num">�m��</param>
    /// <returns>Index</returns>
    public static int RandomIndex<T>(T num) where T : IProbabilityInArray<T, int>, new()
    {
        return RandomIndex(new T().AllValue(num));
    }

    /// <summary>
    /// �K�`���̂悤�Ȋ֐�
    /// </summary>
    /// <param name="num">�m��</param>
    /// <returns>Index</returns>
    public static int RandomIndex<T>(T num, bool isInt = false) where T : IProbabilityInArray<T, float>, new()
    {
        return RandomIndex(new T().AllValue(num));
    }

    #endregion

    #region RandomBool Methods

    /// <summary>
    /// �m����bool��Ԃ��֐�
    /// </summary>
    /// <param name="probability">�m��</param>
    /// <returns>True��False</returns>
    public static bool RandomBool(int probability)
    {
        probability = Mathf.Clamp(probability, 0, MAX_VALUE);
        var randomValue = Random.Range(0,MAX_VALUE);
        if (probability > randomValue) return true;
        return false;
    }

    /// <summary>
    /// �m����bool��Ԃ��֐�
    /// </summary>
    /// <param name="probability">�m��</param>
    /// <returns>True��False</returns>
    public static bool? RandomBool(float probability)
    {
        probability = Mathf.Clamp(probability, 0, MAX_VALUE_F);
        var randomValue = Random.Range(0f, MAX_VALUE_F);
        if (probability > randomValue) return true;
        return false;
    }

    #endregion
}
