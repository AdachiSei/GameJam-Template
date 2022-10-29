using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DisturbMagic;

/// <summary>
/// �T�E���h���Ǘ�����Script
/// </summary>
public class SoundManager : SingletonMonoBehaviour<SoundManager>
{
    #region Public Property

    public int AudioCount => _audioCount;
    public bool IsStopCreate => _isStopCreate;

    #endregion

    #region Inspector Menber

    [SerializeField]
    [Header("�ŏ��ɗ���BGM")]
    private BGMType _type;

    [SerializeField]
    [Header("���y")]
    private BGMData _bGMData;

    [SerializeField]
    [Header("���ʉ�")]
    private SFXData _sFXData;

    [SerializeField]
    [Header("���y���i�[����I�u�W�F�N�g")]
    private GameObject _bGMAudioGameObject;

    [SerializeField]
    [Header("���ʉ����i�[����I�u�W�F�N�g")]
    private GameObject _sFXAudioGameObject;

    [SerializeField]
    [Header("�I�[�f�B�I�\�[�X�����Ă���v���t�@�u")]
    private AudioSource _audioSorcePrefab;

    [SerializeField]
    [Header("BGM�p�̃I�[�f�B�I")]
    List<AudioSource> _bGMAudios = new();

    [SerializeField]
    [Header("BGM�p�̃I�[�f�B�I")]
    List<AudioSource> _sFXAudios = new();

    #endregion

    #region Private Menber

    private int _audioCount;
    private int _newAudioNum;
    private bool _isStopCreate;

    #endregion

    #region Unity Method

    protected override void Awake()
    {
        base.Awake();
        PlayBGM("Test");
        PlaySFX("Test");
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// ���y(BGM)���Đ�����֐�
    /// </summary>
    /// <param name="name">Data�ɐݒ肵�����y(BGM)�̖��O</param>
    /// <param name="volume">���̑傫��</param>
    public void PlayBGM(string name,float volume)
    {
        foreach (var bGM in _bGMData.BGMs)
        {
            if (bGM.Name == name)
            {
                PlayAudioForBGM(bGM, volume);
                return;
            }
        }
        Debug.Log("BGM��������Ȃ�����");
    }

    /// <summary>
    /// ���y(BGM)���Đ�����֐�
    /// </summary>
    /// <param name="name">Data�ɐݒ肵�����y(BGM)�̖��O</param>
    public void PlayBGM(string name) =>
        PlayBGM(name, DMFloat.ONE);

    /// <summary>
    /// ���y(BGM)���Đ�����֐�
    /// </summary>
    /// <param name="type">���y(BGM)�̎��</param>
    /// <param name="volume">���̑傫��</param
    public void PlayBGM(BGMType type,float volume)
    {
        foreach (var bGM in _bGMData.BGMs)
        {
            if (bGM.Type == type)
            {
                PlayAudioForBGM(bGM, volume);
                return;
            }
        }
        Debug.Log("BGM��������Ȃ�����");
    }

    /// <summary>
    /// ���y(BGM)���Đ�����֐�
    /// </summary>
    ///  /// <param name="type">���y(BGM)�̎��</param>
    public void PlayBGM(BGMType type) =>
        PlayBGM(type, DMFloat.ONE);

    /// <summary>
    /// ���ʉ�(SFX)���Đ�����֐�
    /// </summary>
    /// <param name="name">Data�ɐݒ肵�����ʉ�(SFX)�̖��O</param>
    /// <param name="volume">���̑傫��</param>
    public void PlaySFX(string name,float volume)
    {
        foreach (var sFX in _sFXData.SFXes)
        {
            if (sFX.Name == name)
            {
                PlayAudioForSFX(sFX, volume);
                return;
            }
        }
        Debug.Log("SFX��������Ȃ�����");
    }

    /// <summary>
    /// ���ʉ�(SFX)���Đ�����֐�
    /// </summary>
    /// <param name="name">Data�ɐݒ肵�����ʉ�(SFX)�̖��O</param>
    public void PlaySFX(string name) =>
        PlayBGM(name, DMFloat.ONE);

    /// <summary>
    /// ���ʉ�(SFX)���Đ�����֐�
    /// </summary>
    /// <param name="type">���ʉ�(SFX)�̎��</param>
    /// <param name="volume">���̑傫��</param>
    public void PlaySFX(SFXType type,float volume)
    {
        foreach (var sFX in _sFXData.SFXes)
        {
            if(sFX.Type == type)
            {
                PlayAudioForSFX(sFX, volume);
            }
        }
    }

    /// <summary>
    /// ���ʉ�(SFX)���Đ�����֐�
    /// </summary>
    /// /// <param name="type">���ʉ�(SFX)�̎��</param>
    public void PlaySFX(SFXType type) =>
        PlaySFX(type, DMFloat.ONE);

    #endregion

    #region Inspector Methods

    /// <summary>
    /// ��������SFX�pAudio�̐���ύX����֐�
    /// </summary>
    /// <param name="count">��������Audio�̐�</param>
    public void ChangeAudioCount(int count) =>
        _audioCount = count;

    /// <summary>
    /// BGM�p��Prefab�𐶐�����֐�
    /// </summary>
    public void CreateBGM()
    {
        _isStopCreate = false;
        _bGMAudios.Clear();
        InitBGM();
        for (var i = 0; i < _bGMData.BGMs.Length; i++)
        {
            var audio =
            Instantiate(_audioSorcePrefab,
                        new(),
                        Quaternion.identity,
                        _bGMAudioGameObject.transform);
            _bGMAudios.Add(audio);
            audio.name = _bGMData.BGMs[i].Name;
            audio.clip = _bGMData.BGMs[i].AudioClip;
            audio.loop = true;
        }
        _bGMAudios = new(_bGMAudios.Distinct());
    }
    
    /// <summary>
    /// SFX�p��Prefab�𐶐�����֐�
    /// </summary>
    public void CreateSFX()
    {
        _isStopCreate = false;
        _sFXAudios.Clear();
        InitSFX();
        for (var i = 0; i < _audioCount; i++)
        {
            var audio =
            Instantiate(_audioSorcePrefab,
                        new(),
                        Quaternion.identity,
                        _sFXAudioGameObject.transform);

            if (i < 10)
                audio.name = "SFX " + "00" + i;
            else if (i < 100)
                audio.name = "SFX " + "0" + i;
            else
                audio.name = "SFX " + i;

            _sFXAudios.Add(audio);
        }
    }

    /// <summary>
    /// BGM&SFX�p��Prefab��S�폜����֐�
    /// </summary>
    public void Init()
    {
        _isStopCreate = true;
        _bGMAudios.Clear();
        _sFXAudios.Clear();
        InitBGM();
        InitSFX();
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// BGM�p��Prefab��S�폜����֐�
    /// </summary>
    private void InitBGM()
    {
        while (true)
        {
            var children = _bGMAudioGameObject.transform;
            var empty = children.childCount == DMInt.ZERO;
            if (empty) break;
            var DestroyGO = children.GetChild(DMInt.ZERO).gameObject;
            DestroyImmediate(DestroyGO);
        }
    }

    /// <summary>
    /// SFX�p��Prefab��S�폜����֐�
    /// </summary>
    private void InitSFX()
    {
        while (true)
        {
            var children = _sFXAudioGameObject.transform;
            if (children.childCount == DMInt.ZERO)
            {
                break;
            }
            var DestroyGO = children.GetChild(DMInt.ZERO).gameObject;
            DestroyImmediate(DestroyGO);
        }
    }

    /// <summary>
    /// ���ۂɉ��y(BGM)���Đ�����֐�
    /// </summary>
    /// <param name="bGM">���y(BGM)�̖��O</param>
    /// <param name="volume">���̑傫��</param>
    private void PlayAudioForBGM(BGM bGM,float volume)
    {
        foreach (var audio in _bGMAudios)
        {
            if (audio.clip == bGM.AudioClip)
            {
                audio.volume = volume;
                audio.Play();
                Debug.Log(audio.clip.name + "���Đ���");
                return;
            }
        }
        var newAudio =
            Instantiate(_audioSorcePrefab,
                        new(),
                        Quaternion.identity,
                        _bGMAudioGameObject.transform);
        _bGMAudios.Add(newAudio);
        newAudio.clip = bGM.AudioClip;
        newAudio.loop = true;
        Debug.Log("BGM���Đ��ł���...");
    }

    /// <summary>
    /// ���ۂɌ��ʉ�(SFX)���Đ�����֐�
    /// </summary>
    /// <param name="sFX">���ʉ�(SFX)</param>
    /// <param name="volume">���̑傫��</param>
    private void PlayAudioForSFX(SFX sFX,float volume)
    {
        foreach (var audio in _sFXAudios)
        {
            if (audio.clip == null)
            {
                audio.clip = sFX.AudioClip;
                audio.volume = volume;
                audio.Play();
                Debug.Log(audio.clip.name + "���Đ���");
                return;
            }
        }
        var gameObject =
            Instantiate(_audioSorcePrefab,
                        new(),
                        Quaternion.identity,
                        _sFXAudioGameObject.transform);
        gameObject.name = "NewSFX " + _newAudioNum;
        _newAudioNum++;
        _sFXAudios.Add(gameObject);
        var newAudio = _sFXAudios[_sFXAudios.Count - DMInt.ONE];
        newAudio.clip = sFX.AudioClip;
        newAudio.volume = volume;
        newAudio.Play();
        Debug.Log(newAudio.clip.name + "���Đ���");
        Debug.Log("SFX���Đ��ł���...");
    }

    /// <summary>
    /// �|�[�Y�p�̊֐�
    /// </summary>
    private void Pause()
    {
        foreach (var bGMAudio in _bGMAudios)
        {
            if (bGMAudio.isPlaying)
            {
                bGMAudio.Pause();
            }
        }
        foreach (var sFXAudio in _sFXAudios)
        {
            if (sFXAudio.isPlaying)
            {
                sFXAudio.Pause();
            }
        }
    }

    /// <summary>
    /// �|�[�Y�����p�̊֐�
    /// </summary>
    private void Restart()
    {
        foreach (var bGMAudio in _bGMAudios)
        {
            bGMAudio.UnPause();
        }
        foreach (var sFXAudio in _sFXAudios)
        {
            sFXAudio.UnPause();
        }
    }

    #endregion
}
