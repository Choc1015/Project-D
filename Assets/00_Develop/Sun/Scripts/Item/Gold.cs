using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : Item
{
    public override void UseItem()
    {
        CurrencyManager.Instance.EarnGold(value);
        Despawn();
    }
}
