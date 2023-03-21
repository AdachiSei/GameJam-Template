using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UniRx;

/// <summary>
/// サウンドを管理するScript
/// </summary>
public class SoundManager : SingletonMonoBehaviour<SoundManager>
{
    #region Public Properties

    public float MasterVolume => _masterVolume;
    public float BGMVolume => _bgmVolume;
    public float SFXVolume => _sfxVolume;
    public float BGMLength { get; private set; } = 10f;
    public int AudioSourceCount { get; private set; }
    public bool IsStopingToCreate { get; private set; }

    #endregion

    #region Inspector Variables

    [SerializeField]
    [Header("最初に流すBGM")]
    private string _name = "";

    [SerializeField]
    [Header("マスター音量")]
    [Range(0, 1)]
    private float _masterVolume = 1f;

    [SerializeField]
    [Header("音楽の音量")]
    [Range(0, 1)]
    private float _bgmVolume = 1f;

    [SerializeField]
    [Header("効果音の音量")]
    [Range(0, 1)]
    private float _sfxVolume = 1f;

    [SerializeField]
    [Header("音が消えるまでの時間")]
    float _fadeTime = 2f;

    [SerializeField]
    [Header("音楽を格納するオブジェクト")]
    private GameObject _bGMParent = null;

    [SerializeField]
    [Header("効果音を格納するオブジェクト")]
    private GameObject _sFXParent = null;

    [SerializeField]
    [Header("オーディオソースがついているプレファブ")]
    private AudioSource _audioPrefab = null;

    [SerializeField]
    [Header("音楽のクリップ")]
    AudioClip[] _bgmClips = null;

    [SerializeField]
    [Header("効果音のクリップ")]
    AudioClip[] _sfxClips = null;

    [SerializeField]
    [Header("BGM用のオーディオソース")]
    private List<AudioSource> _bgmAudioSources = new();

    [SerializeField]
    [Header("SFX用のオーディオソース")]
    private List<AudioSource> _sfxAudioSources = new();

    #endregion

    #region Private Menber

    private int _newAudioSourceCount = 0;
    private bool _isPausing = false;

    #endregion

    #region Unity Method

    protected override void Awake()
    {
        base.Awake();

        if (_audioPrefab == null) CreateAudio();//オーディオのプレファブが無かったら作成
        if (_bGMParent == null) CreateBGMParent();//BGMを格納するオブジェクトが無かったら作成   
        if (_sFXParent == null) CreateSFXParent();//SFXを格納するオブジェクトが無かったら作成

        if (_audioPrefab.playOnAwake)_audioPrefab.playOnAwake = false;

        this
            .ObserveEveryValueChanged
                (soundManager => soundManager.MasterVolume)
            .Subscribe
                (volume => ReflectMasterVolume())
            .AddTo(this);

        this
            .ObserveEveryValueChanged
                (soundManager => soundManager.BGMVolume)
            .Subscribe
                (volume => ReflectBGMVolume())
            .AddTo(this);

        this
            .ObserveEveryValueChanged
                (soundManager => soundManager.SFXVolume)
            .Subscribe
                (volume => ReflectSFXVolume())
            .AddTo(this);

        //ポーズ用
        PauseManager.Instance.OnPause -= Pause;
        PauseManager.Instance.OnResume -= Resume;
        PauseManager.Instance.OnPause += Pause;
        PauseManager.Instance.OnResume += Resume;

        PlayBGM(_name);
    }

    //private void OnValidate()
    //{
    //    ReflectMasterVolume();
    //    ReflectBGMVolume();
    //    ReflectSFXVolume();
    //}

    //private void OnDestroy()
    //{
    //    if (PauseManager.Instance.IsDontDestroy)
    //    {
    //        PauseManager.Instance.OnPause -= Pause;
    //        PauseManager.Instance.OnResume -= Resume;
    //    }
    //}

    #endregion

    #region Public Methods

    /// <summary>
    /// 音楽(BGM)を再生する関数
    /// </summary>
    /// <param name="name">音楽(BGM)の名前</param>
    /// <param name="isLooping"></param>
    /// <param name="volume">音の大きさ</param>
    public void PlayBGM(string name, bool isLooping = true, float volume = 1)
    {
        var bgmVolume = volume * _masterVolume * _bgmVolume;

        //BGMを止める
        foreach (var audioSource in _bgmAudioSources)
        {
            if(audioSource.isPlaying)
            {
                audioSource.Stop();
                audioSource.name = audioSource.clip.name;
                break;
            }
        }

        if (name == "") return;

        //再生したい音を格納しているオブジェクトから絞り込む
        foreach (var audioSource in _bgmAudioSources)
        {
            if (audioSource.clip.name == name)
            {
                audioSource.name = $"♪ {audioSource.name}";
                audioSource.volume = bgmVolume;
                audioSource.loop = isLooping;

                audioSource.Play();

                return;
            }
        }

        //再生したい音を格納しているオブジェクトが無かったら
        //再生したい音を絞り込む
        foreach (var clip in _bgmClips)
        {
            if (clip.name == name)
            {
                //再生したい音をのAudioを生成
                var newAudioSource = Instantiate(_audioPrefab);
                newAudioSource.transform.SetParent(_bGMParent.transform);
                _bgmAudioSources.Add(newAudioSource);

                newAudioSource.volume = bgmVolume;
                newAudioSource.clip = clip;
                newAudioSource.name = $"New {clip.name}";
                newAudioSource.loop = isLooping;

                newAudioSource.Play();

                return;
            }
        }
        Debug.LogWarning("音楽が見つからなかった");
    }

    /// <summary>
    /// 効果音(SFX)を再生する関数
    /// </summary>
    /// <param name="name">効果音(SFX)の名前</param>
    /// <param name="volume">音の大きさ</param>
    async public void PlaySFX(string name, float volume = 1)
    {
        var sfxVolume = volume * _masterVolume * _sfxVolume;

        //再生したい音を絞り込む
        foreach (var clip in _sfxClips)
        {
            if (clip.name == name)
            {
                //ClipがnullのAudioSourceを探す
                foreach (var audioSource in _sfxAudioSources)
                {
                    if (audioSource.clip == null)
                    {
                        audioSource.clip = clip;
                        audioSource.volume = sfxVolume;

                        var privName = audioSource.name;
                        audioSource.name = clip.name;
                        audioSource.Play();

                        await UniTask.WaitUntil(() => !audioSource.isPlaying && !_isPausing);

                        audioSource.name = privName;
                        audioSource.clip = null;
                        return;
                    }
                }

                //無かったら新しく作る
                var newAudioSource = Instantiate(_audioPrefab);
                newAudioSource.transform.SetParent(_sFXParent.transform);

                newAudioSource.name = $"NewSFX {_newAudioSourceCount}";
                _newAudioSourceCount++;
                _sfxAudioSources.Add(newAudioSource);

                newAudioSource.clip = clip;
                newAudioSource.volume = sfxVolume;

                var newPrivName = newAudioSource.name;
                newAudioSource.name = clip.name;

                newAudioSource.Play();

                await UniTask.WaitUntil(() => !newAudioSource.isPlaying && !_isPausing);

                newAudioSource.name = newPrivName;
                newAudioSource.clip = null;
                return;
            }
        }
        Debug.LogWarning("効果音が見つからなかった");
    }

    /// <summary>
    /// BGMをフェードする関数
    /// </summary>
    public async UniTask FadeBGM()
    {
        //BGMの音量を少しずつ下げる
        var audioSource = _bgmAudioSources.FirstOrDefault(x => x.isPlaying);

        await audioSource.DOFade(0, _fadeTime).AsyncWaitForCompletion();

        audioSource.DOKill();

        //BGMを止める
        audioSource.Stop();
        audioSource.name = audioSource.clip.name;
        audioSource.volume = 1;
    }

    /// <summary>
    /// マスター音量を変更する関数
    /// </summary>
    /// <param name="masterVolume">マスター音量</param>
    public void SetMasterVolume(float masterVolume)
    {
        _masterVolume = masterVolume;
    }

    /// <summary>
    /// 音楽の音量を変更する関数
    /// </summary>
    /// <param name="bgmVolume">音楽の音量</param>
    public void SetBGMVolume(float bgmVolume)
    {
        _bgmVolume = bgmVolume;
    }

    /// <summary>
    /// 効果音の音量を変更する関数
    /// </summary>
    /// <param name="sfxVolume">効果音の音量</param>
    public void SetSFXVolume(float sfxVolume)
    {
        _sfxVolume = sfxVolume;
    }

    /// <summary>
    /// 再生している全ての音の音量の変更を反映する関数
    /// </summary>
    public void ReflectMasterVolume()
    {
        ReflectBGMVolume();
        ReflectSFXVolume();
    }

    /// <summary>
    /// 再生している音楽の音量を反映する関数
    /// </summary>
    public void ReflectBGMVolume()
    {
        var volume = _masterVolume * _bgmVolume;
        _bgmAudioSources
            .FirstOrDefault(_ => _.isPlaying)
            .volume = volume;
    }

    /// <summary>
    /// 再生している全ての効果音の音量を変更を反映する関数
    /// </summary>
    public void ReflectSFXVolume()
    {
        var volume = _masterVolume * _sfxVolume;
        _sfxAudioSources
            .ForEach(_ => { if (_.isPlaying) _.volume = volume; });
    }

    #endregion

    #region Inspector Methods

    /// <summary>
    /// 生成するSFX用Audioの数を変更する関数
    /// </summary>
    /// <param name="count">生成するAudioの数</param>
    public void SetAudioCount(int count)
    {
        AudioSourceCount = count;
    }

    /// <summary>
    /// BGM用のPrefabを生成する関数
    /// </summary>
    public void CreateBGM()
    {
        if (_audioPrefab == null)CreateAudio();
        if(_bGMParent == null)CreateBGMParent();

        IsStopingToCreate = false;

        _bgmAudioSources.Clear();
        InitBGM();

        for (var i = 0; i < _bgmClips.Length; i++)
        {
            var audio = Instantiate(_audioPrefab);
            audio.transform.SetParent(_bGMParent.transform);
            _bgmAudioSources.Add(audio);

            audio.name = _bgmClips[i].name;
            audio.clip = _bgmClips[i];
            audio.loop = true;
        }

        _bgmAudioSources = new(_bgmAudioSources.Distinct());
    }
    
    /// <summary>
    /// SFX用のPrefabを生成する関数
    /// </summary>
    public void CreateSFX()
    {
        if (_audioPrefab == null)CreateAudio();
        if(_sFXParent == null)CreateSFXParent();

        IsStopingToCreate = false;

        _sfxAudioSources.Clear();

        InitSFX();

        for (var i = 0; i < AudioSourceCount; i++)
        {
            var audio = Instantiate(_audioPrefab);
            audio.transform.SetParent(_sFXParent.transform);

            audio.loop = false;
            audio.name = $"SFX {i.ToString("D3")}";

            _sfxAudioSources.Add(audio);
        }
    }

    /// <summary>
    /// BGM&SFX用のPrefabを全削除する関数
    /// </summary>
    public void Init()
    {
        IsStopingToCreate = true;

        _bgmAudioSources.Clear();
        _sfxAudioSources.Clear();

        InitBGM();
        InitSFX();
    }

    #endregion

    #region Editor Methods

    public void SetAudioLength(float length) =>
        BGMLength = length;

    public void ResizeBGMClips(int length) =>
        Array.Resize(ref _bgmClips, length);

    public void ResizeSFXClips(int length) =>
        Array.Resize(ref _sfxClips, length);

    public void AddBGMClip(int index, AudioClip clip) =>
        _bgmClips[index] = clip;

    public void AddSFXClip(int index, AudioClip clip) =>
        _sfxClips[index] = clip;

    #endregion

    #region Private Methods

    /// <summary>
    /// 音楽を格納するオブジェクトを生成する関数
    /// </summary>
    private void CreateBGMParent()
    {
        _bGMParent = new();
        _bGMParent.name = "BGM";
        _bGMParent.transform.SetParent(transform);
    }

    /// <summary>
    /// 効果音を格納するオブジェクトを生成する関数
    /// </summary>
    private void CreateSFXParent()
    {
        _sFXParent = new();
        _sFXParent.name = "SFX";
        _sFXParent.transform.SetParent(transform);
    }

    /// <summary>
    /// オーディオソースが付きオブジェクトを生成する関数
    /// </summary>
    private void CreateAudio()
    {
        GameObject gameObj = new();
        gameObj.transform.SetParent(transform);
        _audioPrefab = gameObj.AddComponent<AudioSource>();
        _audioPrefab.playOnAwake = false;
        _audioPrefab.name = "XD";
    }

    /// <summary>
    /// BGM用のPrefabを全削除する関数
    /// </summary>
    private void InitBGM()
    {
        if (_bGMParent == null) return;
        var parent = _bGMParent.transform;

        while (true)
        {
            if (parent.childCount == 0) break;
            DestroyImmediate(parent.GetChild(0).gameObject);
        }
    }

    /// <summary>
    /// SFX用のPrefabを全削除する関数
    /// </summary>
    private void InitSFX()
    {
        if (_sFXParent == null) return;
        var parent = _sFXParent.transform;

        while (true)
        {
            if (parent.childCount == 0) break;
            DestroyImmediate(parent.GetChild(0).gameObject);
        }
    }

    /// <summary>
    /// ポーズ用の関数
    /// </summary>
    private void Pause()
    {
        _isPausing = true;
        _bgmAudioSources.FirstOrDefault(x => x.isPlaying).Pause();
        _sfxAudioSources.ForEach(x => { if (x.isPlaying) x.Pause(); });
    }

    /// <summary>
    /// ポーズ解除用の関数
    /// </summary>
    private void Resume()
    {
        _isPausing = false;
        _bgmAudioSources.ForEach(x => x.UnPause());
        _sfxAudioSources.ForEach(x => x.UnPause());
    }

    #endregion
}
