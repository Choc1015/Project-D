using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PlayerSkill : MonoBehaviour
{
    private PlayerController playerController;
    private BulletController bulletController;
    private Human hitEnemyTemp;
    private List<Human> hitEnemyTempList = new();
    [SerializeField]private bool useDashAttack;
    public bool isCritical;

    public Action attackAE; // Attack Additional Effects

    public float attackDelayTime;
    public float movementAfterDelay;

    void Start()
    {
        playerController = Utility.playerController;
        bulletController = GetComponent<BulletController>();
    }
    public void Move_X(float x)
    {
        if ((playerController.CanAction(PlayerState.Idle) || playerController.CanAction(PlayerState.Die)) && movementAfterDelay <= 0)
        {
            playerController.movement.StopMove();
            float moveSpeed = playerController.GetStatController().GetStat(StatInfo.MoveSpeed).Value;
            if (playerController.CanAction(PlayerState.Die))
                moveSpeed *= 1.5f;
            playerController.lookDir_X = Vector3.right * x;
            playerController.lateLookDir_X = playerController.lookDir_X;
            playerController.movement.MoveToTrans(playerController.lookDir_X, moveSpeed);
            playerController.animTrigger.TriggerAnim("isMove", AnimationType.Bool, true);
        } 
        
    }
    public void Move_Y(float y)
    {
        if ((playerController.CanAction(PlayerState.Idle) || playerController.CanAction(PlayerState.Die)) && movementAfterDelay <= 0)
        {
            float moveSpeed = playerController.GetStatController().GetStat(StatInfo.MoveSpeed).Value;
            if (playerController.CanAction(PlayerState.Die))
                moveSpeed *= 1.5f;
            playerController.movement.MoveToTrans(Vector3.up * y, moveSpeed);
            playerController.animTrigger.TriggerAnim("isMove", AnimationType.Bool, true);
        }

        
    }
    public void Jump(float x)
    {
        if ((playerController.CanAction(PlayerState.Idle)) && movementAfterDelay <= 0)
        {
            playerController.PlayOneShotSound("Jump");
            float moveSpeed = playerController.GetStatController().GetStat(StatInfo.MoveSpeed).Value;
            //playerController.movement.MoveToRigid(Vector3.right * x, moveSpeed);
            playerController.movement.MoveToRigid(playerController.lookDir_X, moveSpeed);
            playerController.animTrigger.TriggerAnim("JumpTrigger", AnimationType.Trigger);
            playerController.animTrigger.TriggerAnim("isMove", AnimationType.Bool, false);
            playerController.ActiveInvincibility(0.8f);
            Invoke("ResetJump", 0.8f);
            movementAfterDelay = 0.6f;
        }

    }
    
    public void Defense(string defenseType)
    {
        if (playerController.GetPlayerState().CurrentState() != PlayerState.Idle)
            return;
        playerController.ChangeDefenseType(defenseType);
        Invoke("CancelDefense", 10);
    }
    public void OnDefense()
    {
        if(playerController.GetDefenseType() == "")
            playerController.ChangeDefenseType("BasicDefense");
        playerController.animTrigger.TriggerAnim("OnDefense", AnimationType.Bool, true);
    }
    public void OffDefense()
    {
        playerController.ChangeDefenseType();
        playerController.animTrigger.TriggerAnim("OnDefense", AnimationType.Bool, false);
    }
    public void CancelDefense()
    {
        playerController.ChangeDefenseType();
    }
    public void Sliding()
    {
        if (playerController.GetPlayerState().CurrentState() != PlayerState.Idle && movementAfterDelay > 0)
            return;
        playerController.PlayOneShotSound("Sliding");
        playerController.movement.AddForce(Vector3.right * playerController.lateLookDir_X.x, 1000);
        playerController.animTrigger.TriggerAnim("SlidingTrigger", AnimationType.Trigger);
        playerController.animTrigger.TriggerAnim("isMove", AnimationType.Bool, false);
        movementAfterDelay = 0.5f;
    }
    public void DashAttack()
    {
        Sliding();
        useDashAttack = true;
        Invoke("StopDashAttack", 0.4f);
    }
    public void StopDashAttack()
    {
        useDashAttack = false;
        playerController.StopCommand();
    }
    public void Attack()
    {
        if (playerController.CanAction(PlayerState.Idle) || playerController.CanAction(PlayerState.Die))
        {
            int layerMask = 1 << LayerMask.NameToLayer("Item");
            RaycastHit2D hit = Physics2D.Raycast(playerController.attackPos.position, Vector2.down, 2f, layerMask);
            if (hit)
            {
                hit.collider.GetComponent<Item>().UseItem();
                playerController.StopCommand();
            }
            else if (playerController.CanRevive())
            {
                playerController.Revive();
            }
            else if(playerController.CanAction(PlayerState.Idle))
            {
                layerMask = 1 << LayerMask.NameToLayer("Enemy");
                float playerAttackRange = playerController.GetStatController().GetStat(StatInfo.AttakRange).Value;
                RaycastHit2D[] hits = Physics2D.BoxCastAll(playerController.attackPos.position, Vector2.one * playerAttackRange, 0, playerController.lookDir_X, playerAttackRange/2+0.5f, layerMask);
                isCritical = GetCritical();
                Debug.Log(playerController.GetPlayerState().CurrentState());
                foreach (RaycastHit2D hitObj in hits)
                {
                    hitEnemyTemp = hitObj.collider.GetComponent<Human>();
                    if (hitEnemyTempList.Contains(hitEnemyTemp))
                        continue;

                    hitEnemyTempList.Add(hitEnemyTemp);
                    //if (hitEnemyTemp.)
                    //    return;
                    float attackDamage = playerController.GetStatController().GetStat(StatInfo.AttackDamage).Value;

                    if(isCritical || playerController.GetCombo() == playerController.maxCombo-1)
                        GiveDamage(attackDamage * 2, hitEnemyTemp, new KnockBackInfo(Vector3.zero, 400, 0.3f, 2));
                    else
                        GiveDamage(attackDamage, hitEnemyTemp, new KnockBackInfo(Vector3.zero, 100, 0.2f, 0.2f));
                    //playerController.SpawnHitEffect(hitEnemyTemp.transform.position);
                }
                playerController.PlayOneShotSound("Swing");
                playerController.Combo();
                attackAE?.Invoke();
                hitEnemyTempList.Clear();
            }
        }
        movementAfterDelay = 0.4f;


    }
    private bool GetCritical()
    {
        int randInt = UnityEngine.Random.Range(0, 10);
        if (randInt == 0)
            return true;
        else
            return false;
    }
    public void GiveDamage(float attackDamage, Human enemy, KnockBackInfo info = null)
    {
        StartCoroutine(AttackCorou(attackDamage, enemy, info));
    }
    IEnumerator AttackCorou(float attackDamage, Human enemy, KnockBackInfo info = null)
    {
        yield return new WaitForSeconds(attackDelayTime);
        enemy.TakeDamage(attackDamage, playerController, info);
        Debug.Log(enemy);
    }
    public void Heal(float value)
    {
        playerController.HealHealth(value);
        playerController.ActiveUpdatePlayerUI();
    }
    public void ShotBullet()
    {
        float attackDamage = playerController.GetStatController().GetStat(StatInfo.AttackDamage).Value*1.2f;
        bulletController.Shot(playerController.lateLookDir_X, 5,attackDamage);
    }
    public void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.CompareTag("Enemy") && useDashAttack)
        {
            float attackDamage = playerController.GetStatController().GetStat(StatInfo.AttackDamage).Value;
            playerController.PlayOneShotSound("SlidingHit");
            coll.GetComponent<Human>().TakeDamage(attackDamage, playerController, new KnockBackInfo(Vector3.zero, 700, 0.3f,3));
            CameraShake.cameraShake.ActiveCameraShake(0.2f);
        }
    }
}
