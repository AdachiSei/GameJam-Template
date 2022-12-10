using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �|�[�Y�ɕK�v��Scripts
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

    #region Public Method

    /// <summary>
    /// �|�[�Y�Ή��̔񓯊��ő҂��Ă����֐�
    /// </summary>
    /// <param name="time">�҂���</param>
    async public UniTask UniTaskForPause(float time)
    {
        for (float i = 0f; i < time; i += Time.deltaTime)
        {
            if (_isPause) await UniTask.WaitUntil(() => !_isPause);
            await UniTask.NextFrame();
        }
    }

    #endregion
}
