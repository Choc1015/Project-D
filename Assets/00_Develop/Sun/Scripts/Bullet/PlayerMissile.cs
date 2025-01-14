using Photon.Pun.Demo.Cockpit.Forms;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMissile : PlayerBullet
{
    private RaycastHit2D hit;
    private int layerMask;
    public override void Shot(BulletKind kind,Vector3 dir, float speed, float attackDamage, BulletController controller)
    {
        if (this.controller == null)
            this.controller = controller;

        this.dir = dir;
        moveSpeed = speed;
        this.attackDamage = attackDamage;
        this.kind = kind;
        StartCoroutine(FindDir());
        Invoke("DespawnBullet", 5);
        
    }

    IEnumerator FindDir()
    {
        while (true)
        {
            movement.MoveToRigid(dir, moveSpeed);
            
            yield return null;
            layerMask = 1 << LayerMask.NameToLayer("Enemy");
            hit = Physics2D.CircleCast(transform.position, 5, Vector2.zero, 0, layerMask);
            if (hit)
            {
                dir = hit.transform.position - transform.position;
                transform.right = dir;
                dir = dir.normalized;
            }
        }
        
    }
}
