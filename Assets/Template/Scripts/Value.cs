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
    [Header("è¨Ç≥Ç¢íl")]
    T _minValue;

    [SerializeField]
    [Header("ëÂÇ´Ç¢íl")]
    T _maxValue;

    public void ChangeValue(T minValue, T maxValue)
    {
        _minValue = minValue;
        _maxValue = maxValue;
    }
}
