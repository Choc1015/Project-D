using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HittingDaragon : Human
{
    DragonBoss boss;

    private void Start()
    {
        boss = GetComponentInParent<DragonBoss>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damage(collision);

        if (collision.gameObject.CompareTag("Player"))
        {
            if (boss.isAttack == false)
            {
                return;
            }
            EffectManager.Instance.PlayDragonHitEffect(Utility.GetPlayerTr().position + Vector3.up * 2);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Damage(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Damage(collision);
    }
    private void Damage(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (boss.isAttack == false)
            {
                return;
            }

            Debug.Log("Hitting");
            Utility.GetPlayer().TakeDamage(0.1f, this, new KnockBackInfo(Vector3.zero, 100, 0.1f, 0.2f));
            Debug.LogWarning("ÇÇÇØ");
        }
    }
}
