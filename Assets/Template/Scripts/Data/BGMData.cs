using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// BGM(���y)���i�[����ScriptableObject
/// </summary>
[CreateAssetMenu(fileName = "BGMData", menuName = "ScriptableObjects/BGMData", order = 0)]
public class BGMData : ScriptableObject
{
    public BGM[] BGMs => _bGMs;

    [SerializeField]
    [Header("")]
    private BGM[] _bGMs;

}
[Serializable]
public class BGM
{
    public string Name => _name;
    public BGMType Type => _type;
    public AudioClip AudioClip => _audioClip;

    [SerializeField]
    [Header("���O")]
    private string _name;

    [SerializeField]
    [Header("BGM�̎��")]
    private BGMType _type;

    [SerializeField]
    [Header("BGM")]
    private AudioClip _audioClip;
}
