using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;


public class StatController : MonoBehaviour
{
    public StatSO statSO;

    [SerializeField] private StatSO Stats;
    private Dictionary<StatInfo, Stat> stats = new();
    public void Init(bool isPlayer =false)
    {
        if (isPlayer)
        {
            stats.Clear();
            if (Utility.GetPlayerStat() == null)
            {
                Stats = statSO.Clone() as StatSO;
                Utility.SetStat(Stats);
            }
            else
            {
                Stats = Utility.GetPlayerStat();
                
            }
        }
        else
            Stats = statSO.Clone() as StatSO;

        foreach (Stat stat in Stats.Stats)
        {
            stats.Add(stat.statInfo, stat);
        }
    }

    public Stat GetStat(StatInfo statInfo)
    {
        return stats[statInfo];
    }
    public StatSO GetStatSO() => Stats;
}
