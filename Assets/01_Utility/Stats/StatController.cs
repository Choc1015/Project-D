using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;


public class StatController : MonoBehaviour
{
    public StatSO statSO;

    private StatSO Stats;
    private Dictionary<StatInfo, Stat> stats = new();
    public void Init()
    {
        if (Stats != default)
            stats.Clear();
        Stats = statSO.Clone() as StatSO;
        foreach(Stat stat in Stats.Stats)
        {
            stats.Add(stat.statInfo, stat);
        }
    }

    public Stat GetStat(StatInfo statInfo)
    {
        return stats[statInfo];
    }
}
