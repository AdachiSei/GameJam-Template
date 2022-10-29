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
    string[] _sceneNames;
    #endregion

    #region Private Member

    private bool _isGetSceneName = false;

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
    /// Scene��ǂݍ��ފ֐�
    /// </summary>
    /// <param name="name">Scene�̎��(enum)</param>
    public void LoadScene(SceneType type)
    {
        SceneManager.LoadSceneAsync(type.ToString());
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
    /// �O��Scene��ǂݍ��ފ֐�
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
    /// Asset�t�H���_�̒��ɂ���Scene�̖��O��S�ĂƂ��Ă���֐�
    /// </summary>
    public void GetSceneName()
    {
        List<string> sceneNames = new();
        //Scene�̖��O��S�ĂƂ��Ă���
        foreach (var guid in AssetDatabase.FindAssets("t:Scene"))
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            var name = AssetDatabase.LoadMainAssetAtPath(path).name;
            sceneNames.Add(name);
        }
        //���ёւ�
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