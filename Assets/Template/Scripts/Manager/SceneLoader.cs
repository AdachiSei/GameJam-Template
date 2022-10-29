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
    string[] _sceneNames;
    #endregion

    #region Private Member

    private bool _isGetSceneName = false;

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
    /// Sceneを読み込む関数
    /// </summary>
    /// <param name="name">Sceneの種類(enum)</param>
    public void LoadScene(SceneType type)
    {
        SceneManager.LoadSceneAsync(type.ToString());
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
        var name = SceneManager.GetActiveScene().name;
        for (int i = 0; i < _sceneNames.Length; i++)
        {
            if (_sceneNames[i] == name)
            {
                i++;
                LoadScene(_sceneNames[i]);
                return;
            }
        }
    }

    /// <summary>
    /// 前のSceneを読み込む関数
    /// </summary>
    public void LoadPrevScene()
    {
        var name = SceneManager.GetActiveScene().name;
        for (int i = 0; i < _sceneNames.Length; i++)
        {
            if (_sceneNames[i] == name)
            {
                i--;
                LoadScene(_sceneNames[i]);
                return;
            }
        }
    }

    #endregion

    #region Inspector Methods

    /// <summary>
    /// Assetフォルダの中にあるSceneの名前を全てとってくる関数
    /// </summary>
    public void GetSceneName()
    {
        List<string> sceneNames = new();
        //Sceneの名前を全てとってくる
        foreach (var guid in AssetDatabase.FindAssets("t:Scene"))
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            var name = AssetDatabase.LoadMainAssetAtPath(path).name;
            sceneNames.Add(name);
        }
        //並び替え
        sceneNames = new(sceneNames.Distinct());
        sceneNames = new(sceneNames.OrderBy(name =>
       {
           var sceneNum = name.Trim().Split("Scene");
           if (sceneNum[DMInt.ONE] == "")
           {
               sceneNum = new[] {sceneNum[DMInt.ZERO], "0" };
           }
           return int.Parse(sceneNum[DMInt.ONE]);
       }));

        for (int i = 0; i < sceneNames.Count; i++)
        {
            _sceneNames[i] = sceneNames[i];
        }
    }

    #endregion
}