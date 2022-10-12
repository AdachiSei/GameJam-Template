using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// �T�E���h���Ǘ�����Script
/// </summary>
public class SoundManager : SingletonMonoBehaviour<SoundManager>
{
    [SerializeField]
    [Header("�ŏ��ɗ���BGM")]
    BGMType _type;

    [SerializeField]
    [Header("���y")]
    private BGMData[] _bGMDatas;

    [SerializeField]
    [Header("���ʉ�")]
    private SFXData[] _sFXDatas;

    [SerializeField]
    [Header("���ʉ����i�[����I�u�W�F�N�g")]
    GameObject _pool;

    protected override void Awake()
    {
        base.Awake();
        CreatePool();
        PlayBGM(_type);
    }

    #region
    /// <summary>
    /// ���y(BGM)���Đ�����֐�
    /// </summary>
    /// <param name="type">�Đ����������y(BGM)</param>
    private void PlayBGM(BGMType type)
    {
        foreach (var bGM in _bGMDatas)
        {
            if (bGM.Type == type)
            {
                bGM.AudioSource.Play();
                return;
            }
        }
        Debug.Log("BGM���Đ����Ă��܂���");
    }

    /// <summary>
    /// ���y(BGM)���Đ�����֐�
    /// </summary>
    /// <param name="type">�Đ����������y(BGM)</param>
    /// <param name="volume">���y(BGM)�̃{�����[��</param>
    private void PlayBGM(BGMType type, float volume)
    {
        foreach (var bGM in _bGMDatas)
        {
            if (bGM.Type == type)
            {
                bGM.AudioSource.volume = volume;
                bGM.AudioSource.Play();
                return;
            }
        }
        Debug.Log("BGM���Đ����Ă��܂���");
    }
    #endregion

    /// <summary>
    /// ���ʉ�(SFX)���Đ�����֐�
    /// </summary>
    /// <param name="type">�Đ����������ʉ�(SFX)</param>
    private void PlaySFX(SFXType type)
    {
        foreach (var sFX in _sFXDatas)
        {
            if(sFX.Type == type)
            {
                //sFX.AudioClip
                return;
            }
        }
    }

    private void PlaySFX(SFXType type,float volume)
    {
        foreach (var sFX in _sFXDatas)
        {
            if (sFX.Type == type)
            {
                //sFX.AudioClip
                return;
            }
        }
    }


    private void CreatePool()
    {
        Instantiate(gameObject.AddComponent<AudioSource>().clip = _sFXDatas[0].AudioClip, new(), Quaternion.identity,_pool.transform);
    }

    private void Pause()
    {
         
    }

    private void Restart()
    {

    }


    [Serializable]
    public class BGMData
    {
        public BGMType Type => _type;
        public AudioSource AudioSource => _audioSource;

        [SerializeField]
        [Header("���O")]
        private string _name;

        [SerializeField]
        [Header("BGM�̎��")]
        private BGMType _type;

        [SerializeField]
        [Header("BGM")]
        private AudioSource _audioSource;
    }
}
