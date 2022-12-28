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
    #region Public Property

    public BGM[] BGMs => _bGM;

    #endregion

    #region Inspector Member

    [SerializeField]
    [Header("���y")]
    private BGM[] _bGM;

    #endregion

    #region Serializable Class

    [Serializable]
    public class BGM
    {
        public string Name => _name;
        public AudioClip AudioClip => _audioClip;

        [SerializeField]
        [Header("���O")]
        private string _name;

        [SerializeField]
        [Header("BGM")]
        private AudioClip _audioClip;
    }

    #endregion
}
