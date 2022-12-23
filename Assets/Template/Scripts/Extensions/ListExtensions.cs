using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ListExtensions
{
    #region Constants

    private const int OFFSET = -1;

    #endregion

    #region Public Method

    public static int OffsetCount<T>(this List<T> list)
    {
        return list.Count + OFFSET;
    }

    #endregion
}
