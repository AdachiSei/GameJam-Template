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
/// シーンを読み込むために必要なScript
/// </summary>
public class SceneLoader : SingletonMonoBehaviour<SceneLoader>
{
    #region Public Property

    public bool IsGetSceneName => _isGetSceneName;

    #endregion

    #region Inspecter Member

    [SerializeField]
    [Header("全てのシーンの名前")]
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
    /// Sceneを読み込む関数
    /// </summary>
    /// <param name="name">Sceneの名前</param>
    public void LoadScene(string name)
    {
        SceneManager.LoadSceneAsync(name);
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

    #region Inspector Method

    /// <summary>
    /// Assetフォルダの中にあるSceneの名前を全てとってくる関数
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
        //BuildSetingsに入っているSceneの名前を全てとってくる
        foreach (var scene in EditorBuildSettings.scenes)
        {
            var name = Path.GetFileNameWithoutExtension(scene.path);
            sceneNames.Add(name);
        }
        //重複している要素を消してから並び替え
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
    /// BuildSettingsとSceneLoaderのSceneが違ったら呼びなおす関数
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
    /// 読み込みたいScene
    /// </summary>
    /// <param name="_isNext">次のSceneか</param>
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