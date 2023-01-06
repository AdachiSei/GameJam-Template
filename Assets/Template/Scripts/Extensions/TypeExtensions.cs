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
    public static bool IsGetBaseTypes(this Type self,Type type) 
    {
        for (var baseType = self.BaseType; null != baseType; baseType = baseType.BaseType)
        {
            if(baseType == type)
            return true;
            if (baseType == typeof(MonoBehaviour))
            return false;
        }
        return false;
    }

    /// <summary>
    /// 指定された Type の継承元にベースクラスの有無でboolで返します
    /// </summary>
    public static bool IsGetDeclaringTypes(this Type self, Type type)
    {
        for (var declaringType = self.DeclaringType; null != declaringType; declaringType = declaringType.DeclaringType)
        {
            if (declaringType == type)
                return true;
        }
        return false;
    }
}