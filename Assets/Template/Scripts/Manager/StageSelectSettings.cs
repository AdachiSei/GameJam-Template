using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DisturbMagic;

/// <summary>
/// ステージセレクト画面を設定してくれるScript
/// </summary>
public class StageSelectSettings : MonoBehaviour
{
    [SerializeField]
    [Header("設定したい奴")]
    GameObject[] _stage;

    [SerializeField]
    [Header("Context")]
    VerticalLayoutGroup _context;

    [SerializeField]
    [Header("設定したい奴を格納するゲームオブジェクト")]
    HorizontalLayoutGroup _hGroup;

    [SerializeField]
    [Header("いくつずつ設定するか")]
    int _count = 3;

    public void Setting(int count)
    {
        var limitCount = 0;
        var stageCount = _stage.Length - DMInt.ONE;
        var hGroupCount = stageCount / count;
        for (int i = 0; i < _stage.Length - DMInt.ONE; i++)
        {
            foreach (Transform tr in _hGroup.transform)
            {
                limitCount++;
                _stage[0].transform.parent = tr.parent;
                if (limitCount > count) break;
            }
        }
        
    }

    [ContextMenu("Test")]
    public void Test()
    {
        Debug.Log((_stage.Length - DMInt.ONE)/_count + 1);
    }
}
