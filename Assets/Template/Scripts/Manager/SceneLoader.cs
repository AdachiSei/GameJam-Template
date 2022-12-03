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
/// �V�[����ǂݍ��ނ��߂ɕK�v��Script
/// </summary>
public class SceneLoader : SingletonMonoBehaviour<SceneLoader>
{
    #region Inspecter Member

    [SerializeField]
    [Header("���[�f�B���O���ɂ��邭�邷��G")]
    Image _loadingImage;

    [SerializeField]
    [Header("���[�f�B���O���ɕ\������p�l��")]
    Image _loadingPanel;

    [SerializeField]
    [Header("�t�F�[�h�̎��")]
    bool _isSlide;

    [SerializeField]
    [Header("�t�F�[�h����܂ł̎���")]
    private float _fadeTime = 1f;

    [SerializeField]
    [Header("���邭�邷��G�̉�]���x")]
    private float _loadingImageSpeed = 1f;

    [SerializeField]
    [Header("�S�ẴV�[���̖��O")]
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
        CheckScenesName();
        FadeIn(_isSlide);
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

    #endregion

    #region Inspector Method

    /// <summary>
    /// Asset�t�H���_�̒��ɂ���Scene�̖��O��S�ĂƂ��Ă���֐�
    /// </summary>
    public void GetSceneName()
    {
        var offset = 0;
        var isPlaying = EditorApplication.isPlaying;
        if (isPlaying == false)
        {
            offset = 1;
        }
        Array.Resize(ref _sceneNames, EditorBuildSettings.scenes.Length + offset);
        List<string> sceneNames = new();
        //BuildSetings�ɓ����Ă���Scene�̖��O��S�ĂƂ��Ă���
        foreach (var scene in EditorBuildSettings.scenes)
        {
            var name = Path.GetFileNameWithoutExtension(scene.path);
            sceneNames.Add(name);
        }
        //�d�����Ă���v�f�������Ă�����ёւ�
        sceneNames = new(sceneNames.Distinct());
        sceneNames = new(sceneNames.OrderBy(name =>
       {
           var sceneNum = name.Split("Scene");
           if (sceneNum[OFFSET] == "")
           {
               sceneNum = new[] { sceneNum[0], "0" };
           }
           return int.Parse(sceneNum[OFFSET]);
       }));

        for (int i = 0; i < sceneNames.Count; i++)
        {
            _sceneNames[i] = sceneNames[i];
        }
        if (isPlaying == false)
        {
            _sceneNames[_sceneNames.Length - offset] = "RemoveThis";
        }
    }

    #endregion

    #region Private Mehod

    /// <summary>
    /// BuildSettings��SceneLoader��Scene���������ĂтȂ����֐�
    /// </summary>
    private void CheckScenesName()
    {   
        if (_sceneNames.Length != EditorBuildSettings.scenes.Length)
        {
            GetSceneName();
        }
        else
        {
            foreach (var scene in EditorBuildSettings.scenes)
            {
                var sceneName = Path.GetFileNameWithoutExtension(scene.path).ToString();
                if (_sceneNames.Any(name => name == sceneName) == false)
                {
                    GetSceneName();
                    return;
                }
            }
        }
    }

    /// <summary>
    /// �ǂݍ��݂���Scene
    /// </summary>
    /// <param name="_isNext">����Scene��</param>
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

    private void FadeIn(bool a)
    {
        switch (a)
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