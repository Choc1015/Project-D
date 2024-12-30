using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 dir;
    public Movement movement;
    protected BulletController controller;
    protected float attackDamage;
    public void Shot(Vector3 dir, float speed, float attackDamage, BulletController controller)
    {
        if (this.controller == null)
            this.controller = controller;
        movement.MoveToRigid(dir, speed);
        this.attackDamage = attackDamage;
        Invoke("DespawnBullet", 5);
    }
    protected void DespawnBullet()
    {
        controller.DespawnBullet(this);
    }
}
