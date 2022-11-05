using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ステージセレクト画面を設定してくれるScript
/// </summary>
public class StageSelectSettings : MonoBehaviour
{
    [SerializeField]
    [Header("設定したい奴")]
    GameObject[] _gameObject;

    [SerializeField]
    [Header("Context")]
    HorizontalLayoutGroup _hGroup;

    [SerializeField]
    [Header("設定したい奴を格納するゲームオブジェクト")]
    VerticalLayoutGroup _vGroup;

    public void Setting(int count)
    {
        for (int i = 0; i < count; i++)
        {

        }
        foreach (Transform i in _vGroup.transform)
        {
            _gameObject[0].transform.parent = i.parent;
        }
    }
}
