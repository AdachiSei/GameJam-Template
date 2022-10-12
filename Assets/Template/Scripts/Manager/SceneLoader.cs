using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// �V�[����ǂݍ��݂������ɕK�v��Script
/// </summary>
public class SceneLoader : SingletonMonoBehaviour<SceneLoader>
{
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
}
