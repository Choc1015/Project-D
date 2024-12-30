using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : Bullet
{
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Enemy"))
        {
            coll.GetComponent<Human>().TakeDamage(attackDamage, Utility.playerController, new KnockBackInfo(Vector3.zero, 150, 0.2f, 0.3f));
            CancelInvoke();
            DespawnBullet();
        }
    }
}
