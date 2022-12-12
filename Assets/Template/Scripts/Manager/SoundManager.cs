using System;
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

    public int AudioCount => _audioCount;
    public bool IsStopCreate => _isStopCreate;

    #endregion

    #region Inspector Menber

    [SerializeField]
    [Header("最初に流すBGM")]
    private string _name;

    [SerializeField]
    [Header("音が消えるまでの時間")]
    float _fadeTime = 2f;

    [SerializeField]
    [Header("音楽")]
    private BGMData _bGMData = null;

    [SerializeField]
    [Header("効果音")]
    private SFXData _sFXData = null;

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
    [Header("BGM用のオーディオ")]
    private List<AudioSource> _bGMAudios = new();

    [SerializeField]
    [Header("SFX用のオーディオ")]
    private List<AudioSource> _sFXAudios = new();

    #endregion

    #region Private Menber

    private int _audioCount;
    private int _newAudioNum;
    private bool _isStopCreate;

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
    }

    private void OnDisable()
    {
        if (IsDontDestroy)
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
        //BGMを止める
        foreach (var audio in _bGMAudios)
        {
            audio.Stop();
        }
        //再生したい音を格納しているオブジェクトから絞り込む
        foreach (var audio in _bGMAudios)
        {
            if (audio.name == name)
            {
                audio.volume = volume;
                audio.Play();
                return;
            }
        }
        //再生したい音を格納しているオブジェクトが無かったら
        //再生したい音をDataから絞り込む
        foreach (var bGM in _bGMData.BGMs)
        {
            if (bGM.Name == name)
            {
                //再生したい音をのAudioを生成
                var newAudio = Instantiate(_audioPrefab);
                newAudio.transform.SetParent(_bGMParent.transform);
                _bGMAudios.Add(newAudio);
                newAudio.volume = volume;
                newAudio.clip = bGM.AudioClip;
                newAudio.name = bGM.Name;
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
        //再生したい音をDataからを絞り込む
        foreach (var sFX in _sFXData.SFXes)
        {
            if (sFX.Name == name)
            {
                //ClipがnullのAudioを探す
                foreach (var audio in _sFXAudios)
                {
                    if (audio.clip == null)
                    {
                        audio.clip = sFX.AudioClip;
                        audio.volume = volume;
                        var privName = audio.name;
                        audio.name = sFX.Name;
                        audio.Play();
                        await PauseManager.Instance.UniTaskForPause(sFX.AudioClip.length);
                        audio.name = privName;
                        audio.clip = null;
                        return;
                    }
                }
                //無かったら新しく作る
                var gameObject = Instantiate(_audioPrefab);
                gameObject.transform.SetParent(_sFXParent.transform);
                gameObject.name = "NewSFX " + _newAudioNum;
                _newAudioNum++;
                _sFXAudios.Add(gameObject);
                var newAudio = _sFXAudios[_sFXAudios.Count - OFFSET];
                newAudio.clip = sFX.AudioClip;
                newAudio.volume = volume;
                var newPrivName = newAudio.name;
                newAudio.name = sFX.Name;
                newAudio.Play();
                await PauseManager.Instance.UniTaskForPause(sFX.AudioClip.length);
                newAudio.name = newPrivName;
                newAudio.clip = null;
                return;
            }
        }
        Debug.Log("SFXが見つからなかった");
    }

    /// <summary>
    /// BGMを止める関数
    /// </summary>
    async public UniTask FadeBGM()
    {
        //BGMの音量を少しずつ下げる
        foreach (var audio in _bGMAudios)
        {
            //audio.Stop();
            if (!audio.isPlaying) audio.DOFade(0, _fadeTime);
        }
        //await UniTask.NextFrame();

        await UniTaskForFloat.Delay(_fadeTime);

        //BGMを止める
        foreach (var audio in _bGMAudios)
        {
            if (!audio.isPlaying)
            {
                audio.Stop();
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
        _bGMAudios.Clear();
        InitBGM();
        for (var i = 0; i < _bGMData.BGMs.Length; i++)
        {
            var audio = Instantiate(_audioPrefab);
            audio.transform.SetParent(_bGMParent.transform);
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
        if (_audioPrefab == null)CreateAudio();
        if(_sFXParent == null)CreateSFXParent();
        _isStopCreate = false;
        _sFXAudios.Clear();
        InitSFX();
        for (var i = 0; i < _audioCount; i++)
        {
            var audio = Instantiate(_audioPrefab);
            audio.transform.SetParent(_sFXParent.transform);
            audio.loop = true;
            if (i < 10) audio.name = "SFX " + "00" + i;
            else if (i < 100) audio.name = "SFX " + "0" + i;
            else audio.name = "SFX " + i;
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
        foreach (var bGMAudio in _bGMAudios)
        {
            if (bGMAudio.isPlaying) bGMAudio.Pause();
        }
        foreach (var sFXAudio in _sFXAudios)
        {
            if (sFXAudio.isPlaying)sFXAudio.Pause();
        }
    }

    /// <summary>
    /// ポーズ解除用の関数
    /// </summary>
    private void Resume()
    {
        foreach (var bGMAudio in _bGMAudios) bGMAudio.UnPause();
        foreach (var sFXAudio in _sFXAudios) sFXAudio.UnPause();
    }

    #endregion
}
