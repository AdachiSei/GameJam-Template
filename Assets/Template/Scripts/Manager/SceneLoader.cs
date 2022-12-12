using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
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
    #region Public Property

    public string[] SceneNames => _sceneNames;

    #endregion

    #region Inspecter Member

    [SerializeField]
    [Header("ローディング時にくるくるする絵")]
    Image _loadingImage;

    [SerializeField]
    [Header("ローディング時に表示するパネル")]
    Image _loadingPanel;

    [SerializeField]
    [Header("フェードの種類")]
    bool _isSlide;

    [SerializeField]
    [Header("フェードするまでの時間")]
    private float _fadeTime = 1f;

    [SerializeField]
    [Header("くるくるする絵の回転速度")]
    private float _loadingImageSpeed = 1f;

    [SerializeField]
    [Header("全てのシーンの名前")]
    private string[] _sceneNames;

    #endregion

    #region Private Member

    private  Vector3 _rotDir = new(0f,0f,-360);

    #endregion

    #region Const Member

    private const int OFFSET = 1;
    private const int LOOP = -1;
    private const float MAX_ALPFA = 1f;
    private const float FADE_POS = 800;

    #endregion

    #region Unity Method

    protected override void Awake()
    {
        base.Awake();
        FadeIn(_isSlide);
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
        if (_loadingPanel) await FadeOut(_isSlide);
        _loadingImage?.gameObject.SetActive(true);
        _loadingImage?
            .transform
                .DORotate(_rotDir, _loadingImageSpeed, RotateMode.FastBeyond360)
                .SetEase(Ease.Linear)
                .SetLoops(LOOP);
        await SceneManager.LoadSceneAsync(name);
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

    #endregion

    #region Editor Method

    public void ResizeSceneNames(int length)
    {
        Array.Resize(ref _sceneNames, length);
    }

    public void AddSceneName(int index, string name)
    {
        _sceneNames[index] = name;
    }

    #endregion

    #region Private Mehod

    /// <summary>
    /// 読み込みたいScene
    /// </summary>
    /// <param name="_isNext">次のSceneか</param>
    private void WantToLoadScene(bool _isNext)
    {
        var offset = -1;
        if (_isNext) offset = 1;
        var name = SceneManager.GetActiveScene().name;
        for (int i = 0; i < _sceneNames.Length; i++)
        {
            if (_sceneNames[i] == name)
            {
                LoadScene(_sceneNames[i + offset]);
                return;
            }
        }
    }

    private void FadeIn(bool isSlide)
    {
        switch (isSlide)
        {
            case false:
                _loadingPanel?.DOFade(0f, _fadeTime);
                break;
            case true:
                _loadingPanel?.rectTransform.DOLocalMoveX(-FADE_POS, _fadeTime);
                break;
        }
    }

    async private UniTask FadeOut(bool isSlide)
    {
        switch (isSlide)
        {
            case false:
                _loadingPanel.DOFade(MAX_ALPFA, _fadeTime);
                break;
            case true:
                _loadingPanel.rectTransform.DOLocalMoveX(0f, _fadeTime);
                break;
        }
        await UniTask.Delay(TimeSpan.FromSeconds(_fadeTime));
    }

    #endregion
}