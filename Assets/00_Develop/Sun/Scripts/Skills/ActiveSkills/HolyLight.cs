using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyLight : InstallationSkill
{
    public override void UseSkill()
    {
        int layerMask = (1 << LayerMask.NameToLayer("Player")) + (1 << LayerMask.NameToLayer("Enemy"));
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, skillRange, Vector2.zero, layerMask);
        foreach(RaycastHit2D hit in hits)
        {
            if (hit.collider.CompareTag("Player"))
            {
                Debug.Log(hit.collider+" Player Buff");
            }
            else
            {
                Debug.Log(hit.collider + " Enemy Debuff");
            }
        }
        //targetPlayer.HealHealth(value);
        //Debug.Log($"{targetPlayer} Heal ! : {value}");
        base.UseSkill();
    }
}
