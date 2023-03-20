using System;

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

    public static void ForEach<T>(this T[] array, Action<T> action)
    {
        foreach (var item in array) action(item);
    }

    #endregion
}
