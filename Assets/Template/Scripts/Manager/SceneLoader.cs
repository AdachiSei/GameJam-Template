using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// シーンを読み込みたい時に必要なScript
/// </summary>
public class SceneLoader : SingletonMonoBehaviour<SceneLoader>
{
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
}
