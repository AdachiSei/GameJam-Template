using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �X�e�[�W�Z���N�g��ʂ�ݒ肵�Ă����Script
/// </summary>
public class StageSelectSettings : MonoBehaviour
{
    [SerializeField]
    [Header("�ݒ肵�����z")]
    GameObject[] _gameObject;

    [SerializeField]
    [Header("Context")]
    HorizontalLayoutGroup _hGroup;

    [SerializeField]
    [Header("�ݒ肵�����z���i�[����Q�[���I�u�W�F�N�g")]
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
