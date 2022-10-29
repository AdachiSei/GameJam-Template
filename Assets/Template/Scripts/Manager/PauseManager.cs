using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : SingletonMonoBehaviour<PauseManager>
{
    #region private Member

    private bool _isPause;

    #endregion

    #region Events

    public event Action OnPause;
    public event Action OnUnPause;

    #endregion

    #region Unity Method

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (!_isPause)
            {
                _isPause = true;
                OnPause();
            }
            else
            {
                _isPause = false;
                OnUnPause();
            }
        }
    }

    #endregion
}
