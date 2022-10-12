using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// SFX(���ʉ�)���i�[����ScriptableObject
/// </summary>
[CreateAssetMenu(fileName = "SFXData", menuName = "ScriptableObjects/SFXData", order = 0)]
public class SFXData : ScriptableObject
{
    public SFXType Type => _type;
    public AudioClip AudioClip => _audioClip;

    [SerializeField]
    [Header("BGM�̎��")]
    private SFXType _type;

    [SerializeField]
    [Header("SFX�̃N���b�v")]
    private AudioClip _audioClip;
}
