using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : Item
{
    
    public override void UseItem()
    {
        CurrencyManager.Instance.EarnGold(value);
        if (controller == null)
            controller = GetComponent<SoundController>();
        controller.PlayOneShotSound("GetItem");
        Despawn();
    }
}
