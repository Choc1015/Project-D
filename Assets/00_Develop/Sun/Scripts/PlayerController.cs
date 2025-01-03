using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Photon;
using Photon.Pun;

public class PlayerController : Human
{
    [SerializeField] private SkillCommandController skillController;
    [SerializeField] private SkillFunctionController skillFunctionsController;
    [SerializeField] private PlayerUI playerUI;

    public Transform attackPos;
    public SpriteRenderer sprite;
    public AnimationTrigger animTrigger;
    public PlayerStateMachine playerState;

    public Vector3 moveDir;
    public Vector3 lookDIr_X;
    public CloneLight spriteLight;
    [SerializeField] private string defenseType = "";

    [SerializeField] private SkillSwap skillSwapPrefab;
    public SkillSwap skillSwapUI;

    public int maxCombo;
    private int attackCombo;
    private float comboTimer;

    public SoundController soundController;
    //private bool isJumpInput, isJump;
    //private float jumpStartPoint;
    public PhotonView pv;

    void Awake()
    {
        if (!pv.IsMine)
            return;

        statController.Init();
        Utility.playerController = this;
        L_CinemachineCameraController.playerTrans = Utility.GetPlayerTr();
        GameManager.Instance.players.Add(this);
        skillSwapUI = Instantiate(skillSwapPrefab, GameObject.Find("UI").transform);
        skillSwapUI.Init(skillFunctionsController);

        playerUI = PhotonNetwork.Instantiate("Prefabs/UI/PlayerUI", Vector3.zero, Quaternion.identity).GetComponent<PlayerUI>();
        
    }
    [PunRPC]
    public void LocalUpdate(bool flipX)
    {
        sprite.flipX = flipX;
        spriteLight?.ChangeSprite();
    }
    private void Update()
    {
        if (!pv.IsMine)
            return;

        pv.RPC("LocalUpdate", RpcTarget.All, lookDIr_X.x == -1 ? true : false);
        
        transform.position = GameManager.Instance.GetClampPosition(transform);
    }
    void LateUpdate()
    {
        if (!pv.IsMine)
            return;

        if (playerState.CurrentState() == PlayerState.Idle)
        {
            skillController.ControllerAction();
            
        }
        
        if(attackCombo > 0 && comboTimer > 0)
        {
            comboTimer -= Time.deltaTime;
        }
        else if(comboTimer <= 0)
        {
            ResetCombo();
        }
    }
    public void Combo()
    {
        
        animTrigger.TriggerAnim("Attack", AnimationType.Trigger);
        animTrigger.SetIntegerAnim("Combo", attackCombo);
        attackCombo++;
        comboTimer = 0.6f;
        if (maxCombo == attackCombo)
            ResetCombo();
        
    }
    private void ResetCombo()
    {
        attackCombo = 0;
    }
    public void ChangeDefenseType(string defenseType = "")
    {
        this.defenseType = defenseType;
    }
    public string GetDefenseType() => defenseType;
    public override void TakeDamage(float attackDamage, Human attackHuman, KnockBackInfo info=null)
    {
        if (!pv.IsMine)
            return;

        if (this.info != null && this.info.isKnockBack)
            return;

        soundController.PlayOneShotSound("Hit");

        float damage = attackDamage;
        if (defenseType == "BasicDefense")
            damage = attackDamage * 0.2f;
        else if (defenseType == "GodDefense")
            damage = attackDamage * 0f;
        else if (defenseType == "ReflectionDefense")
        {
            damage = attackDamage * 0.2f;

            attackHuman.TakeDamage(attackDamage*0.8f, this, info);
        }
        if (damage != attackDamage)
            info.ResetValue();
        base.TakeDamage(damage, attackHuman, info);
        if (playerState.CurrentState() != PlayerState.Die)
        {
            if (damage != attackDamage)
                animTrigger.TriggerAnim("DefenseHit", AnimationType.Trigger);
            else
                StartCoroutine(Stun());
        }

        ActiveUpdatePlayerUI();
        //UpdatePlayerUI(StatInfo.Health, statController.GetStat(StatInfo.Health).GetMaxValue(), statController.GetStat(StatInfo.Health).Value);
    }
    public void ActiveUpdatePlayerUI()
    {
        pv.RPC("UpdatePlayerUI", RpcTarget.All, StatInfo.Health, statController.GetStat(StatInfo.Health).GetMaxValue(), statController.GetStat(StatInfo.Health).Value);
    }
    [PunRPC]
    public void UpdatePlayerUI(StatInfo stat, float maxValue, float curValue)
    {
        playerUI?.SetValue(stat, maxValue, curValue);
    }
    private IEnumerator KnockBack()
    {
        if(this.info.isKnockBack)
            animTrigger.TriggerAnim("KnockBack", AnimationType.Trigger);
        yield return new WaitForSeconds(info.knockBackTime);
        playerState.ChangeState(PlayerState.Idle);
        movement.StopMove();
        if (this.info.isKnockBack)
        {
            this.info.isKnockBack = false;
            animTrigger.TriggerAnim("EndKnockBack", AnimationType.Trigger);
        }

    }
    private IEnumerator Stun()
    {
        playerState.ChangeState(PlayerState.Stun);
        animTrigger.TriggerAnim("Stun", AnimationType.Trigger);
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
    public void ActiveSkillSwap()
    {
        skillSwapUI.ActiveSkillSwap();
    }
    public void DisableSkillSwap()
    {
        skillSwapUI.DisableSkillSwap();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPos.position+lookDIr_X, Vector2.one * 1.5f);
    }
    
}
