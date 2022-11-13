using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Calculator
{
    public static int Probability(int[] num)
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
        var random = UnityEngine.Random.Range(0f, 100f);
        Debug.Log("—” " + random);
        for (int i = 0; i < probability.Length; i++)
        {
            if (probability[i] > random)
            {
                Debug.Log("Œ‹‰Ê‚Í" + i);
                return i;
            }
        }
        return 0;
    }

    public static int Probability(float[] num)
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
        for (int i = 0; i < probability.Length; i++)
        {
            if(probability[i] > random)
            {
                Debug.Log("Œ‹‰Ê‚Í" + i);
                return i;
            }    
        }
        return 0;
    }
}