using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealDrone : InstallationSkill
{
    private PlayerController targetPlayer;
    private void OnEnable()
    {
        targetPlayer = GameManager.Instance.players[Random.Range(0, GameManager.Instance.players.Count)];
        SpawnObj(targetPlayer.transform.position);
    }
    public override void UseSkill()
    {
        targetPlayer.HealHealth(value);
        Debug.Log($"{targetPlayer} Heal ! : {value}");
        base.UseSkill();
    }
}
