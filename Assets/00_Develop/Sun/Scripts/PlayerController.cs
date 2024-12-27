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

    [SerializeField] private string defenseType = "";

    

    //private bool isJumpInput, isJump;
    //private float jumpStartPoint;

    void Awake()
    {
        statController.Init();
        Utility.playerController = this;
        GameManager.Instance.players.Add(this);
    }
    void Update()
    {
        if(playerState.CurrentState() == PlayerState.Idle)
        {
            skillController.ControllerAction();
            sprite.flipX = lookDIr_X.x == -1 ? true : false;
        }
        


    }
    public void ChangeDefenseType(string defenseType = "") => this.defenseType = defenseType;
    public override void TakeDamage(float attackDamage, Human attackHuman, string setStateName = "")
    {
        float damage = attackDamage;
        if (defenseType == "BasicDefense")
            damage = attackDamage * 0.5f;
        else if (defenseType == "GodDefense")
            damage = attackDamage * 0.1f;
        else if (defenseType == "ReflectionDefense")
        {
            damage = attackDamage * 0.3f;
            attackHuman.TakeDamage(damage, this, setStateName);
        }

        base.TakeDamage(damage, attackHuman, setStateName);
    }
    public void StopMove()
    {
        movement.StopMove();
        animTrigger.TriggerAnim("isMove", AnimationType.Bool, false);
        //Debug.Log("Stop");
    }
    protected override void KnockBackHuman(Vector3 dir)
    {
        playerState.ChangeState(PlayerState.KnockBack);
        base.KnockBackHuman(dir);
        Invoke("ResetState", 0.3f);
    }
    protected override void StunHuman(Vector3 dir)
    {
        base.StunHuman(dir);
        StartCoroutine(ResetState(0.5f, 1.5f));
    }
    IEnumerator ResetState(float t1, float t2)
    {
        yield return new WaitForSeconds(t1);
        StopMove();
        yield return new WaitForSeconds(t2);
        playerState.ChangeState(PlayerState.Idle);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPos.position+lookDIr_X, Vector2.one * 1.5f);
    }
}
