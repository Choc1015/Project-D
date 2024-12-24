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
    private bool isControlDisable;

    private RaycastHit2D[] hits;
    private int layermask = 0;
    //private bool isJumpInput, isJump;
    //private float jumpStartPoint;

    void Start()
    {
        statController.Init();
        Utility.playerController = this;
    }

    void Update()
    {
        //if(!isJump)
        //    isJumpInput = playerInput.InputJump();


        //InputKey();
        //if(!isControlDisable)
        //    PlayerAction();
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
        if (useAttack)
        {
            Attack();
            
        }
    }
    public void Attack()
    {
        if (layermask == 0)
            layermask = 1 << LayerMask.NameToLayer("Enemy");

        movement.StopMove();

        hits = Physics2D.RaycastAll(transform.position, Vector3.right, 1, layermask);

        foreach (RaycastHit2D hit in hits)
        {
            hit.collider.GetComponent<Human>().TakeDamage(statController.GetStat(StatInfo.AttackDamage).Value);
        }
        StartCoroutine(ControlDisableTime(statController.GetStat(StatInfo.AttackDelay).Value));

        Debug.Log("Attack");
    }
    IEnumerator ControlDisableTime(float timer)
    {
        isControlDisable = true;
        yield return new WaitForSeconds(timer);
        isControlDisable = false;
    }
}
