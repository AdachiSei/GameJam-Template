using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Calculator
{

    public static float Probability(float[] num)
    {
        float[] probability = { };
        float sum = num.Sum();
        var limitCount = 1;
        Array.Resize(ref probability, num.Length);
        for (int index = 0; index < num.Length; index++)
        {
            for (int count = 0; count < limitCount; count++)
            {
                probability[index] += num[count] * 100f / sum;
            }
             Debug.Log(index + "”Ô–Ú " + probability[index]);
            limitCount++;
        }
        var random = UnityEngine.Random.Range(0f,100f);
        Debug.Log("—” " + random);
        var result = probability.FirstOrDefault(p => p > random);
        Debug.Log("Œ‹‰Ê‚Í..." + result);
        return result;
    }
}