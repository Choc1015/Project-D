using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatInfo { Health, AttackDamage, AttackDelay, MoveSpeed, AttakRange, Mana };
[System.Serializable]
public class Stat 
{
    public StatInfo statInfo;
    [SerializeField] private float maxValue;
    [SerializeField] private float curValue;
    public float Value 
    { 
        set 
        { 
            curValue = value;
            if (curValue > maxValue)
                curValue = maxValue;
        } 
        get 
        { 
            return curValue; 
        } 
    }
    public float GetMaxValue() => maxValue;
    public void BuffStat(float newMaxValue)
    {
        maxValue += newMaxValue;
        curValue += newMaxValue;
    }
}
