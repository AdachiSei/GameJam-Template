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
    public float BGMLength => _bgmLength;
    public int AudioSourceNumber => _audioSourceNumber;
    public bool IsStopToCreate => _isStopingToCreate;

    #endregion

    #region Inspector Menber

    [SerializeField]
    [Header("最初に流すBGM")]
    private string _name;

    [SerializeField]
    [Header("マスター音量")]
    [Range(0, 1)]
    private float _masterVolume;

    [SerializeField]
    [Header("音楽の音量")]
    [Range(0, 1)]
    private float _bgmVolume;

    [SerializeField]
    [Header("効果音の音量")]
    [Range(0, 1)]
    private float _sfxVolume;

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
    AudioClip[] _bgmClips;

    [SerializeField]
    [Header("効果音のクリップ")]
    AudioClip[] _sfxClips;

    [SerializeField]
    [Header("BGM用のオーディオソース")]
    private List<AudioSource> _bgmAudioSources = new();

    [SerializeField]
    [Header("SFX用のオーディオソース")]
    private List<AudioSource> _sfxAudioSources = new();

    #endregion

    #region Private Menber

    private float _bgmLength = 10f;
    private int _audioSourceNumber;
    private int _newAudioSourceNumber;
    private bool _isStopingToCreate;
    private bool _isPausing;

    #endregion

    #region Unity Method

    protected override void Awake()
    {
        base.Awake();

        //オーディオのプレファブが無かったら
        if (_audioPrefab == null) CreateAudio();

        //BGMを格納するオブジェクトが無かったら
        if (_bGMParent == null) CreateBGMParent();

        //SFXを格納するオブジェクトが無かったら
        if (_sFXParent == null) CreateSFXParent();

        if(_audioPrefab.playOnAwake)_audioPrefab.playOnAwake = false;

        var soundManager = this;

        soundManager
            .ObserveEveryValueChanged
                (soundManager => soundManager.MasterVolume)
            .Subscribe
                (volume => ReflectMasterVolume());

        soundManager
            .ObserveEveryValueChanged
                (soundManager => soundManager.BGMVolume)
            .Subscribe
                (volume => ReflectBGMVolume());

        soundManager
            .ObserveEveryValueChanged
                (soundManager => soundManager.SFXVolume)
            .Subscribe
                (volume => ReflectSFXVolume());

        //ポーズ用
        PauseManager.Instance.OnPause += Pause;
        PauseManager.Instance.OnResume += Resume;

        PlayBGM(_name);
        PlaySFX(SFXNames.ONOFFELECTRICITY);
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
    public void PlayBGM(string name,bool isLooping = true,float volume = 1)
    {
        var bgmVolume = volume * _masterVolume * _bgmVolume;

        //BGMを止める
        foreach (var audioSource in _bgmAudioSources)
        {
            audioSource.Stop();
            audioSource.name = audioSource.clip.name;
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

                        var isPlaying = !audioSource.isPlaying && !_isPausing;
                        await UniTask.WaitUntil(() => isPlaying);

                        audioSource.name = privName;
                        audioSource.clip = null;
                        return;
                    }
                }

                //無かったら新しく作る
                var newAudioSource = Instantiate(_audioPrefab);
                newAudioSource.transform.SetParent(_sFXParent.transform);

                newAudioSource.name = $"NewSFX {_newAudioSourceNumber}";
                _newAudioSourceNumber++;
                _sfxAudioSources.Add(newAudioSource);

                newAudioSource.clip = clip;
                newAudioSource.volume = sfxVolume;

                var newPrivName = newAudioSource.name;
                newAudioSource.name = clip.name;

                newAudioSource.Play();

                var isPlayingNew = !newAudioSource.isPlaying && !_isPausing;
                await UniTask.WaitUntil(() => isPlayingNew);

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
    async public UniTask FadeBGM()
    {
        //BGMの音量を少しずつ下げる
        foreach (var audio in _bgmAudioSources)
        {
            //audio.Stop();
            if (audio.isPlaying) audio.DOFade(0, _fadeTime);
        }
        //await UniTask.NextFrame();
        await UniTaskForFloat.Delay(_fadeTime);

        //BGMを止める
        foreach (var audio in _bgmAudioSources)
        {
            if (audio.isPlaying)
            {
                audio.Stop();
                audio.name = audio.clip.name;
                audio.volume = 1;
            }
        }
    }

    /// <summary>
    /// マスター音量を変更する関数
    /// </summary>
    /// <param name="masterVolume">マスター音量</param>
    public void ChangeMasterVolume(float masterVolume)
    {
        _masterVolume = masterVolume;
    }

    /// <summary>
    /// 音楽の音量を変更する関数
    /// </summary>
    /// <param name="bgmVolume">音楽の音量</param>
    public void ChangeBGMVolume(float bgmVolume)
    {
        _bgmVolume = bgmVolume;
    }

    /// <summary>
    /// 効果音の音量を変更する関数
    /// </summary>
    /// <param name="sfxVolume">効果音の音量</param>
    public void ChangeSFXVolume(float sfxVolume)
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
    /// 再生している全ての音楽の音量を反映する関数
    /// </summary>
    public void ReflectBGMVolume()
    {
        foreach (var audioSource in _bgmAudioSources)
        {
            if(audioSource.isPlaying)
            {
                audioSource.volume = _masterVolume * _bgmVolume; 
            }
        }
    }

    /// <summary>
    /// 再生している全ての効果音の音量を変更を反映する関数
    /// </summary>
    public void ReflectSFXVolume()
    {
        foreach (var audioSource in _sfxAudioSources)
        {
            if (audioSource.isPlaying)
            {
                audioSource.volume = _masterVolume * _sfxVolume;
            }
        }
    }

    #region Inspector Methods

    /// <summary>
    /// 生成するSFX用Audioの数を変更する関数
    /// </summary>
    /// <param name="number">生成するAudioの数</param>
    public void ChangeAudioCount(int number)
    {
        _audioSourceNumber = number;
    }

    /// <summary>
    /// BGM用のPrefabを生成する関数
    /// </summary>
    public void CreateBGM()
    {
        if (_audioPrefab == null)CreateAudio();
        if(_bGMParent == null)CreateBGMParent();

        _isStopingToCreate = false;

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

        _isStopingToCreate = false;

        _sfxAudioSources.Clear();

        InitSFX();

        for (var i = 0; i < _audioSourceNumber; i++)
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
        _isStopingToCreate = true;

        _bgmAudioSources.Clear();
        _sfxAudioSources.Clear();

        InitBGM();
        InitSFX();
    }

    #endregion

    #region Editor Methods

    public void ChangeAudioLength(float length)
    {
        _bgmLength = length;
    }

    public void ResizeBGMClips(int length)
    {
        Array.Resize(ref _bgmClips, length);
    }

    public void ResizeSFXClips(int length)
    {
        Array.Resize(ref _sfxClips, length);
    }

    public void AddBGMClip(int index, AudioClip clip)
    {
        _bgmClips[index] = clip;
    }

    public void AddSFXClip(int index, AudioClip clip)
    {
        _sfxClips[index] = clip;
    }

    #endregion

    #endregion

    #region Private Methods

    /// <summary>
    /// 音楽を格納するオブジェクトを生成する関数
    /// </summary>
    private void CreateBGMParent()
    {
        _bGMParent = new();
        _bGMParent.name = "BGM";
        _bGMParent.transform.parent = transform;
    }

    /// <summary>
    /// 効果音を格納するオブジェクトを生成する関数
    /// </summary>
    private void CreateSFXParent()
    {
        _sFXParent = new();
        _sFXParent.name = "SFX";
        _sFXParent.transform.parent = transform;
    }

    /// <summary>
    /// オーディオソースが付きオブジェクトを生成する関数
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
    /// BGM用のPrefabを全削除する関数
    /// </summary>
    private void InitBGM()
    {
        if (_bGMParent == null) return;
        while (true)
        {
            var children = _bGMParent.transform;
            var empty = children.childCount == 0;
            if (empty) break;
            var DestroyGO = children.GetChild(0).gameObject;
            DestroyImmediate(DestroyGO);
        }
    }

    /// <summary>
    /// SFX用のPrefabを全削除する関数
    /// </summary>
    private void InitSFX()
    {
        if (_sFXParent == null) return;
        while (true)
        {
            var children = _sFXParent.transform;
            var empty = children.childCount == 0;
            if (empty) break;
            var DestroyGO = children.GetChild(0).gameObject;
            DestroyImmediate(DestroyGO);
        }
    }

    /// <summary>
    /// ポーズ用の関数
    /// </summary>
    private void Pause()
    {
        _isPausing = true;
        foreach (var bGMAudio in _bgmAudioSources)
        {
            if (bGMAudio.isPlaying) bGMAudio.Pause();
        }
        foreach (var sFXAudio in _sfxAudioSources)
        {
            if (sFXAudio.isPlaying)sFXAudio.Pause();
        }
    }

    /// <summary>
    /// ポーズ解除用の関数
    /// </summary>
    private void Resume()
    {
        _isPausing = false;
        foreach (var bgm in _bgmAudioSources) bgm.UnPause();
        foreach (var sfx in _sfxAudioSources) sfx.UnPause();
    }

    #endregion
}
