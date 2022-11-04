using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Transformの拡張メソッドをもっているScript
/// </summary>
public static class TransformExtensions
{
    #region Position Methods

    public static Transform ChangePosX(this Transform tr, float x)
    {
        var pos = tr.position;
        pos.x = x;
        tr.position = pos;
        return tr;
    }

    public static Transform ChangePosY(this Transform tr, float y)
    {
        var pos = tr.position;
        pos.y = y;
        tr.position = pos;
        return tr;
    }

    public static Transform ChangePosZ(this Transform tr, float z)
    {
        var pos = tr.position;
        pos.z = z;
        tr.position = pos;
        return tr;
    }

    public static Transform ChangePosAll(this Transform tr, float x, float y, float z)
    {
        var pos = tr.position;
        pos.x = x;
        pos.y = y;
        pos.z = z;
        tr.position = pos;
        return tr;
    }

    public static Transform AddPosX(this Transform tr, float x)
    {
        var pos = tr.position;
        pos.x += x;
        tr.position = pos;
        return tr;
    }

    public static Transform AddPosY(this Transform tr, float y)
    {
        var pos = tr.position;
        pos.y += y;
        tr.position = pos;
        return tr;
    }

    public static Transform AddPosZ(this Transform tr, float z)
    {
        var pos = tr.position;
        pos.z += z;
        tr.position = pos;
        return tr;
    }

    public static Transform AddPosAll(this Transform tr, float x, float y, float z)
    {
        var pos = tr.position;
        pos.x += x;
        pos.y += y;
        pos.z += z;
        tr.position = pos;
        return tr;
    }

    #endregion

    #region Rotation Methods

    public static Transform ChangeRotX(this Transform tr, float x)
    {
        var rot = tr.rotation;
        rot.x = x;
        tr.rotation = rot;
        return tr;
    }

    public static Transform ChangeRotY(this Transform tr, float y)
    {
        var rot = tr.rotation;
        rot.y = y;
        tr.rotation = rot;
        return tr;
    }

    public static Transform ChangeRotZ(this Transform tr, float z)
    {
        var rot = tr.rotation;
        rot.z = z;
        tr.rotation = rot;
        return tr;
    }

    public static Transform ChangeRotAll(this Transform tr, float x, float y, float z)
    {
        var rot = tr.rotation;
        rot.x = x;
        rot.y = y;
        rot.z = z;
        tr.rotation = rot;
        return tr;
    }

    public static Transform AddRotX(this Transform tr, float x)
    {
        var rot = tr.rotation;
        rot.x += x;
        tr.rotation = rot;
        return tr;
    }

    public static Transform AddRotY(this Transform tr, float y)
    {
        var rot = tr.rotation;
        rot.y += y;
        tr.rotation = rot;
        return tr;
    }

    public static Transform AddRotZ(this Transform tr, float z)
    {
        var rot = tr.rotation;
        rot.z += z;
        tr.rotation = rot;
        return tr;
    }

    public static Transform AddRotAll(this Transform tr, float x, float y, float z)
    {
        var rot = tr.rotation;
        rot.x += x;
        rot.y += y;
        rot.z += z;
        tr.rotation = rot;
        return tr;
    }

    #endregion

    #region Scale Methods

    public static Transform ChangeScaleX(this Transform tr, float x)
    {
        var scale = tr.localScale;
        scale.x = x;
        tr.position = scale;
        return tr;
    }

    public static Transform ChangeScaleY(this Transform tr, float y)
    {
        var scale = tr.localScale;
        scale.y = y;
        tr.position = scale;
        return tr;
    }

    public static Transform ChangeScaleZ(this Transform tr, float z)
    {
        var scale = tr.localScale;
        scale.z = z;
        tr.position = scale;
        return tr;
    }

    public static Transform ChangeScaleAll(this Transform tr, float x, float y, float z)
    {
        var scale = tr.localScale;
        scale.x = x;
        scale.y = y;
        scale.z = z;
        tr.position = scale;
        return tr;
    }

    public static Transform AddScaleX(this Transform tr, float x)
    {
        var scale = tr.localScale;
        scale.x += x;
        tr.position = scale;
        return tr;
    }

    public static Transform AddScaleY(this Transform tr, float y)
    {
        var scale = tr.localScale;
        scale.y += y;
        tr.position = scale;
        return tr;
    }

    public static Transform AddScaleZ(this Transform tr, float z)
    {
        var scale = tr.localScale;
        scale.z += z;
        tr.position = scale;
        return tr;
    }

    public static Transform AddScaleAll(this Transform tr, float x, float y, float z)
    {
        var scale = tr.localScale;
        scale.x += x;
        scale.y += y;
        scale.z += z;
        tr.position = scale;
        return tr;
    }

    #endregion

    #region Translate Methods

    public static Transform TranslateX(this Transform tr, float x)
    {
        tr.Translate(x, 0f, 0f);
        return tr;
    }

    public static Transform TranslateY(this Transform tr, float y)
    {
        tr.Translate(0f, y, 0f);
        return tr;
    }

    public static Transform TranslateZ(this Transform tr, float z)
    {
        tr.Translate(0f, 0f, z);
        return tr;
    }

    #endregion
}
