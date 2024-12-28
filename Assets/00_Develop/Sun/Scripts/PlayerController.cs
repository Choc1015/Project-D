using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;


public class PlayerController : Human
{
    [SerializeField] private SkillCommandController skillController;
    [SerializeField] private PlayerUI playerUI;

    public Transform attackPos;
    public SpriteRenderer sprite;
    public AnimationTrigger animTrigger;
    public PlayerStateMachine playerState;

    public Vector3 moveDir;
    public Vector3 lookDIr_X;
    public CloneLight spriteLight;
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
        spriteLight?.ChangeSprite();
        if (playerState.CurrentState() == PlayerState.Idle)
        {
            skillController.ControllerAction();
            sprite.flipX = lookDIr_X.x == -1 ? true : false;
        }

    }
    public void ChangeDefenseType(string defenseType = "") => this.defenseType = defenseType;

    public override void TakeDamage(float attackDamage, Human attackHuman, KnockBackInfo info=null)
    {
        if (this.info != null && this.info.isLKnockBack)
            return;

        float damage = attackDamage;
        if (defenseType == "BasicDefense")
            damage = attackDamage * 0.5f;
        else if (defenseType == "GodDefense")
            damage = attackDamage * 0.1f;
        else if (defenseType == "ReflectionDefense")
        {
            damage = attackDamage * 0.3f;

            attackHuman.TakeDamage(damage, this, info);
        }

        base.TakeDamage(damage, attackHuman, info);

        if(playerState.CurrentState() != PlayerState.Die)
        {
            StartCoroutine(Stun());
        }
        playerUI.SetValue(StatInfo.Health, statController.GetStat(StatInfo.Health).GetMaxValue(), statController.GetStat(StatInfo.Health).Value);
    }
    private IEnumerator KnockBack()
    {
        yield return new WaitForSeconds(info.knockBackTime);
        playerState.ChangeState(PlayerState.Idle);
        movement.StopMove();
        if (this.info.isLKnockBack)
            this.info.isLKnockBack = false;

    }
    private IEnumerator Stun()
    {
        playerState.ChangeState(PlayerState.Stun);
        yield return new WaitForSeconds(info.stunTime);
        StartCoroutine(KnockBack());
        movement.StopMove();
    }
    public void StopMove()
    {
        movement.StopMove();
        animTrigger.TriggerAnim("isMove", AnimationType.Bool, false);
    }
    protected override void DieHuman()
    {
        playerState.ChangeState(PlayerState.Die);
        base.DieHuman();
    }
    public void ResetState()
    {
        playerState.ChangeState(PlayerState.Idle);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPos.position+lookDIr_X, Vector2.one * 1.5f);
    }
    
}
