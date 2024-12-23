using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Human
{
    [SerializeField] private PlayerInput playerInput;
    private Vector3 moveDir;
    private Vector3 lookDIr_X;

    private bool useAttack;
    private bool useSkill;
    private bool isJump;

    private RaycastHit2D[] hits;
    //private bool isJumpInput, isJump;
    //private float jumpStartPoint;
    void Start()
    {
        statController.Init();
    }

    void Update()
    {
        //if(!isJump)
        //    isJumpInput = playerInput.InputJump();


        InputKey();
        PlayerAction();


        //if (isJumpInput)
        //{
        //    isJump = true;
        //    movement.rigid.gravityScale = 2;
        //    movement.Jump(100);
        //    jumpStartPoint = transform.position.y;
        //    isJumpInput = false;
        //}

        //if (isJump)
        //{
        //    moveDir.y = movement.rigid.velocity.y;
        //    if(transform.position.y < jumpStartPoint)
        //    {
        //        isJump = false;
        //        movement.rigid.gravityScale = 0;
        //    }
        //}

        
    }

    private void InputKey()
    {
        moveDir = playerInput.InputMove();
        useAttack = playerInput.InputAttack();
        useSkill = playerInput.InputSkill();
        isJump = playerInput.InputJump();
    }
    private void PlayerAction()
    {
        //if(Mathf.Abs(moveDir.x) == 1 && moveDir.y == 0 && useAttack)
        //{
        //    Debug.Log("방어");
        //}
        //else if(Mathf.Abs(moveDir.x) == 1 && moveDir.y == -1 && isJump)
        //{
        //    Debug.Log($"{moveDir.x} 방향 슬라이딩");

        //}
        //else
        //{
        //    if (moveDir.magnitude > 0)
        //    {
        //        movement.MoveTo(moveDir, statController.GetStat(StatInfo.MoveSpeed).Value);
        //    }
        //    else
        //        movement.StopMove();
        //}

        if (useAttack)
        {
            hits = Physics2D.RaycastAll(transform.position, Vector3.left, 1, 1);
            foreach(RaycastHit2D hit in hits)
            {
                hit.collider.GetComponent<Human>().TakeDamage(statController.GetStat(StatInfo.AttackDamage).Value);
            }
            useAttack = false;
            statController.GetStat(StatInfo.AttackDelay).Value = statController.GetStat(StatInfo.AttackDelay).GetMaxValue();
        }

        if (moveDir.magnitude > 0)
        {
            movement.MoveTo(moveDir, statController.GetStat(StatInfo.MoveSpeed).Value);
        }
        else
            movement.StopMove();
    }
}
