using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Calculator
{
    const float MAX_VALUE = 100f;

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
        Array.Resize(ref probability, num.Length);
        for (int index = 0; index < num.Length; index++)
        {
            for (int count = 0; count < limitCount; count++)
            {
                probability[index] += num[count] * MAX_VALUE / sum;
            }
            Debug.Log(index + "番目 " + probability[index]);
            limitCount++;
        }
        var random = UnityEngine.Random.Range(0f, MAX_VALUE);
        Debug.Log("乱数 " + random);
        for (int i = 0; i < probability.Length; i++)
        {
            if (probability[i] > random)
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
        Array.Resize(ref probability, num.Length);
        for (int index = 0; index < num.Length; index++)
        {
            for (int count = 0; count < limitCount; count++)
            {
                probability[index] += num[count] * MAX_VALUE / sum;
            }
            Debug.Log(index + "番目 " + probability[index]);
            limitCount++;
        }
        var random = UnityEngine.Random.Range(0f, MAX_VALUE);
        Debug.Log("乱数 " + random);
        for (int i = 0; i < probability.Length; i++)
        {
            if (probability[i] > random)
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
    public static int RandomIndex<T>(T[] num) where T : IProbabilityArrayOrList<T,int>,new()
    {
        return RandomIndex(new T().AllValue(num));
    }

    /// <summary>
    /// ガチャのような関数
    /// </summary>
    /// <param name="num">確率</param>
    /// <returns>Index</returns>
    public static int RandomIndex<T>(T[] num,int isInt = 0) where T : IProbabilityArrayOrList<T, float>, new()
    {
        return RandomIndex(new T().AllValue(num));
    }

    /// <summary>
    /// ガチャのような関数
    /// </summary>
    /// <param name="num">確率</param>
    /// <returns>Index</returns>
    public static int RandomIndex<T>(IReadOnlyList<T> num) where T : IProbabilityArrayOrList<T, int>, new()
    {
        return RandomIndex(new T().AllValue(num.ToArray()));
    }

    /// <summary>
    /// ガチャのような関数
    /// </summary>
    /// <param name="num">確率</param>
    /// <returns>Index</returns>
    public static int RandomIndex<T>(IReadOnlyList<T> num, bool isInt = false) where T : IProbabilityArrayOrList<T, float>, new()
    {
        return RandomIndex(new T().AllValue(num.ToArray()));
    }

    /// <summary>
    /// ガチャのような関数
    /// </summary>
    /// <param name="num">確率</param>
    /// <returns>Index</returns>
    public static int RandomIndex<T>(T num) where T : IProbabilityInArrayOrList<T, int>, new()
    {
        return RandomIndex(new T().AllValue(num));
    }

    /// <summary>
    /// ガチャのような関数
    /// </summary>
    /// <param name="num">確率</param>
    /// <returns>Index</returns>
    public static int RandomIndex<T>(T num, bool isInt = false) where T : IProbabilityInArrayOrList<T, float>, new()
    {
        return RandomIndex(new T().AllValue(num));
    }
}
