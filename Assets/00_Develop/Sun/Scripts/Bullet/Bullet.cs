using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletKind { Bullet, Missile };
public class Bullet : MonoBehaviour
{
    protected BulletKind kind;
    protected Vector3 dir;
    public Movement movement;
    protected BulletController controller;
    protected float attackDamage;
    protected float moveSpeed;
    public virtual void Shot(BulletKind kind,Vector3 dir,  float speed, float attackDamage, BulletController controller)
    {
        if (this.controller == null)
            this.controller = controller;
        movement.MoveToRigid(dir, speed);
        this.attackDamage = attackDamage;
        this.kind = kind;
        Invoke("DespawnBullet", 5);
    }
    protected void DespawnBullet()
    {
        controller.DespawnBullet(kind, this);
    }
}
