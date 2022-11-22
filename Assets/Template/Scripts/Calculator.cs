using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 計算機
/// </summary>
public static class Calculator
{
    #region Const Members

    const int MAX_VALUE = 100;

    const float MAX_VALUE_F = 100f;

    #endregion

    #region RandomIndex Methods

    /// <summary>
    /// ガチャのような関数
    /// </summary>
    /// <param name="num">確率</param>
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
            Debug.Log(index + "番目 " + probability[index]);
            limitCount++;
        }
        var randomValue = Random.Range(0f, MAX_VALUE_F);
        Debug.Log("乱数 " + randomValue);
        for (int i = 0; i < probability.Length; i++)
        {
            if (probability[i] > randomValue)
            {
                Debug.Log("結果は" + i);
                return i;
            }
        }
        return 0;
    }

    /// <summary>
    /// ガチャのような関数
    /// </summary>
    /// <param name="num">確率</param>
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
            Debug.Log(index + "番目 " + probability[index]);
            limitCount++;
        }
        var randomValue = Random.Range(0f, MAX_VALUE_F);
        Debug.Log("乱数 " + randomValue);
        for (int i = 0; i < probability.Length; i++)
        {
            if (probability[i] > randomValue)
            {
                Debug.Log("結果は" + i);
                return i;
            }
        }
        return 0;
    }

    /// <summary>
    /// ガチャのような関数
    /// </summary>
    /// <param name="num">確率</param>
    /// <returns>Index</returns>
    public static int RandomIndex(IReadOnlyList<float> num)
    {
        var newNum = num.ToArray();
        return RandomIndex(newNum);
    }

    /// <summary>
    /// ガチャのような関数
    /// </summary>
    /// <param name="num">確率</param>
    /// <returns>Index</returns>
    public static int RandomIndex(IReadOnlyList<int> num)
    {
        var newNum = num.ToArray();
        return RandomIndex(newNum);
    }

    /// <summary>
    /// ガチャのような関数
    /// </summary>
    /// <param name="num">確率</param>
    /// <returns>Index</returns>
    public static int RandomIndex<T>(T[] num) where T : IProbabilityT<T,int>,new()
    {
        return RandomIndex(new T().AllValue(num));
    }

    /// <summary>
    /// ガチャのような関数
    /// </summary>
    /// <param name="num">確率</param>
    /// <returns>Index</returns>
    public static int RandomIndex<T>(T[] num,int isInt = 0) where T : IProbabilityT<T, float>, new()
    {
        return RandomIndex(new T().AllValue(num));
    }

    /// <summary>
    /// ガチャのような関数
    /// </summary>
    /// <param name="num">確率</param>
    /// <returns>Index</returns>
    public static int RandomIndex<T>(IReadOnlyList<T> num) where T : IProbabilityT<T, int>, new()
    {
        return RandomIndex(new T().AllValue(num.ToArray()));
    }

    /// <summary>
    /// ガチャのような関数
    /// </summary>
    /// <param name="num">確率</param>
    /// <returns>Index</returns>
    public static int RandomIndex<T>(IReadOnlyList<T> num, bool isInt = false) where T : IProbabilityT<T, float>, new()
    {
        return RandomIndex(new T().AllValue(num.ToArray()));
    }

    /// <summary>
    /// ガチャのような関数
    /// </summary>
    /// <param name="num">確率</param>
    /// <returns>Index</returns>
    public static int RandomIndex<T>(T num) where T : IProbabilityInT<T, int>, new()
    {
        return RandomIndex(new T().AllValue(num));
    }

    /// <summary>
    /// ガチャのような関数
    /// </summary>
    /// <param name="num">確率</param>
    /// <returns>Index</returns>
    public static int RandomIndex<T>(T num, bool isInt = false) where T : IProbabilityInT<T, float>, new()
    {
        return RandomIndex(new T().AllValue(num));
    }

    #endregion

    #region RandomBool Methods

    /// <summary>
    /// 確率でboolを返す関数
    /// </summary>
    /// <param name="probability">確率</param>
    /// <returns>TrueかFalse</returns>
    public static bool RandomBool(int probability)
    {
        probability = Mathf.Clamp(probability, 0, MAX_VALUE);
        var randomValue = Random.Range(0,MAX_VALUE);
        if (probability > randomValue) return true;
        return false;
    }

    /// <summary>
    /// 確率でboolを返す関数
    /// </summary>
    /// <param name="probability">確率</param>
    /// <returns>TrueかFalse</returns>
    public static bool? RandomBool(float probability)
    {
        probability = Mathf.Clamp(probability, 0, MAX_VALUE_F);
        var randomValue = Random.Range(0f, MAX_VALUE_F);
        if (probability > randomValue) return true;
        return false;
    }

    #endregion
}
