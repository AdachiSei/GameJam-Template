using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// シーンを読み込みたい時に必要
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
