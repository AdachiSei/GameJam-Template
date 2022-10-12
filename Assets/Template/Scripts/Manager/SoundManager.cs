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
    [SerializeField]
    [Header("最初に流すBGM")]
    BGMType _type;

    [SerializeField]
    [Header("音楽")]
    private BGMData[] _bGMDatas;

    [SerializeField]
    [Header("効果音")]
    private SFXData[] _sFXDatas;

    [SerializeField]
    [Header("効果音を格納するオブジェクト")]
    GameObject _pool;

    protected override void Awake()
    {
        base.Awake();
        CreatePool();
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
        foreach (var sFX in _sFXDatas)
        {
            if(sFX.Type == type)
            {
                //sFX.AudioClip
                return;
            }
        }
    }

    private void PlaySFX(SFXType type,float volume)
    {
        foreach (var sFX in _sFXDatas)
        {
            if (sFX.Type == type)
            {
                //sFX.AudioClip
                return;
            }
        }
    }


    private void CreatePool()
    {
        Instantiate(gameObject.AddComponent<AudioSource>().clip = _sFXDatas[0].AudioClip, new(), Quaternion.identity,_pool.transform);
    }

    private void Pause()
    {
         
    }

    private void Restart()
    {

    }


    [Serializable]
    public class BGMData
    {
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
