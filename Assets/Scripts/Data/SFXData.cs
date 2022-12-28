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
    #region Public Property

    public SFX[] SFXes => _sFX;

    #endregion

    #region Inspector Member

    [SerializeField]
    [Header("���ʉ�")]
    private SFX[] _sFX;

    #endregion

    #region Serializable Class

    [Serializable]
    public class SFX
    {
        public string Name => _name;
        public AudioClip AudioClip => _audioClip;

        [SerializeField]
        [Header("���O")]
        private string _name;

        [SerializeField]
        [Header("SFX�̃N���b�v")]
        private AudioClip _audioClip;
    }

    #endregion
}
