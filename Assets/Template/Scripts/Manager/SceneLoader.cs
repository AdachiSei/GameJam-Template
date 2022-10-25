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
    #region Inspecter Member

    [SerializeField]
    List<string> _sceneNames = new();

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
        for (int i = 0; i < _sceneNames.Count; i++)
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
        for (int i = 0; i < _sceneNames.Count; i++)
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
    [ContextMenu("GetSceneName")]
    public void GetSceneName()
    {
        //Scene�̖��O��S�ĂƂ��Ă���
        foreach (var guid in AssetDatabase.FindAssets("t:Scene"))
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            var name = AssetDatabase.LoadMainAssetAtPath(path).name;
            _sceneNames.Add(name);
        }
        //���ёւ�
        _sceneNames = new(_sceneNames.Distinct());
        _sceneNames = new(_sceneNames.OrderBy(name =>
       {
           var sceneNum = name.Trim().Split("Scene");
           if (sceneNum[DM.ONE] == "")
           {
               sceneNum = new[] { sceneNum[DM.ZERO], "0" };
           }
           return int.Parse(sceneNum[DM.ONE]);
       }));
    }

    #endregion
}