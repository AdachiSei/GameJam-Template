using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DisturbMagic;

/// <summary>
/// サウンドを管理するScript
/// </summary>
public class SoundManager : SingletonMonoBehaviour<SoundManager>
{
    public int AudioCount => _audioCount;
    public bool IsStopCreate => _isStopCreate;

    #region inspector menber

    [SerializeField]
    [Header("最初に流すBGM")]
    private BGMType _type;

    [SerializeField]
    [Header("音楽")]
    private BGMDatas[] _bGMDatas;

    [SerializeField]
    [Header("")]
    private BGMData _bGMData;

    [SerializeField]
    [Header("効果音")]
    private SFXData _sFXData;

    [SerializeField]
    [Header("音楽を格納するオブジェクト")]
    private GameObject _bGMAudioGameObject;

    [SerializeField]
    [Header("効果音を格納するオブジェクト")]
    private GameObject _sFXAudioGameObject;

    [SerializeField]
    [Header("オーディオソースがついているプレファブ")]
    private AudioSource _audioSorcePrefab;

    #endregion

    private int _audioCount;
    private bool _isStopCreate;
    List<AudioSource> _bGMAudios = new();
    List<AudioSource> _sFXAudios = new();

    public void ChangeCreateAudioCount(int count)
    {
        _audioCount = count;
    }

    protected override void Awake()
    {
        base.Awake();
        //CreateSFX();
        PlayBGM("Test");
    }

    #region

    void PlayAudio(BGM bGM)
    {
        foreach (var audio in _bGMAudios)
        {
            if (audio.clip == bGM.AudioClip)
            {
                audio.Play();
                Debug.Log(audio.clip.name + "を再生中");
                return;
            }
        }
        Debug.Log("BGMを再生できなかった");
    }

    public void PlayBGM(string name)
    {
        foreach (var bGM in _bGMData.BGMs)
        {
            if (bGM.Name == name)
            {
                PlayAudio(bGM);
                return;
            }
        }
        Debug.Log("BGMが見つからなかった");
    }

    /// <summary>
    /// 音楽(BGM)を再生する関数
    /// </summary>
    /// <param name="type">再生したい音楽(BGM)</param>
    public void PlayBGM(BGMType type)
    {
        foreach (var bGM in _bGMDatas)
        {
            if (bGM.Type == type)
            {
                bGM.AudioSource.Play();
                return;
            }
        }
        Debug.Log("BGMを再生していない");
    }

    /// <summary>
    /// 音楽(BGM)を再生する関数
    /// </summary>
    /// <param name="type">再生したい音楽(BGM)</param>
    /// <param name="volume">音楽(BGM)のボリューム</param>
    public void PlayBGM(BGMType type, float volume)
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
        Debug.Log("BGMを再生していない");
    }

    #endregion

    /// <summary>
    /// 効果音(SFX)を再生する関数
    /// </summary>
    /// <param name="type">再生したい効果音(SFX)</param>
    public void PlaySFX(SFXType type)
    {
        foreach (var sFX in _sFXData.SFXes)
        {
            if(sFX.Type == type)
            {
                foreach (var audio in _sFXAudios)
                {
                    if(audio.clip != null)
                    {
                        audio.clip = sFX.AudioClip;
                        audio.Play();
                        return;
                    }
                }
            }
        }
    }

    public void PlaySFX(SFXType type,float volume)
    {
        foreach (var sFX in _sFXData.SFXes)
        {
            if (sFX.Type == type)
            {
                //sFX.AudioClip
                return;
            }
        }
    }  

    public void CreateBGM()
    {
        _isStopCreate = false;
        _bGMAudios.Clear();
        while (true)
        {
            var children = _bGMAudioGameObject.transform;
            if (children.childCount == DMInt.ZERO)
            {
                break;
            }
            DestroyImmediate(children.GetChild(DMInt.ZERO).gameObject);
        }
        
        if (_bGMAudios.Count >= _bGMData.BGMs.Length)
        {
            return;
        }
        for (var i = 0; i < _bGMData.BGMs.Length; i++)
        {
            _bGMAudios
                .Add(Instantiate(_audioSorcePrefab,
                             new(),
                            Quaternion.identity,
                            _bGMAudioGameObject.transform));
            _bGMAudios[i].clip = _bGMData.BGMs[i].AudioClip;
            _bGMAudios[i].loop = true;
        }
        _bGMAudios = new(_bGMAudios.Distinct());
    }
    
    public void CreateSFX()
    {
        _isStopCreate = false;
        _sFXAudios.Clear();
        while (true)
        {
            var children = _sFXAudioGameObject.transform;
            if (children.childCount == DMInt.ZERO)
            {
                break;
            }
            DestroyImmediate(children.GetChild(DMInt.ZERO).gameObject);
        }
        for (var i = 0; i < _audioCount; i++)
        {
            _sFXAudios
               .Add(Instantiate(_audioSorcePrefab,
                           new(),
                           Quaternion.identity,
                           _sFXAudioGameObject.transform));
        }
    }

    public void Init()
    {
        _isStopCreate = true;
        _bGMAudios.Clear();
        _sFXAudios.Clear();
        while (true)
        {
            var children = _bGMAudioGameObject.transform;
            if (children.childCount == DMInt.ZERO)
            {
                break;
            }
            DestroyImmediate(children.GetChild(DMInt.ZERO).gameObject);
        }
        while (true)
        {
            var children = _sFXAudioGameObject.transform;
            if (children.childCount == DMInt.ZERO)
            {
                break;
            }
            DestroyImmediate(children.GetChild(DMInt.ZERO).gameObject);
        }
    }

    private void Pause()
    {
        foreach (var bGMAudio in _bGMDatas)
        {
            if(bGMAudio.AudioSource.isPlaying)
            {
                bGMAudio.AudioSource.Pause();
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
        foreach (var bGMAudio in _bGMDatas)
        {
            bGMAudio.AudioSource.UnPause();
        }
        foreach (var sFXAudio in _sFXAudios)
        {
            sFXAudio.UnPause();
        }
    }


    [Serializable]
    public class BGMDatas
    {
        public string Name => _name;
        public BGMType Type => _type;
        public AudioSource AudioSource => _audioSource;

        [SerializeField]
        [Header("名前")]
        private string _name;

        [SerializeField]
        [Header("BGMの種類")]
        private BGMType _type;

        [SerializeField]
        [Header("BGM")]
        private AudioSource _audioSource;
    }
}
