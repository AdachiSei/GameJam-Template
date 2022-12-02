using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スコアを管理するManager
/// </summary>
public class ScoreManager : SingletonMonoBehaviour<ScoreManager>
{
    #region Public Property

    public int Score => _score;

    #endregion

    #region Inspector Member

    [SerializeField]
    [Header("スコア")]
    private int _score = 0;

    #endregion

    #region Public Methods

    public void AddScore(int score)
    {
        _score += score;
    }

    public void Init()
    {
        _score = 0;
    }

    #endregion
}
