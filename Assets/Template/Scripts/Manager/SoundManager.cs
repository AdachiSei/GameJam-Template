using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DisturbMagic;
using Cysharp.Threading.Tasks;

/// <summary>
/// サウンドを管理するScript
/// </summary>
public class SoundManager : SingletonMonoBehaviour<SoundManager>
{
    #region Public Property

    public int AudioCount => _audioCount;
    public bool IsStopCreate => _isStopCreate;

    #endregion

    #region Inspector Menber

    [SerializeField]
    [Header("最初に流すBGM")]
    private BGMType _type;

    [SerializeField]
    [Header("音楽")]
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

    [SerializeField]
    [Header("BGM用のオーディオ")]
    private List<AudioSource> _bGMAudios = new();

    [SerializeField]
    [Header("BGM用のオーディオ")]
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
        //オーディオソースが付いているプレファブが無かったら
        if (_audioSorcePrefab == null)
        {
            CreateAudio();
        }
        if (_bGMAudioGameObject == null)
        {
            CreateBGMAudioGameObject();
        }
        if (_sFXAudioGameObject == null)
        {
            CreateSFXAudioGameObject();
        }
        //ポーズ用
        PauseManager.Instance.OnPause += Pause;
        PauseManager.Instance.OnUnPause += UnPause;
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// 音楽(BGM)を再生する関数
    /// </summary>
    /// <param name="name">Dataに設定した音楽(BGM)の名前</param>
    /// <param name="volume">音の大きさ</param>
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
        Debug.Log("BGMが見つからなかった");
    }

    /// <summary>
    /// 音楽(BGM)を再生する関数
    /// </summary>
    /// <param name="name">Dataに設定した音楽(BGM)の名前</param>
    public void PlayBGM(string name) =>
        PlayBGM(name, DMFloat.ONE);

    /// <summary>
    /// 音楽(BGM)を再生する関数
    /// </summary>
    /// <param name="type">音楽(BGM)の種類</param>
    /// <param name="volume">音の大きさ</param
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
        Debug.Log("BGMが見つからなかった");
    }

    /// <summary>
    /// 音楽(BGM)を再生する関数
    /// </summary>
    ///  /// <param name="type">音楽(BGM)の種類</param>
    public void PlayBGM(BGMType type) =>
        PlayBGM(type, DMFloat.ONE);

    /// <summary>
    /// 効果音(SFX)を再生する関数
    /// </summary>
    /// <param name="name">Dataに設定した効果音(SFX)の名前</param>
    /// <param name="volume">音の大きさ</param>
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
        Debug.Log("SFXが見つからなかった");
    }

    /// <summary>
    /// 効果音(SFX)を再生する関数
    /// </summary>
    /// <param name="name">Dataに設定した効果音(SFX)の名前</param>
    public void PlaySFX(string name) =>
        PlaySFX(name, DMFloat.ONE);

    /// <summary>
    /// 効果音(SFX)を再生する関数
    /// </summary>
    /// <param name="type">効果音(SFX)の種類</param>
    /// <param name="volume">音の大きさ</param>
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
    /// 効果音(SFX)を再生する関数
    /// </summary>
    /// /// <param name="type">効果音(SFX)の種類</param>
    public void PlaySFX(SFXType type) =>
        PlaySFX(type, DMFloat.ONE);

    #endregion

    #region Inspector Methods

    /// <summary>
    /// 生成するSFX用Audioの数を変更する関数
    /// </summary>
    /// <param name="count">生成するAudioの数</param>
    public void ChangeAudioCount(int count) =>
        _audioCount = count;

    /// <summary>
    /// BGM用のPrefabを生成する関数
    /// </summary>
    public void CreateBGM()
    {
        if (_audioSorcePrefab == null)
        {
            CreateAudio();
        }
        if(_bGMAudioGameObject == null)
        {
            CreateBGMAudioGameObject();
        }
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
    /// SFX用のPrefabを生成する関数
    /// </summary>
    public void CreateSFX()
    {
        if (_audioSorcePrefab == null)
        {
            CreateAudio();
        }
        if(_sFXAudioGameObject == null)
        {
            CreateSFXAudioGameObject();
        }
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
    /// BGM&SFX用のPrefabを全削除する関数
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
    /// 音楽を格納するオブジェクトを生成する関数
    /// </summary>
    private void CreateBGMAudioGameObject()
    {
        _bGMAudioGameObject = new();
        _bGMAudioGameObject.name = "BGM";
        _bGMAudioGameObject.transform.parent = transform;
    }

    /// <summary>
    /// 効果音を格納するオブジェクトを生成する関数
    /// </summary>
    private void CreateSFXAudioGameObject()
    {
        _sFXAudioGameObject = new();
        _sFXAudioGameObject.name = "SFX";
        _sFXAudioGameObject.transform.parent = transform;
    }

    /// <summary>
    /// オーディオソースが付きオブジェクトを生成する関数
    /// </summary>
    private void CreateAudio()
    {
        GameObject gameObj = new();
        gameObj.transform.parent = transform;
        _audioSorcePrefab = gameObj.AddComponent<AudioSource>();
        _audioSorcePrefab.playOnAwake = false;
        _audioSorcePrefab.name = "XD";
    }

    /// <summary>
    /// 実際に音楽(BGM)を再生する関数
    /// </summary>
    /// <param name="bGM">音楽(BGM)の名前</param>
    /// <param name="volume">音の大きさ</param>
    private void PlayAudioForBGM(BGM bGM,float volume)
    {
        _audioSorcePrefab.playOnAwake = false;
        foreach (var audio in _bGMAudios)
        {
            if (audio.clip == bGM.AudioClip)
            {
                audio.volume = volume;
                audio.Play();
                Debug.Log(bGM.Name + "を再生した");
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
        Debug.Log(bGM.Name + "を再生できた...");
    }

    /// <summary>
    /// 実際に効果音(SFX)を再生する関数
    /// </summary>
    /// <param name="sFX">効果音(SFX)</param>
    /// <param name="volume">音の大きさ</param>
    async private void PlayAudioForSFX(SFX sFX,float volume)
    {
        _audioSorcePrefab.playOnAwake = false;
        foreach (var audio in _sFXAudios)
        {
            if (audio.clip == null)
            {
                audio.clip = sFX.AudioClip;
                audio.volume = volume;
                audio.Play();
                Debug.Log(sFX.Name + "を再生中");
                var clipLength = sFX.AudioClip.length;
                for (float i = 0f; i < clipLength; i += Time.deltaTime)
                {
                    while (_isPause)
                    {
                        await UniTask.NextFrame();
                    }
                    await UniTask.NextFrame();
                }
                audio.clip = null;
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
        Debug.Log(sFX.Name + "を再生中...");
        await UniTask.Delay(TimeSpan.FromSeconds(sFX.AudioClip.length));
        newAudio.clip = null;
    }

    /// <summary>
    /// BGM用のPrefabを全削除する関数
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
    /// SFX用のPrefabを全削除する関数
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
    /// ポーズ用の関数
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
    /// ポーズ解除用の関数
    /// </summary>
    private void UnPause()
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
