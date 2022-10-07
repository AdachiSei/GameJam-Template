using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// �V�[����ǂݍ��݂������ɕK�v
/// </summary>
public class SceneLoader : SingletonMonoBehaviour<SceneLoader>
{
    public void LoadScene(string name)
    {
        SceneManager.LoadSceneAsync(name);
    }

    public void LoadScene(SceneType type)
    {
        SceneManager.LoadSceneAsync(type.ToString());
    }
}
