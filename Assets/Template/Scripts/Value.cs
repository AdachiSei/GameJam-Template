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
    [Header("�������l")]
    T _minValue;

    [SerializeField]
    [Header("�傫���l")]
    T _maxValue;

    public void ChangeValue(T minValue, T maxValue)
    {
        _minValue = minValue;
        _maxValue = maxValue;
    }
}
