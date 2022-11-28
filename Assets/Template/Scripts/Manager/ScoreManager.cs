using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スコアを管理するManager
/// </summary>
public class ScoreManager : SingletonMonoBehaviour<ScoreManager>
{
    public int Score => _score;

    [SerializeField]
    [Header("スコア")]
    private int _score;

    public void AddScore(int score)
    {
        _score += score;
    }

    public void Init()
    {
        _score = 0;
    }
}
