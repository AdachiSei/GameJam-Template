using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using DisturbMagic;

/// <summary>
/// �V�[����ǂݍ��ނ��߂ɕK�v��Script
/// </summary>
public class SceneLoader : SingletonMonoBehaviour<SceneLoader>
{
    #region Public Property

    public bool IsGetSceneName => _isGetSceneName;

    #endregion

    #region Inspecter Member

    [SerializeField]
    [Header("�S�ẴV�[���̖��O")]
    private string[] _sceneNames;

    #endregion

    #region Private Member

    private bool _isGetSceneName = false;

    #endregion

    #region Unity Method

    protected override void Awake()
    {
        base.Awake();
        CheckScenesName();
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Scene��ǂݍ��ފ֐�
    /// </summary>
    /// <param name="name">Scene�̖��O</param>
    public void LoadScene(string name)
    {
        SceneManager.LoadSceneAsync(name);
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
           if (sceneNum[DMInt.ONE] == "")
           {
               sceneNum = new[] { sceneNum[DMInt.ZERO], "0" };
           }
           return int.Parse(sceneNum[DMInt.ONE]);
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
    void CheckScenesName()
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
    void WantToLoadScene(bool _isNext)
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

    #endregion
}