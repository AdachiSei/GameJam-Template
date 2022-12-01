using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// BGM(‰¹Šy)‚ðŠi”[‚·‚éScriptableObject
/// </summary>
[CreateAssetMenu(fileName = "BGMData", menuName = "ScriptableObjects/BGMData", order = 0)]
public class BGMData : ScriptableObject
{
    #region Public Property

    public BGM[] BGMs => _bGM;

    #endregion

    #region Inspector Member

    [SerializeField]
    [Header("‰¹Šy")]
    private BGM[] _bGM;

    #endregion

    #region Serializable Class

    [Serializable]
    public class BGM
    {
        public string Name => _name;
        public AudioClip AudioClip => _audioClip;

        [SerializeField]
        [Header("–¼‘O")]
        private string _name;

        [SerializeField]
        [Header("BGM")]
        private AudioClip _audioClip;
    }

    #endregion
}
