using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 時間を管理するマネージャー
/// </summary>
public class TimeManager : SingletonMonoBehaviour<TimeManager>
{
    #region Properties

    public float Timer => _timer;

    #endregion

    #region Inspector Variables

    [SerializeField]
    [Header("タイマー")]
    private float _timer = 0f;

    #endregion

    #region Member Variables

    private bool _isCounting = false;

    #endregion

    #region Unity Methods

    protected override void Awake()
    {
        base.Awake();
        StartTimer();
    }

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

    #region Private Methods

    async private void CountTimer()
    {
        while(_isCounting)
        {
            _timer += Time.deltaTime;
            await UniTask.NextFrame();
        }
    }

    #endregion
}
