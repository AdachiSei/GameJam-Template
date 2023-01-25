using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Type 型の拡張メソッドを管理するクラス
/// </summary>
public static class TypeExtensions
{
    /// <summary>
    /// 指定された Type の継承元であるすべての型を取得します
    /// </summary>
    public static IEnumerable<Type> GetBaseTypes(this Type self)
    {
        for (var baseType = self.BaseType; null != baseType; baseType = baseType.BaseType)
        {
            yield return baseType;
        }
    }

    /// <summary>
    /// 指定された Type の継承元にベースクラスの有無でboolで返します
    /// </summary>
    public static bool IsGetBaseTypes(this Type self,Type type,bool isStop = false) 
    {
        for (var baseType = self.BaseType; null != baseType; baseType = baseType.BaseType)
        {
            Debug.Log(baseType);
            if (baseType == type)return true;
            if (baseType == typeof(MonoBehaviour) && !isStop)return false;
        }
        return false;
    }
}