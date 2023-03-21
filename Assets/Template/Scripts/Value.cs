using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Value<T>
{
    #region Properties

    public T MinValue => _minValue;
    public T MaxValue => _maxValue;

    #endregion

    #region Inspector Variables

    [SerializeField]
    [Header("小さい値")]
    private T _minValue;

    [SerializeField]
    [Header("大きい値")]
    private T _maxValue;

    #endregion

    #region Public Methods

    public void SetValue(T minValue, T maxValue)
    {
        _minValue = minValue;
        _maxValue = maxValue;
    }

    #endregion
}
