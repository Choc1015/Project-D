using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : Human
{
    [SerializeField] private SkillCommandController skillController;

    public Transform attackPos;
    public SpriteRenderer sprite;
    public AnimationTrigger animTrigger;
    public PlayerStateMachine playerState;

    public Vector3 moveDir;
    public Vector3 lookDIr_X;

    private string defenseType;

    //private bool isJumpInput, isJump;
    //private float jumpStartPoint;

    void Awake()
    {
        statController.Init();
        Utility.playerController = this;
        playerState = new();
    }

    void Update()
    {
        
        skillController.ControllerAction();
        sprite.flipX = lookDIr_X.x == -1 ? true : false;


    }
    public void ChangeDefenseType(string defenseType = "") => this.defenseType = defenseType;
    public void TakeDamage(float attackDamage, Human attackHuman = null)
    {
        if (defenseType == "BasicDefense")
            attackDamage *= 0.5f;
        else if (defenseType == "GodDefense")
            attackDamage *= 0.1f;
        else if (defenseType == "ReflectionDefense")
        {
            attackDamage *= 0.3f;
            attackHuman.TakeDamage(attackDamage);
        }

        TakeDamage(attackDamage);
    }
    public void StopMove()
    {
        movement.StopMove();
        animTrigger.TriggerAnim("isMove", AnimationType.Bool, false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPos.position+lookDIr_X, Vector2.one * 1.5f);
    }
}
