using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ArrayExtensions
{
    #region Constants

    private const int OFFSET = -1;

    #endregion

    #region Public Method

    public static int OffsetLength<T>(this T[] array)
    {
        return array.Length + OFFSET;
    }

    #endregion
}
