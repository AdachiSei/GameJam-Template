using System;
using UnityEngine;

/// <summary>
/// ポーズに必要なScripts
/// </summary>
public class PauseManager : SingletonMonoBehaviour<PauseManager>
{
    public bool IsPausing { get; private set; } = false;

    public event Action OnPause;
    public event Action OnResume;

    void Update()
    {
        if (Input.GetButtonDown(InputName.CANCEL))
        {
            if (!IsPausing)
            {
                IsPausing = true;
                OnPause();
            }
            else
            {
                IsPausing = false;
                OnResume();
            }
        }
    }
}
