using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Calculator
{
    public static TValue RandomT<T, TValue>(TValue a, TValue b) where T : IRandom<TValue>, new()
    {
        return new T().RandomNum(a, b);
    }

    public static TValue B<TValue>(int a) where TValue : Testoo<TValue>,new()
    {
        return new TValue().A(a);
    }
}

public interface Testoo<T>
{
    public T A(int a);
}

public struct RandomInt : IRandom<int>
{
    public int RandomNum(int min, int max) => Random.Range(min, max);
}

public struct RandomFloat : IRandom<float>
{
    public float RandomNum(float min, float max) => Random.Range(min, max);
}