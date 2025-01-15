using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyLight : InstallationSkill
{
    private float timer = 5;
    public override void UseSkill()
    {
        int layerMask = (1 << LayerMask.NameToLayer("Player")) + (1 << LayerMask.NameToLayer("Enemy"));
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, skillRange, Vector2.zero, layerMask);
        foreach(RaycastHit2D hit in hits)
        {
            if (hit.collider.CompareTag("Player"))
            {
                Utility.GetPlayer().GetStatController().GetStat(StatInfo.MoveSpeed).BuffStat(3);
                Invoke("DisableSkill", timer);
            }
            else
            {
                hit.collider.GetComponent<Human>().GetStatController().GetStat(StatInfo.AttackDamage).Value *= 0.7f;
            }
        }
        //targetPlayer.HealHealth(value);
        //Debug.Log($"{targetPlayer} Heal ! : {value}");
        base.UseSkill();
    }

    public void DisableSkill() => Utility.GetPlayer().GetStatController().GetStat(StatInfo.MoveSpeed).BuffStat(-3);
}
