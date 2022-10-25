using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// サウンドを管理するScript
/// </summary>
public class SoundManager : SingletonMonoBehaviour<SoundManager>
{
    #region inspector menber

    [SerializeField]
    [Header("最初に流すBGM")]
    private BGMType _type;

    [SerializeField]
    [Header("音楽")]
    private BGMDatas[] _bGMDatas;

    [SerializeField]
    [Header("効果音")]
    private SFXData _sFXData;

    [SerializeField]
    [Header("効果音を格納するオブジェクト")]
    private GameObject _sFXAudioGameObject;

    [SerializeField]
    [Header("オーディオソースがついているプレファブ")]
    private AudioSource _audioSorcePrefab;

    [SerializeField]
    [Header("効果音を格納するオブジェクトの生成数")]
    private int _sFXAudioNum = 30;

    #endregion

    List<AudioSource> _sFXAudios = new();

    protected override void Awake()
    {
        base.Awake();
        CreateSFX();
        PlayBGM(_type);
    }

    #region
    /// <summary>
    /// 音楽(BGM)を再生する関数
    /// </summary>
    /// <param name="type">再生したい音楽(BGM)</param>
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
        Debug.Log("BGMを再生していません");
    }

    /// <summary>
    /// 音楽(BGM)を再生する関数
    /// </summary>
    /// <param name="type">再生したい音楽(BGM)</param>
    /// <param name="volume">音楽(BGM)のボリューム</param>
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
        Debug.Log("BGMを再生していません");
    }
    #endregion

    /// <summary>
    /// 効果音(SFX)を再生する関数
    /// </summary>
    /// <param name="type">再生したい効果音(SFX)</param>
    private void PlaySFX(SFXType type)
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

    private void PlaySFX(SFXType type,float volume)
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

    private void CreateSFX()
    {
        for (var i = 0; i < _sFXAudioNum; i++)
        {
             _sFXAudios.Add(Instantiate(_audioSorcePrefab,
                                new(),
                                Quaternion.identity,
                                _sFXAudioGameObject.transform));
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
