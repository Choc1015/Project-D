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
            rigid.velocity = dir.normalized * moveSpeed;
        }
    }
    public void MoveToRigid(Vector3 dir, float moveSpeed, bool isMaxPosion)
    {
        if (rigid != null)
        {
            rigid.velocity = dir.normalized * moveSpeed;
            if (isMaxPosion)
            {
                Vector3 newVelocity = rigid.velocity; // 현재 속도 가져오기
                newVelocity.y = -dir.normalized.y;                    // Y축 속도 설정
                rigid.velocity = newVelocity;         // 수정된 속도 적용
            }
        }
    }
    public void MoveToTrans(Vector3 dir, float moveSpeed)
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
