using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
/*using Photon;
using Photon.Pun;*/
using DG.Tweening;
using UnityEditor.U2D.Aseprite;

public enum PlayerType { None, Warrior, Priest, Wizard };
public class PlayerController : Human/*, IPunObservable*/
{
    //public string playerClass;
    public PlayerType playerType;
    [SerializeField] private SkillCommandController skillController;
    [SerializeField] private SkillFunctionController skillFunctionsController;
    [SerializeField] private PlayerUI playerUI;

    public Transform attackPos;
    public SpriteRenderer sprite;
    public SpriteRenderer playerBlessing;
    public GameObject soul;
    public AnimationTrigger animTrigger;
    private PlayerStateMachine playerState;
    public PlayerStateMachine GetPlayerState() => playerState;
    private PlayerSkill playerSkill;

    public Vector3 moveDir;
    public Vector3 lookDir_X, lateLookDir_X;
    public Vector3 offset;
    public CloneLight spriteLight;
    [SerializeField] private string defenseType = "";
    [SerializeField] private float defenseValue;
    private bool isInvincibility;

    [SerializeField] private SkillSwap skillSwapPrefab;
    public SkillSwap skillSwapUI;

    public int maxCombo;
    private int attackCombo;
    private float comboTimer;

    private SoundController soundController;

    private Color baseColor, dieColor;

    public PlayerSkill GetPlayerSkill() => playerSkill;
    //private bool isJumpInput, isJump;
    //private float jumpStartPoint;
    // public PhotonView pv;

    private ReviveInfo reviveInfo = new();

    public Effect enemyHitPrefab;
    private ObjectPool<Effect> EnemyHitObjPool;

    private float deathTimer;

   

    void Awake()
    {
        //if (!pv.IsMine)
        //    return;
        EnemyHitObjPool = new ObjectPool<Effect>(enemyHitPrefab, 5, null);

        //GameManager.Instance.players.Add(this);
        baseColor = new Color(1, 1, 1, 1f);
        dieColor = new Color(1, 1, 1, 0f);

        soundController = GetComponent<SoundController>();
        playerSkill = GetComponent<PlayerSkill>();
        playerState = GetComponent<PlayerStateMachine>();
        playerUI = UIManager.Instance.GetPlayerUI();
        skillSwapUI = UIManager.Instance.SpawnSkillSwapUI(skillSwapPrefab);
        skillSwapUI.Init(skillFunctionsController);
        //skillSwapUI = PhotonNetwork.Instantiate($"Prefabs/UI/SkillUI_{playerClass}", Vector3.zero, Quaternion.identity).GetComponent<SkillSwap>();
        //skillSwapUI.Init(skillFunctionsController);

        //playerUI = PhotonNetwork.Instantiate("Prefabs/UI/PlayerUI", Vector3.zero, Quaternion.identity).GetComponent<PlayerUI>();
    }
    public void SpawnHitEffect(Vector3 pos)
    {
        EnemyHitObjPool.SpawnObject().Spawn(EnemyHitObjPool, pos,transform);
    }

    private void OnEnable()
    {
        Init();
    }
    private void Init()
    {

        statController.Init(true);

        Utility.SetPlayer(gameObject);
        L_CinemachineCameraController.playerTrans = Utility.GetPlayerTr();
        ActiveUpdatePlayerUI();
    }
    //[PunRPC]
    public void LocalUpdate(bool flipX)
    {
        sprite.transform.localScale = flipX?Vector3.one+(Vector3.left*2): Vector3.one;
        spriteLight?.ChangeSprite();
    }
    private void Update()
    {
        //if (!pv.IsMine)
        //return;

        //pv.RPC("LocalUpdate", RpcTarget.All, lookDIr_X.x == -1 ? true : false);
        LocalUpdate(lateLookDir_X.x == -1 ? true : false);
        transform.position = GameManager.Instance.GetClampPosition(transform);
        playerSkill.movementAfterDelay -= Time.deltaTime;

        if(playerState.CurrentState() == PlayerState.Die)
        {
            deathTimer -= Time.deltaTime;
            if(deathTimer < 0)
            {
                Debug.Log("게임오버");
            }
        }
    }
    void LateUpdate()
    {
        //if (!pv.IsMine)
        //    return;
        if (GameManager.Instance.currentState == GameState.Stop)
        {
            StopCommand();
            return;
        }
        

        if (CanAction(PlayerState.Idle) || CanAction(PlayerState.Die))
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
    public bool CanAction(PlayerState state) => playerState.CurrentState() == state;
    public void Combo()
    {
        
        animTrigger.TriggerAnim("Attack", AnimationType.Trigger);
        animTrigger.SetIntegerAnim("Combo", attackCombo);
        attackCombo++;
        comboTimer = 0.6f;
        if (maxCombo == attackCombo)
            ResetCombo();
        
    }
    public int GetCombo() => attackCombo;
    private void ResetCombo()
    {
        attackCombo = 0;
    }
    public void ChangeDefenseType(string defenseType = "", float defenseValue = 0)
    {
        if (this.defenseValue > 0)
            return;
        this.defenseType = defenseType;
        this.defenseValue = defenseValue;
    }
    public void ResetDefenseType()
    {
        defenseType = "";
        defenseValue = 0;
    }
    public string GetDefenseType() => defenseType;
    public override void TakeDamage(float attackDamage, Human attackHuman, KnockBackInfo info=null)
    {
        //if (!pv.IsMine)
        //    return;
        if (playerState.CurrentState() == PlayerState.Die || isInvincibility)
            return;
        if (this.info != null && this.info.isKnockBack)
            return;

        soundController.PlayOneShotSound("Hit");

        float damage = attackDamage;
        if (defenseType == "BasicDefense")
            damage = attackDamage * 0.2f;
        else if (defenseType == "GodDefense" || defenseType == "ReflectionDefense" || defenseType == "MagicDefense")
        {
            defenseValue -= damage;
            if (defenseType == "ReflectionDefense")
                attackHuman.TakeDamage(attackDamage, this, info);

            if (defenseValue <= 0)
                ResetDefenseType();
            else                    
                damage = attackDamage * 0f;
        }
        if (damage != attackDamage)
            info.ResetValue();
        ActiveInvincibility(0.1f);
        base.TakeDamage(damage, attackHuman, info);

        int randHitVoice = UnityEngine.Random.Range(0, 4);
        if (randHitVoice == 0)
            soundController.PlayOneShotSound("HitVoice");

        if (playerState.CurrentState() != PlayerState.Die)
        {
            if (damage != attackDamage)
                animTrigger.TriggerAnim("DefenseHit", AnimationType.Trigger);
            else
                StartCoroutine(Stun());
        }

        ActiveUpdatePlayerUI();
    }
    public void ActiveInvincibility(float resetTimer)
    {
        isInvincibility = true;
        Invoke("ResetIsInvincibility", resetTimer);
    }
    private void ResetIsInvincibility()  => isInvincibility = false;
    public void ActiveUpdatePlayerUI()
    {
        playerUI?.SetValue(StatInfo.Health, statController.GetStat(StatInfo.Health).GetMaxValue(), statController.GetStat(StatInfo.Health).Value);
        playerUI?.SetValue(StatInfo.Mana, statController.GetStat(StatInfo.Mana).GetMaxValue(), statController.GetStat(StatInfo.Mana).Value);
    }
    //private void UpdatePlayerUI(StatInfo stat, float maxValue, float curValue)
    //{
    //    playerUI?.pv.RPC("SetValue", RpcTarget.All,stat, maxValue, curValue);
    //}
    private IEnumerator KnockBack()
    {
        
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

        if (this.info.isKnockBack)
            animTrigger.TriggerAnim("KnockBack", AnimationType.Trigger);
        else
            animTrigger.TriggerAnim("Stun", AnimationType.Trigger);

        yield return new WaitForSeconds(info.stunTime);
        StartCoroutine(KnockBack());
        movement.StopMove();
    }
    public void StopCommand()
    {
        movement.StopMove();
        animTrigger.TriggerAnim("isMove", AnimationType.Bool, false);
        playerSkill.OffDefense();
        lookDir_X = Vector3.zero;
        //playerState.ChangeState(PlayerState.Idle);
    }
    protected override void DieHuman()
    {
        playerState.ChangeState(PlayerState.Die);
        soundController.PlayOneShotSound("Die");
        sprite.DOColor(dieColor, 0.5f);
        playerBlessing.DOColor(dieColor, 0.5f);
        soul.SetActive(true);
        movement.StopMove();
        deathTimer = 20;
        //base.DieHuman();
    }
    
    public override void Revive()
    {
        if(playerState.CurrentState() == PlayerState.Die)
        {
            soundController.PlayOneShotSound("revive");
            playerState.ChangeState(PlayerState.Idle);
            sprite.DOColor(baseColor, 0.5f);
            playerBlessing.DOColor(baseColor, 0.5f);
            soul.SetActive(false);
            Utility.GetPlayer().HealHealth(999);
            GameManager.Instance.RevivePlayer(this, reviveInfo);
            reviveInfo.canRevive = false;
            reviveInfo.nextPlayer = default;
            reviveInfo.statue = default;
            GameManager.Instance.maxXTemp = 0;
        }
        
    }
    public void ResetState()
    {
        playerState.ChangeState(PlayerState.Idle);
    }
    public void ActiveSkillSwap()
    {
        //skillSwapUI.pv.RPC("ActiveSkillSwap", RpcTarget.All);
        skillSwapUI?.ActiveSkillSwap();
    }
    public void DisableSkillSwap()
    {
        skillSwapUI.DisableSkillSwap();
    }
    public Statue GetHitStatue() => reviveInfo.statue;
    public void OnTriggerStatue(bool isOn, PlayerType playerType, Statue statue)
    {
        reviveInfo.canRevive = isOn;

        if (isOn)
        {
            reviveInfo.nextPlayer = playerType;
            reviveInfo.statue = statue;
        }
        else
        {
            reviveInfo.nextPlayer = default;
            reviveInfo.statue = default;
        }

    }
    public void Heal(StatInfo info,float healValue)
    {
        if (statController != null)
        {
            statController.GetStat(info).Value += statController.GetStat(info).GetMaxValue()*healValue;
            ActiveUpdatePlayerUI();
        }
    }

    public bool CanRevive() => reviveInfo.canRevive;
    public void PlayOneShotSound(string soundName)
    {
        soundController.PlayOneShotSound(soundName);
    }
    //public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    //{
    //    Vector3 vec = default;
    //    try
    //    {
    //        vec = (Vector3)stream.ReceiveNext();
    //    }
    //    catch
    //    {

    //    }
    //    if (stream.IsWriting)
    //    {
    //        stream.SendNext(transform.position);
    //    }
    //    else
    //    {
    //        transform.DOMove(vec, 0.2f, true);
    //    }

    //}
}
