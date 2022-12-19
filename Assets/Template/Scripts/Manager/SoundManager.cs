using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Cysharp.Threading.Tasks;
using DG.Tweening;

/// <summary>
/// サウンドを管理するScript
/// </summary>
public class SoundManager : SingletonMonoBehaviour<SoundManager>
{
    #region Public Property

    public float BGMLength => _bgmLength;
    public int AudioCount => _audioCount;
    public bool IsStopCreate => _isStopCreate;

    #endregion

    #region Inspector Menber

    [SerializeField]
    [Header("最初に流すBGM")]
    private string _name;

    [SerializeField]
    [Header("マスター音量")]
    [Range(0,1)]
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
    private List<AudioSource> _bGMAudioSources = new();

    [SerializeField]
    [Header("SFX用のオーディオソース")]
    private List<AudioSource> _sFXAudioSources = new();

    #endregion

    #region Private Menber

    private float _bgmLength = 10f;
    private int _audioCount;
    private int _newAudioSourceNum;
    private bool _isStopCreate;
    private bool _isPausing;

    #endregion

    #region Const Member

    private const int OFFSET = 1;

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
        _audioPrefab.playOnAwake = false;
        //ポーズ用
        PauseManager.Instance.OnPause += Pause;
        PauseManager.Instance.OnResume += Resume;
        PlayBGM(_name);
        PlaySFX(BGMNames.GAMEOVER);
    }

    private void OnDisable()
    {
        if (PauseManager.Instance.IsDontDestroy)
        {
            PauseManager.Instance.OnPause -= Pause;
            PauseManager.Instance.OnResume -= Resume;
        }
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// 音楽(BGM)を再生する関数
    /// </summary>
    /// <param name="name">Dataに設定した音楽(BGM)の名前</param>
    /// <param name="volume">音の大きさ</param>
    public void PlayBGM(string name,float volume = 1)
    {
        var bgmVolume = volume * _masterVolume * _bgmVolume;
        //BGMを止める
        foreach (var audio in _bGMAudioSources)
        {
            audio.Stop();
            audio.name = audio.clip.name;
        }
        if (name == "") return;
        //再生したい音を格納しているオブジェクトから絞り込む
        foreach (var audio in _bGMAudioSources)
        {
            var audioName = audio.name == name;
            var clipName = audio.clip.name == name;
            if (audioName || clipName)
            {
                audio.name = $"♪ {audio.name}";
                audio.volume = bgmVolume;
                audio.Play();
                return;
            }
        }
        //再生したい音を格納しているオブジェクトが無かったら
        //再生したい音をDataから絞り込む
        foreach (var clip in _bgmClips/*_bGMData.BGMs*/)
        {
            //var bGMName = bgm.Name == name;
            var clipName = clip.name == name;
            if (clipName)
            {
                //再生したい音をのAudioを生成
                var newAudio = Instantiate(_audioPrefab);
                newAudio.transform.SetParent(_bGMParent.transform);
                _bGMAudioSources.Add(newAudio);
                newAudio.volume = bgmVolume;
                newAudio.clip = clip;
                newAudio.name = $"New {clip.name}";
                newAudio.loop = true;
                newAudio.Play();
                return;
            }
        }
        Debug.Log("BGMが見つからなかった");
    }

    /// <summary>
    /// 効果音(SFX)を再生する関数
    /// </summary>
    /// <param name="name">Dataに設定した効果音(SFX)の名前</param>
    /// <param name="volume">音の大きさ</param>
    async public void PlaySFX(string name, float volume = 1)
    {
        var sfxVolume = volume * _masterVolume * _sfxVolume;
        //再生したい音をDataからを絞り込む
        foreach (var clip in _sfxClips/*_sFXData.SFXes*/)
        {
            //var sFXName = sfx.Name == name;
            var clipName = clip.name == name;
            if (clipName)
            {
                //ClipがnullのAudioを探す
                foreach (var audio in _sFXAudioSources)
                {
                    if (audio.clip == null)
                    {
                        audio.clip = clip;
                        audio.volume = sfxVolume;
                        var privName = audio.name;
                        audio.name = clip.name;
                        audio.Play();
                        await UniTask.WaitUntil(() => !audio.isPlaying && !_isPausing);
                        audio.name = privName;
                        audio.clip = null;
                        return;
                    }
                }
                //無かったら新しく作る
                var audioSource = Instantiate(_audioPrefab);
                audioSource.transform.SetParent(_sFXParent.transform);
                audioSource.name = "NewSFX " + _newAudioSourceNum;
                _newAudioSourceNum++;
                _sFXAudioSources.Add(audioSource);
                var newAudio = _sFXAudioSources[_sFXAudioSources.Count - OFFSET];
                newAudio.clip = clip;
                newAudio.volume = sfxVolume;
                var newPrivName = newAudio.name;
                newAudio.name = clip.name;
                newAudio.Play();
                await UniTask.WaitUntil(() => !newAudio.isPlaying && !_isPausing);
                newAudio.name = newPrivName;
                newAudio.clip = null;
                return;
            }
        }
        Debug.Log("SFXが見つからなかった");
    }

    /// <summary>
    /// BGMをフェードする関数
    /// </summary>
    async public UniTask FadeBGM()
    {
        //BGMの音量を少しずつ下げる
        foreach (var audio in _bGMAudioSources)
        {
            //audio.Stop();
            if (audio.isPlaying) audio.DOFade(0, _fadeTime);
        }
        //await UniTask.NextFrame();
        await UniTaskForFloat.Delay(_fadeTime);

        //BGMを止める
        foreach (var audio in _bGMAudioSources)
        {
            if (audio.isPlaying)
            {
                audio.Stop();
                audio.name = audio.clip.name;
                audio.volume = 1;
            }
        }
    }

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
        if (_audioPrefab == null)CreateAudio();
        if(_bGMParent == null)CreateBGMParent();
        _isStopCreate = false;
        _bGMAudioSources.Clear();
        InitBGM();
        for (var i = 0; i < _bgmClips.Length; i++)
        {
            var audio = Instantiate(_audioPrefab);
            audio.transform.SetParent(_bGMParent.transform);
            _bGMAudioSources.Add(audio);
            audio.name = _bgmClips[i].name;
            audio.clip = _bgmClips[i];
            audio.loop = true;
        }
        _bGMAudioSources = new(_bGMAudioSources.Distinct());
    }
    
    /// <summary>
    /// SFX用のPrefabを生成する関数
    /// </summary>
    public void CreateSFX()
    {
        if (_audioPrefab == null)CreateAudio();
        if(_sFXParent == null)CreateSFXParent();
        _isStopCreate = false;
        _sFXAudioSources.Clear();
        InitSFX();
        for (var i = 0; i < _audioCount; i++)
        {
            var audio = Instantiate(_audioPrefab);
            audio.transform.SetParent(_sFXParent.transform);
            audio.loop = false;
            if (i < 10) audio.name = "SFX " + "00" + i;
            else if (i < 100) audio.name = "SFX " + "0" + i;
            else audio.name = "SFX " + i;
            _sFXAudioSources.Add(audio);
        }
    }

    /// <summary>
    /// BGM&SFX用のPrefabを全削除する関数
    /// </summary>
    public void Init()
    {
        _isStopCreate = true;
        _bGMAudioSources.Clear();
        _sFXAudioSources.Clear();
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
        foreach (var bGMAudio in _bGMAudioSources)
        {
            if (bGMAudio.isPlaying) bGMAudio.Pause();
        }
        foreach (var sFXAudio in _sFXAudioSources)
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
        foreach (var bgm in _bGMAudioSources) bgm.UnPause();
        foreach (var sfx in _sFXAudioSources) sfx.UnPause();
    }

    #endregion
}
