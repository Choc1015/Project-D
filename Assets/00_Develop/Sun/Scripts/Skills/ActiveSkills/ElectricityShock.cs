using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricityShock : EnchantSkill
{
    void Start()
    {
        playerSkill.attackAE += DetectRange;
        SetLayerMask(1 << LayerMask.NameToLayer("Enemy"));
        Invoke("DespawnObj", despawnTimer);
    }
    protected override void UseSkill(RaycastHit2D hit)
    {
        playerSkill.GiveDamage(value, hit.collider.GetComponent<Human>(), new KnockBackInfo(Vector3.zero, 100, 0.1f, 0.4f));
    }
}