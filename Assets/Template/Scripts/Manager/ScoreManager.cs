using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �X�R�A���Ǘ�����Manager
/// </summary>
public class ScoreManager : SingletonMonoBehaviour<ScoreManager>
{
    public int Score => _score;

    [SerializeField]
    [Header("�X�R�A")]
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
