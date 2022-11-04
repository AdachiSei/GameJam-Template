using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DisturbMagic;
using Cysharp.Threading.Tasks;

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
    private string _name;

    [SerializeField]
    [Header("���y")]
    private BGMData _bGMData;

    [SerializeField]
    [Header("���ʉ�")]
    private SFXData _sFXData;

    [SerializeField]
    [Header("���y���i�[����I�u�W�F�N�g")]
    private GameObject _bGMParent;

    [SerializeField]
    [Header("���ʉ����i�[����I�u�W�F�N�g")]
    private GameObject _sFXParent;

    [SerializeField]
    [Header("�I�[�f�B�I�\�[�X�����Ă���v���t�@�u")]
    private AudioSource _audioPrefab;

    [SerializeField]
    [Header("BGM�p�̃I�[�f�B�I")]
    private List<AudioSource> _bGMAudios = new();

    [SerializeField]
    [Header("SFX�p�̃I�[�f�B�I")]
    private List<AudioSource> _sFXAudios = new();

    #endregion

    #region Private Menber

    private int _audioCount;
    private int _newAudioNum;
    private bool _isStopCreate;
    private bool _isPause;

    #endregion

    #region Unity Method

    protected override void Awake()
    {
        base.Awake();
        if (_audioPrefab == null)//�I�[�f�B�I�̃v���t�@�u������������
        {
            CreateAudio();
        }
        if (_bGMParent == null)//BGM���i�[����I�u�W�F�N�g������������
        {
            CreateBGMParent();
        }
        if (_sFXParent == null)//SFX���i�[����I�u�W�F�N�g������������
        {
            CreateSFXParent();
        }
        _audioPrefab.playOnAwake = false;
        //�|�[�Y�p
        PauseManager.Instance.OnPause += Pause;
        PauseManager.Instance.OnResume += Resume;
        PlayBGM("Test");
        PlaySFX("Test");
    }

    private void OnDisable()
    {
        PauseManager.Instance.OnPause -= Pause;
        PauseManager.Instance.OnResume -= Resume;
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// ���y(BGM)���Đ�����֐�
    /// </summary>
    /// <param name="name">Data�ɐݒ肵�����y(BGM)�̖��O</param>
    /// <param name="volume">���̑傫��</param>
    public void PlayBGM(string name,float volume = 1)
    {
        //�ŏ���BGM���~�߂�
        foreach(var audio in _bGMAudios)
        {
            audio.Stop();
        }
        //�Đ������������i�[���Ă���I�u�W�F�N�g����i�荞��
        foreach (var audio in _bGMAudios)
        {
            if (audio.name == name)
            {
                audio.volume = volume;
                audio.Play();
                return;
            }
        }
        //�Đ������������i�[���Ă���I�u�W�F�N�g������������
        //�Đ�����������Data����i�荞��
        foreach (var bGM in _bGMData.BGMs)
        {
            if (bGM.Name == name)
            {
                //�Đ�������������Audio�𐶐�
                var newAudio =
                Instantiate(_audioPrefab,
                        new(),
                        Quaternion.identity,
                        _bGMParent.transform);
                _bGMAudios.Add(newAudio);
                newAudio.volume = volume;
                newAudio.clip = bGM.AudioClip;
                newAudio.name = bGM.Name;
                newAudio.loop = true;
                newAudio.Play();
                return;
            }
        }
        Debug.Log("BGM��������Ȃ�����");
    }

    /// <summary>
    /// ���ʉ�(SFX)���Đ�����֐�
    /// </summary>
    /// <param name="name">Data�ɐݒ肵�����ʉ�(SFX)�̖��O</param>
    /// <param name="volume">���̑傫��</param>
    async public void PlaySFX(string name, float volume = 1)
    {
        //�Đ�����������Data������i�荞��
        foreach (var sFX in _sFXData.SFXes)
        {
            if (sFX.Name == name)
            {
                //Clip��null��Audio��T��
                foreach (var audio in _sFXAudios)
                {
                    if (audio.clip == null)
                    {
                        audio.clip = sFX.AudioClip;
                        audio.volume = volume;
                        var privName = audio.name;
                        audio.name = sFX.Name;
                        audio.Play();
                        await UniTaskSeconds(sFX.AudioClip.length);
                        audio.name = privName;
                        audio.clip = null;
                        return;
                    }
                }
                //����������V�������
                var gameObject =
                    Instantiate(_audioPrefab,
                                new(),
                                Quaternion.identity,
                                _sFXParent.transform);
                gameObject.name = "NewSFX " + _newAudioNum;
                _newAudioNum++;
                _sFXAudios.Add(gameObject);
                var newAudio = _sFXAudios[_sFXAudios.Count - DMInt.ONE];
                newAudio.clip = sFX.AudioClip;
                newAudio.volume = volume;
                var newPrivName = newAudio.name;
                newAudio.name = sFX.Name;
                newAudio.Play();
                await UniTaskSeconds(sFX.AudioClip.length);
                newAudio.name = newPrivName;
                newAudio.clip = null;
                return;
            }
        }
        Debug.Log("SFX��������Ȃ�����");
    }

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
        if (_audioPrefab == null)
        {
            CreateAudio();
        }
        if(_bGMParent == null)
        {
            CreateBGMParent();
        }
        _isStopCreate = false;
        _bGMAudios.Clear();
        InitBGM();
        for (var i = 0; i < _bGMData.BGMs.Length; i++)
        {
            var audio =
            Instantiate(_audioPrefab,
                        new(),
                        Quaternion.identity,
                        _bGMParent.transform);
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
        if (_audioPrefab == null)
        {
            CreateAudio();
        }
        if(_sFXParent == null)
        {
            CreateSFXParent();
        }
        _isStopCreate = false;
        _sFXAudios.Clear();
        InitSFX();
        for (var i = 0; i < _audioCount; i++)
        {
            var audio =
            Instantiate(_audioPrefab,
                        new(),
                        Quaternion.identity,
                        _sFXParent.transform);
            audio.loop = true;
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
    /// ���y���i�[����I�u�W�F�N�g�𐶐�����֐�
    /// </summary>
    private void CreateBGMParent()
    {
        _bGMParent = new();
        _bGMParent.name = "BGM";
        _bGMParent.transform.parent = transform;
    }

    /// <summary>
    /// ���ʉ����i�[����I�u�W�F�N�g�𐶐�����֐�
    /// </summary>
    private void CreateSFXParent()
    {
        _sFXParent = new();
        _sFXParent.name = "SFX";
        _sFXParent.transform.parent = transform;
    }

    /// <summary>
    /// �I�[�f�B�I�\�[�X���t���I�u�W�F�N�g�𐶐�����֐�
    /// </summary>
    private void CreateAudio()
    {
        GameObject gameObj = new();
        gameObj.transform.parent = transform;
        _audioPrefab = gameObj.AddComponent<AudioSource>();
        _audioPrefab.playOnAwake = false;
        _audioPrefab.name = "XD";
    }

    /// <summary>
    /// �|�[�Y�Ή��̔񓯊��ő҂��Ă����֐�
    /// </summary>
    /// <param name="time">�҂���</param>
    async private UniTask UniTaskSeconds(float time)
    {
        for (float i = 0f; i < time; i += Time.deltaTime)
        {
            while (_isPause)
            {
                await UniTask.NextFrame();
            }
            await UniTask.NextFrame();
        }
    }

    /// <summary>
    /// BGM�p��Prefab��S�폜����֐�
    /// </summary>
    private void InitBGM()
    {
        if (_bGMParent == null) return;
        while (true)
        {
            var children = _bGMParent.transform;
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
        if (_sFXParent == null) return;
        while (true)
        {
            var children = _sFXParent.transform;
            if (children.childCount == DMInt.ZERO)
            {
                break;
            }
            var DestroyGO = children.GetChild(DMInt.ZERO).gameObject;
            DestroyImmediate(DestroyGO);
        }
    }


    /// <summary>
    /// �|�[�Y�p�̊֐�
    /// </summary>
    private void Pause()
    {
        _isPause = true;
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
    private void Resume()
    {
        _isPause = false;
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
