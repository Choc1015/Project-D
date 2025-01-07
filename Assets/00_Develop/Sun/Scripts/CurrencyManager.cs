using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CurrencyManager : Singleton<CurrencyManager>
{
    public float Gold { get; private set; }
    public Action updateGold;
    public void EarnGold(float value)
    {
        Gold += value;
        updateGold?.Invoke();
    }
    public void SpendGold(float value)
    {
        Gold -= value;
        updateGold?.Invoke();
    }
}
