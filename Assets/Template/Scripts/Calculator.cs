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
        float[] probability = { };
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
    public static int RandomIndex(int[] num)
    {
        float[] probability = { };
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

    public static int RandomIndex<T>(T[] num) where T : IRandom<T,int>,new()
    {
        return RandomIndex(new T().AllValue(num));
    }

    public static int RandomIndex<T>(T[] num,int bug = 0) where T : IRandom<T, float>, new()
    {
        return RandomIndex(new T().AllValue(num));
    }

    public static int RandomIndex<T>(List<T> num) where T : IRandom<T, int>, new()
    {
        return RandomIndex(new T().AllValue(num.ToArray()));
    }

    public static int RandomIndex<T>(List<T> num, int bug = 0) where T : IRandom<T, float>, new()
    {
        return RandomIndex(new T().AllValue(num.ToArray()));
    }
}

[Serializable]
public struct RandomInt : IRandom<RandomInt,int>
{
    [SerializeField]
    int _a;

    public int[] AllValue(RandomInt[] num)
    {
        return num.Select(e => e._a).ToArray();
    }
}

[Serializable]
public struct RandomFloat : IRandom<RandomFloat, float>
{
    [SerializeField]
    float _a;

    public float[] AllValue(RandomFloat[] num)
    {
        return num.Select(e => e._a).ToArray();
    }
}