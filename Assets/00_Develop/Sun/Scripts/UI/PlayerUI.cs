using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : UIBase
{
    [SerializeField] private Image hpValue;
    [SerializeField] private Image manaValue;
    public void SetValue(StatInfo stat, float maxValue, float curValue)
    {
        if (stat == StatInfo.Health)
            hpValue.fillAmount = curValue / maxValue;
        else if(stat == StatInfo.Mana)
            manaValue.fillAmount = curValue / maxValue;
    }
    
}