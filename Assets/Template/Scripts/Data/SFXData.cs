using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// SFX(効果音)を格納するScriptableObject
/// </summary>
[CreateAssetMenu(fileName = "SFXData", menuName = "ScriptableObjects/SFXData", order = 0)]
public class SFXData : ScriptableObject
{
    public SFXType Type => _type;
    public AudioClip AudioClip => _audioClip;

    [SerializeField]
    [Header("BGMの種類")]
    private SFXType _type;

    [SerializeField]
    [Header("SFXのクリップ")]
    private AudioClip _audioClip;
}
