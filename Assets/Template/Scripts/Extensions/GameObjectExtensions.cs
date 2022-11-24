using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameObjectExtensions
{
    public static void ChangeActive(this GameObject gameObject)
    {
        var activeSelf = gameObject.activeSelf;
        if (activeSelf == true) gameObject.SetActive(false);
        else gameObject.SetActive(true);
    }
}
