using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// サウンドを管理するScript
/// </summary>
public class SoundManager : SingletonMonoBehaviour<SoundManager>
{
    [SerializeField]
    BGMData[] _bGMDatas;

    [SerializeField]
    SFXData[] _sFXDatas;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    [Serializable]
    public class BGMData
    {
        public BGMType Type => _type;
        public AudioSource AudioSource => _audioSource;

        [SerializeField]
        [Header("名前")]
        string _name;

        [SerializeField]
        [Header("BGMの種類")]
        BGMType _type;

        [SerializeField]
        [Header("BGM")]
        AudioSource _audioSource;
    }
}
