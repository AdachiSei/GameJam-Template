using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 時間を管理するマネージャー
/// </summary>
public class TimeManager : SingletonMonoBehaviour<TimeManager>
{
    #region Public Property

    public float Timer => _timer;

    #endregion

    #region Inspector Member

    [SerializeField]
    [Header("タイマー")]
    private float _timer = 0f;

    #endregion

    #region Private Member

    private bool _isCounting = true;

    #endregion

    #region Unity Method

    //private void Update()
    //{
    //    if (_isCounting)
    //    {
    //        _timer += Time.deltaTime;
    //    }
    //}

    #endregion

    #region Public Methods

    public void StartTimer()
    {
        if (_isCounting) return;
        _isCounting = true;
        CountTimer();
    }

    public void StopTimer()
    {
        _isCounting = false;
    }

    public void Init()
    {
        _timer = 0;
    }

    #endregion

    #region Private Method

    async private void CountTimer()
    {
        while(_isCounting)
        {
            await UniTask.NextFrame();
            _timer += Time.deltaTime;
        }
    }

    #endregion
}
