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
    /// ポーズ対応の非同期で待ってくれる関数
    /// </summary>
    /// <param name="time">待つ時間</param>
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
