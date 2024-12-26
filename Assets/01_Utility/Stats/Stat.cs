using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatInfo { Health, AttackDamage, AttackDelay, MoveSpeed, AttakRange };
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
    public float SetMaxValue(float newMaxValue) => maxValue = newMaxValue;
}
