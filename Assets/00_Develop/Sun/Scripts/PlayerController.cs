using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Human
{
    [SerializeField] private PlayerInput playerInput;
    private Vector3 moveDir;

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

        moveDir = playerInput.InputMove();

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

        if (moveDir.magnitude > 0)
        {
            movement.MoveTo(moveDir, statController.GetStat(StatInfo.MoveSpeed).Value);
        }
        else
            movement.StopMove();
    }
}
