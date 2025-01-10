using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealDrone : InstallationSkill
{
    private PlayerController targetPlayer;
    private void OnEnable()
    {
        targetPlayer = Utility.GetPlayer();
        SpawnObj(targetPlayer.transform.position);
    }
    public override void UseSkill()
    {
        targetPlayer.HealHealth(value);
        targetPlayer.ActiveUpdatePlayerUI();
        Debug.Log($"{targetPlayer} Heal ! : {value}");
        base.UseSkill();
    }
}
