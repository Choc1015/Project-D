using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    public Image value;
    public void SetHPValue(float curValue, float maxValue)
    {
        value.fillAmount = curValue / maxValue;
    }
}
