using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Value<T>
{
    public T MinValue => _minValue;
    public T MaxValue => _maxValue;

    [SerializeField]
    [Header("小さい値")]
    private T _minValue;

    [SerializeField]
    [Header("大きい値")]
    private T _maxValue;

    public void ChangeValue(T minValue, T maxValue)
    {
        _minValue = minValue;
        _maxValue = maxValue;
    }
}
