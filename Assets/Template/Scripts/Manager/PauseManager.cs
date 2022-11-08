using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ポーズに必要なScripts
/// </summary>
public class PauseManager : SingletonMonoBehaviour<PauseManager>
{
    #region private Member

    private bool _isPause;

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
            if (!_isPause)
            {
                _isPause = true;
                OnPause();
            }
            else
            {
                _isPause = false;
                OnResume();
            }
        }
    }

    #endregion
}
