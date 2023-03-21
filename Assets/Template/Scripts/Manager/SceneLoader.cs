using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;

/// <summary>
/// シーンを読み込むために必要なScript
/// </summary>
public class SceneLoader : SingletonMonoBehaviour<SceneLoader>
{
    #region Properties

    public string ActiveScene => SceneManager.GetActiveScene().name;
    public string[] AllSceneName => _allSceneName;

    #endregion

    #region Inspecter Variables

    [SerializeField]
    [Header("ローディング時に回転する絵")]
    private Image _loadingImage = null;

    [SerializeField]
    [Header("ローディング時に表示するパネル")]
    private Image _loadingPanel = null;

    [SerializeField]
    [Header("フェードするまでの時間")]
    private float _fadeTime = 1f;

    [SerializeField]
    [Header("回転する絵の回転速度")]
    private float _loadingImageSpeed = 1f;

    [SerializeField]
    [Header("全てのシーンの名前")]
    private string[] _allSceneName = null;

    #endregion

    #region Member Variables

    private  Vector3 _rotDir = new Vector3(0f,0f,-360);

    #endregion

    #region Constants

    private const int LOOP_VALUE = -1;
    private const float MAX_ALPFA_VALUE = 1f;

    #endregion

    #region Unity Methods

    protected override void Awake()
    {
        base.Awake();
        FadeIn();
        _loadingImage?.gameObject.SetActive(false);
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Sceneを読み込む関数
    /// </summary>
    /// <param name="name">Sceneの名前</param>
    async public void LoadScene(string name)
    {
        if (_loadingPanel) await FadeOut();
        _loadingImage?.gameObject.SetActive(true);
        _loadingImage?
            .transform
                .DORotate(_rotDir, _loadingImageSpeed, RotateMode.FastBeyond360)
                .SetEase(Ease.Linear)
                .SetLoops(LOOP_VALUE);
        await SceneManager.LoadSceneAsync(name);
        _loadingImage?.gameObject.SetActive(false);
        _loadingImage?.DOKill();
    }

    /// <summary>
    /// 今のSceneをリロードするための関数
    /// </summary>
    public void ReloadScene()
    {
        var name = SceneManager.GetActiveScene().name;
        LoadScene(name);
    }

    /// <summary>
    /// 次のSceneを読み込む関数
    /// </summary>
    public void LoadNextScene()
    {
        WantToLoadScene(true);
    }

    /// <summary>
    /// 前のSceneを読み込む関数
    /// </summary>
    public void LoadPrevScene()
    {
        WantToLoadScene(false);
    }  

    #region Editor Methods

    public void ResizeSceneNames(int length)
    {
        Array.Resize(ref _allSceneName, length);
    }

    public void AddSceneName(int index, string name)
    {
        _allSceneName[index] = name;
    }

    #endregion

    #endregion

    #region Private Mehods

    /// <summary>
    /// 読み込みたいScene
    /// </summary>
    /// <param name="_isNext">次のSceneか</param>
    private void WantToLoadScene(bool _isNext)
    {
        var offset = -1;
        if (_isNext) offset = 1;
        var name = SceneManager.GetActiveScene().name;
        for (int i = 0; i < _allSceneName.Length; i++)
        {
            if (_allSceneName[i] == name)
            {
                LoadScene(_allSceneName[i + offset]);
                return;
            }
        }
    }

    private void FadeIn()
    {
        _loadingPanel?.DOFade(0f, _fadeTime);
    }

    async private UniTask FadeOut()
    {
        await _loadingPanel?.DOFade(MAX_ALPFA_VALUE, _fadeTime).AsyncWaitForCompletion();
    }

    #endregion
}