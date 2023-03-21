using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;

/// <summary>
/// �V�[����ǂݍ��ނ��߂ɕK�v��Script
/// </summary>
public class SceneLoader : SingletonMonoBehaviour<SceneLoader>
{
    #region Properties

    public string ActiveScene => SceneManager.GetActiveScene().name;
    public string[] AllSceneName => _allSceneName;

    #endregion

    #region Inspecter Variables

    [SerializeField]
    [Header("���[�f�B���O���ɉ�]����G")]
    private Image _loadingImage = null;

    [SerializeField]
    [Header("���[�f�B���O���ɕ\������p�l��")]
    private Image _loadingPanel = null;

    [SerializeField]
    [Header("�t�F�[�h����܂ł̎���")]
    private float _fadeTime = 1f;

    [SerializeField]
    [Header("��]����G�̉�]���x")]
    private float _loadingImageSpeed = 1f;

    [SerializeField]
    [Header("�S�ẴV�[���̖��O")]
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
    /// Scene��ǂݍ��ފ֐�
    /// </summary>
    /// <param name="name">Scene�̖��O</param>
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
    /// ����Scene�������[�h���邽�߂̊֐�
    /// </summary>
    public void ReloadScene()
    {
        var name = SceneManager.GetActiveScene().name;
        LoadScene(name);
    }

    /// <summary>
    /// ����Scene��ǂݍ��ފ֐�
    /// </summary>
    public void LoadNextScene()
    {
        WantToLoadScene(true);
    }

    /// <summary>
    /// �O��Scene��ǂݍ��ފ֐�
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
    /// �ǂݍ��݂���Scene
    /// </summary>
    /// <param name="_isNext">����Scene��</param>
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