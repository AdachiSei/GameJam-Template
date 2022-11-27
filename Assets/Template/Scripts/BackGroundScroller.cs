using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �w�i���X�N���[�����Ă����Script
/// </summary>
public class BackGroundScroller : MonoBehaviour
{
    #region Inspector Member

    [SerializeField]
    [Header("�w�i")]
    SpriteRenderer[] _background = new SpriteRenderer[2];

    [SerializeField]
    [Header("�X�N���[���X�s�[�h")]
    float _speed = -0.03f;

    [SerializeField]
    [Header("���̒n�_�𒴂����獶�Ƀ��[�v����")]
    float _limitPosX = -12f;

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
