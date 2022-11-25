using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 背景をスクロールしてくれるScript
/// </summary>
public class BackGroundScroller : MonoBehaviour
{
    #region Inspector Member

    [SerializeField]
    [Header("背景")]
    SpriteRenderer[] _background = new SpriteRenderer[2];

    [SerializeField]
    [Header("スクロールスピード")]
    float _speed = 0.03f;

    [SerializeField]
    [Header("この地点を超えたら左にワープする")]
    float _limitPosX = 25.5f;

    #endregion

    #region Public Member

    bool _isPlaying = true;

    #endregion

    #region Const Member

    const int ONE_OTHER = 1;

    #endregion

    #region Unity Methods

    void Awake()
    {
        PauseManager.Instance.OnPause += Pause;
        PauseManager.Instance.OnResume += Resume;
    }

    void Update()
    {
        if (_isPlaying)
        {
            _background[0].gameObject.transform.TranslateX(_speed);
            _background[ONE_OTHER].gameObject.transform.TranslateX(_speed);

            if (_background[0].gameObject.transform.position.x <= _limitPosX)
            {
                _background[0].gameObject.transform.ChangePosX(-_limitPosX);
            }
            else if (_background[ONE_OTHER].gameObject.transform.position.x <= _limitPosX)
            {
                _background[ONE_OTHER].gameObject.transform.ChangePosX(-_limitPosX);
            }
        }
    }

    private void OnDisable()
    {
        PauseManager.Instance.OnPause -= Pause;
        PauseManager.Instance.OnResume -= Resume;
    }

    #endregion

    #region Private Methods

    private void Pause()
    {
        _isPlaying = false;
    }

    private void Resume()
    {
        _isPlaying = true;
    }

    #endregion
}
