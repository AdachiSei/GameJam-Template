using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// SFX(���ʉ�)���i�[����ScriptableObject
/// </summary>
[CreateAssetMenu(fileName = "SFXData", menuName = "ScriptableObjects/SFXData", order = 1)]
public class SFXData : ScriptableObject
{
    public SFX[] SFXes => _sFXes;

    [SerializeField]
    [Header("")]
    private SFX[] _sFXes;

}
[Serializable]
public class SFX
{
    public string Name => _name;
    public SFXType Type => _type;
    public AudioClip AudioClip => _audioClip;

    [SerializeField]
    [Header("���O")]
    private string _name;

    [SerializeField]
    [Header("BGM�̎��")]
    private SFXType _type;

    [SerializeField]
    [Header("SFX�̃N���b�v")]
    private AudioClip _audioClip;
}
