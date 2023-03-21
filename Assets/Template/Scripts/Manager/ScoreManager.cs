using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スコアを管理するManager
/// </summary>
public static class ScoreManager
{
    #region Property

    public static int Score { get; private set; }

    #endregion

    #region Public Methods

    public static void AddScore(int score)
    {
        Score += score;
    }

    public static void Init()
    {
        Score = 0;
    }

    #endregion
}
