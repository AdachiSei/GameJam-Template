using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Type �^�̊g�����\�b�h���Ǘ�����N���X
/// </summary>
public static class TypeExtensions
{
    /// <summary>
    /// �w�肳�ꂽ Type �̌p�����ł��邷�ׂĂ̌^���擾���܂�
    /// </summary>
    public static IEnumerable<Type> GetBaseTypes(this Type self)
    {
        for (var baseType = self.BaseType; null != baseType; baseType = baseType.BaseType)
        {
            yield return baseType;
        }
    }

    /// <summary>
    /// �w�肳�ꂽ Type �̌p�����Ƀx�[�X�N���X�̗L����bool�ŕԂ��܂�
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