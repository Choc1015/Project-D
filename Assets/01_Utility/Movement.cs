using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Rigidbody2D rigid;

    public void MoveToRigid(Vector3 dir, float moveSpeed)
    {
        if(rigid != null)
        {
            if (dir == Vector3.zero)
            {
                rigid.velocity = Vector3.zero;
                return;
            }
            rigid.velocity = dir.normalized * moveSpeed;
        }
    }
    public void MoveToRigid(Vector3 dir, float moveSpeed, bool isDie)
    {
        if (rigid != null)
        {
            if (!isDie)
            {
                rigid.bodyType = RigidbodyType2D.Static;
            }
        }
    }
    public void MoveToTrans(Vector2 dir, float moveSpeed)
    {
        transform.Translate(dir.normalized * moveSpeed * Time.deltaTime);
    }
    public void AddForce(Vector3 dir, float power)
    {
        if (rigid != null)
        {
            rigid.AddForce(dir.normalized * power);
        }
    }
    public void Jump(float jumpPower)
    {
        if (rigid != null)
        {
            rigid.AddForce(Vector3.up * jumpPower);
        }
    }
    public void StopMove()
    {
        if(rigid != null && rigid.velocity.magnitude > 0)
        {
            rigid.velocity = Vector3.zero;
        }
    }
    public void KnockBack(KnockBackInfo info)
    {
        if(rigid != null)
        {
            StopMove();
            rigid.AddForce(info.dir.normalized * info.power);
        }
    }
}
