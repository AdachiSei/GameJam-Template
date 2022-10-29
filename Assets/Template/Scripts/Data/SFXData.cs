using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// SFX(効果音)を格納するScriptableObject
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
    [Header("名前")]
    private string _name;

    [SerializeField]
    [Header("BGMの種類")]
    private SFXType _type;

    [SerializeField]
    [Header("SFXのクリップ")]
    private AudioClip _audioClip;
}
