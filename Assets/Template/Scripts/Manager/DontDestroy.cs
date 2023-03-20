using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    #region Unity Methods

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    #endregion
}