using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// TransformÇÃägí£ÉÅÉ\ÉbÉhÇÇ‡Ç¡ÇƒÇ¢ÇÈScript
/// </summary>
public static class TransformExtensions
{
    #region Position Methods

    public static Transform SetPosX(this Transform transform, float x)
    {
        var pos = transform.position;
        pos.x = x;
        transform.position = pos;
        return transform;
    }

    public static Transform SetPosY(this Transform trandform, float y)
    {
        var pos = trandform.position;
        pos.y = y;
        trandform.position = pos;
        return trandform;
    }

    public static Transform SetPosZ(this Transform trandform, float z)
    {
        var pos = trandform.position;
        pos.z = z;
        trandform.position = pos;
        return trandform;
    }

    public static Transform SetPosAll(this Transform transform, float x, float y, float z = 0f)
    {
        var pos = transform.position;
        pos.x = x;
        pos.y = y;
        pos.z = z;
        transform.position = pos;
        return transform;
    }

    public static Transform AddPosX(this Transform transform, float x)
    {
        var pos = transform.position;
        pos.x += x;
        transform.position = pos;
        return transform;
    }

    public static Transform AddPosY(this Transform transform, float y)
    {
        var pos = transform.position;
        pos.y += y;
        transform.position = pos;
        return transform;
    }

    public static Transform AddPosZ(this Transform transform, float z)
    {
        var pos = transform.position;
        pos.z += z;
        transform.position = pos;
        return transform;
    }

    public static Transform AddPosAll(this Transform transform, float x, float y, float z = 0f)
    {
        var pos = transform.position;
        pos.x += x;
        pos.y += y;
        pos.z += z;
        transform.position = pos;
        return transform;
    }

    #endregion

    #region Rotation Methods

    public static Transform SetRotX(this Transform transform, float x)
    {
        var rot = transform.rotation;
        rot.x = x;
        transform.rotation = rot;
        return transform;
    }

    public static Transform SetRotY(this Transform trandform, float y)
    {
        var rot = trandform.rotation;
        rot.y = y;
        trandform.rotation = rot;
        return trandform;
    }

    public static Transform SetRotZ(this Transform transform, float z)
    {
        var rot = transform.rotation;
        rot.z = z;
        transform.rotation = rot;
        return transform;
    }

    public static Transform SetRotAll(this Transform transform, float x, float y, float z)
    {
        var rot = transform.rotation;
        rot.x = x;
        rot.y = y;
        rot.z = z;
        transform.rotation = rot;
        return transform;
    }

    public static Transform AddRotX(this Transform transform, float x)
    {
        var rot = transform.rotation;
        rot.x += x;
        transform.rotation = rot;
        return transform;
    }

    public static Transform AddRotY(this Transform transform, float y)
    {
        var rot = transform.rotation;
        rot.y += y;
        transform.rotation = rot;
        return transform;
    }

    public static Transform AddRotZ(this Transform transform, float z)
    {
        var rot = transform.rotation;
        rot.z += z;
        transform.rotation = rot;
        return transform;
    }

    public static Transform AddRotAll(this Transform transform, float x, float y, float z)
    {
        var rot = transform.rotation;
        rot.x += x;
        rot.y += y;
        rot.z += z;
        transform.rotation = rot;
        return transform;
    }

    #endregion

    #region Scale Method

    public static Transform SetScaleAll(this Transform transform, float all)
    {
        var scale = transform.localScale;
        scale.x = all;
        scale.y = all;
        scale.z = all;
        transform.localScale = scale;
        return transform;
    }

    #endregion

    #region Translate Methods

    public static Transform TranslateX(this Transform transform, float x)
    {
        transform.Translate(x, 0f, 0f);
        return transform;
    }

    public static Transform TranslateY(this Transform transform, float y)
    {
        transform.Translate(0f, y, 0f);
        return transform;
    }

    public static Transform TranslateZ(this Transform transform, float z)
    {
        transform.Translate(0f, 0f, z);
        return transform;
    }

    #endregion
}
