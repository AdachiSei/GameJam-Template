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
    public int AudioCount => _audioCount;
    public bool IsStopCreate => _isStopCreate;

    #region inspector menber

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
    List<AudioSource> _bGMAudios = new();

    [SerializeField]
    List<AudioSource> _sFXAudios = new();

    #endregion

    #region private menber

    private int _audioCount;
    private int _newAudioNum;
    private bool _isStopCreate;

    #endregion


    protected override void Awake()
    {
        base.Awake();
        //PlayBGM("Test");
        PlaySFX(SFXType.Empty);
    }

    #region bgm methods

    /// <summary>
    /// ���y(BGM)���Đ�����֐�
    /// </summary>
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
    public void PlayBGM(string name) =>
        PlayBGM(name, DMFloat.ONE);

    /// <summary>
    /// ���y(BGM)���Đ�����֐�
    /// </summary>
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
    public void PlayBGM(BGMType type) =>
        PlayBGM(type, DMFloat.ONE);

    /// <summary>
    /// ���ۂɉ��y(BGM)���Đ�����֐�
    /// </summary>
    void PlayAudioForBGM(BGM bGM,float volume)
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

    #endregion

    #region sfx methods

    /// <summary>
    /// ���ۂɌ��ʉ�(SFX)���Đ�����֐�
    /// </summary>
    void PlayAudioForSFX(SFX sFX,float volume)
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
    /// ���ʉ�(SFX)���Đ�����֐�
    /// </summary>
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
    public void PlaySFX(string name) =>
        PlayBGM(name, DMFloat.ONE);

    /// <summary>
    /// ���ʉ�(SFX)���Đ�����֐�
    /// </summary>
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
    public void PlaySFX(SFXType type) =>
        PlaySFX(type, DMFloat.ONE);

    #endregion

    public void ChangeAudioCount(int count) =>
        _audioCount = count;

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
            _bGMAudios[i].clip = _bGMData.BGMs[i].AudioClip;
            _bGMAudios[i].loop = true;
        }
        _bGMAudios = new(_bGMAudios.Distinct());
    }
    
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
    /// BGM&SFX�p��Prefab��S�폜
    /// </summary>
    public void Init()
    {
        _isStopCreate = true;
        _bGMAudios.Clear();
        _sFXAudios.Clear();
        InitBGM();
        InitSFX();
    }

    /// <summary>
    /// BGM�p��Prefab��S�폜
    /// </summary>
    void InitBGM()
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
    /// SFX�p��Prefab��S�폜
    /// </summary>
    void InitSFX()
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
}
