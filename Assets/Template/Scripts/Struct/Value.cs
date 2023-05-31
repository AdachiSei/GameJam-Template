using System;
using UnityEngine;

/// <summary>
/// https://techblog.kayac.com/trap-around-struct-in-csharp
/// https://albatrus.com/entry/2021/07/03/190000
/// https://baba-s.hatenablog.com/entry/2022/05/19/090000
/// </summary>
[Serializable]
public struct Value<T> : IEquatable<Value<T>> where T : struct 
{
    public T Min;
    public T Max;

    public Value(T minValue, T maxValue)
    {
        Min = minValue;
        Max = maxValue;
    }

    public bool Equals(Value<T> other)
    {
        return 
            Min.Equals(other.Min) &&
            Max.Equals(other.Max);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Min, Max);
    }
}
