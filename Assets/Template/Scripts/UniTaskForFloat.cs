using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cysharp.Threading.Tasks
{
    public static class UniTaskForFloat
    {
        private const float OFFSET = 1000;

        async public static UniTask Delay(float delayTime)
        {
            await UniTask.Delay((int)(delayTime * OFFSET));
        }
    }
}
