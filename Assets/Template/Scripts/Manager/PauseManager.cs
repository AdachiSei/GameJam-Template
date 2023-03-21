using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ポーズに必要なScripts
/// </summary>
public class PauseManager : SingletonMonoBehaviour<PauseManager>
{
    #region Member Variables

    private bool _isPausing = false;

    #endregion

    #region Events

    public event Action OnPause;
    public event Action OnResume;

    #endregion

    #region Unity Method

    void Update()
    {
        if (Input.GetButtonDown(InputName.CANCEL))
        {
            if (!_isPausing)
            {
                _isPausing = true;
                OnPause();
            }
            else
            {
                _isPausing = false;
                OnResume();
            }
        }
    }

    #endregion
}
