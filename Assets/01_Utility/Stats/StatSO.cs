using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StatSO", menuName = "Scriptable Objects/StatSO")]
public class StatSO : ScriptableObject
{
    public Stat[] Stats;

    public object Clone()
    {
        return Instantiate(this);
    }

    public void BuffStat(StatInfo statInfo, float value)
    {
        foreach(Stat stat in Stats)
        {
            if(stat.statInfo == statInfo)
            {
                stat.BuffStat(value); 
            }
        }
    }
}
