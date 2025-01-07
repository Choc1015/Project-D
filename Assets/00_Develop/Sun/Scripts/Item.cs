using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemKind { Buff, Heal };
public class Item : MonoBehaviour
{
    public ItemKind kind;
    public StatInfo statInfo;
    public float value;

    public void UseItem()
    {
        if(kind == ItemKind.Buff)
            Utility.GetPlayerStat().BuffStat(statInfo, value);
        else
            Utility.GetPlayer().Heal(statInfo, value);
    }
}
