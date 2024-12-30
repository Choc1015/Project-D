using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static event Action<int> OnWaveStart;

    public static void WaveStart(int wavePoint)
    {
        OnWaveStart?.Invoke(wavePoint);
    }

}
