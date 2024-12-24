using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Rigidbody2D rigid;

    public void MoveTo(Vector3 dir, float moveSpeed)
    {
        if(rigid != null)
        {
            rigid.velocity = dir.normalized * moveSpeed;
        }
    }
    //public void Jump(float jumpPower)
    //{
    //    if(rigid != null)
    //    {
    //        rigid.AddForce(Vector3.up * jumpPower);
    //    }
    //}
    public void StopMove()
    {
        if(rigid != null && rigid.velocity.magnitude > 0)
        {
            rigid.velocity = Vector3.zero;
        }
    }
}