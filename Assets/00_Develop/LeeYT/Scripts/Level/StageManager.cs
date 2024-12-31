using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : Singleton<StageManager>
{
    public int WaveEnemyCount;
    public bool IsStopCamera= false;

    private void Update()
    {
        if(WaveEnemyCount == 0)
            IsStopCamera = false;
    }

}
