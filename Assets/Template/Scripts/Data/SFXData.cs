using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// SFX(Œø‰Ê‰¹)‚ðŠi”[‚·‚éScriptableObject
/// </summary>
[CreateAssetMenu(fileName = "SFXData", menuName = "ScriptableObjects/SFXData", order = 0)]
public class SFXData : ScriptableObject
{
    public SFXType Type => _type;
    public AudioClip AudioClip => _audioClip;

    [SerializeField]
    [Header("BGM‚ÌŽí—Þ")]
    SFXType _type;

    [SerializeField]
    [Header("SFX‚ÌƒNƒŠƒbƒv")]
    AudioClip _audioClip;
}
