using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatInfo { Health, AttackDamage, AttackDelay, MoveSpeed };
[System.Serializable]
public class Stat 
{
    public StatInfo statInfo;
    [SerializeField] private float maxValue;
    [SerializeField] private float curValue;
    public float Value { set { curValue = value; } get { return curValue; } }

}
