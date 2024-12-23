using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class StatController : MonoBehaviour
{
    public StatSO statSO;

    private StatSO Stats;
    private Dictionary<StatInfo, Stat> stats = new();
    private void Init()
    {
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
