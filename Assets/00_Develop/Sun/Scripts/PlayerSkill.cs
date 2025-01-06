using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PlayerSkill : MonoBehaviour
{
    private PlayerController playerController;
    private BulletController bulletController;
    private EnemyStateMachine hitEnemyTemp;
    private bool useDashAttack, isCritical;

    public Action attackAE; // Attack Additional Effects

    void Start()
    {
        playerController = Utility.playerController;
        bulletController = GetComponent<BulletController>();
    }
    public void Move_X(float x)
    {
        if (playerController.CanAction())
        {
            playerController.movement.StopMove();
            float moveSpeed = playerController.GetStatController().GetStat(StatInfo.MoveSpeed).Value;
            playerController.lookDIr_X = Vector3.right * x;
            playerController.movement.MoveToTrans(playerController.lookDIr_X, moveSpeed);
            playerController.animTrigger.TriggerAnim("isMove", AnimationType.Bool, true);
        } 
        
    }
    public void Move_Y(float y)
    {
        if (playerController.CanAction())
        {
            float moveSpeed = playerController.GetStatController().GetStat(StatInfo.MoveSpeed).Value;
            playerController.movement.MoveToTrans(Vector3.up * y, moveSpeed);
            playerController.animTrigger.TriggerAnim("isMove", AnimationType.Bool, true);
        }

        
    }
    public void Jump(float x)
    {
        if (playerController.CanAction())
        {
            playerController.PlayOneShotSound("Jump");
            float moveSpeed = playerController.GetStatController().GetStat(StatInfo.MoveSpeed).Value;
            playerController.movement.MoveToRigid(Vector3.right * x, moveSpeed);
            playerController.animTrigger.TriggerAnim("JumpTrigger", AnimationType.Trigger);
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
        if (playerController.GetPlayerState().CurrentState() != PlayerState.Idle)
            return;
        playerController.PlayOneShotSound("Sliding");
        playerController.movement.AddForce(Vector3.right * playerController.lookDIr_X.x, 1000);
        playerController.animTrigger.TriggerAnim("SlidingTrigger", AnimationType.Trigger);
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
    }
    public void Attack()
    {
        if (playerController.CanAction())
        {
            if(playerController.GetPlayerState().CurrentState() == PlayerState.Die)
            {
                if(playerController.CanRevive())
                    playerController.Revive();
            }
            else
            {
                int layerMask = 1 << LayerMask.NameToLayer("Enemy");

                RaycastHit2D[] hits = Physics2D.BoxCastAll(playerController.attackPos.position, Vector2.one * 1.5f, 0, playerController.lookDIr_X, 1, layerMask);
                isCritical = GetCritical();
                foreach (RaycastHit2D hit in hits)
                {
                    hitEnemyTemp = hit.collider.GetComponent<EnemyStateMachine>();
                    //if (hitEnemyTemp.)
                    //    return;
                    float attackDamage = playerController.GetStatController().GetStat(StatInfo.AttackDamage).Value;

                    
                    CameraShake.cameraShake.ActiveCameraShake(0.01f);
                    if(isCritical)
                    {
                        UIManager.Instance.hitImage.InvokeActiveGO(0.1f);
                        GiveDamage(attackDamage*2, hitEnemyTemp, new KnockBackInfo(Vector3.zero, 200, 0.3f, 2));
                    }
                    else
                        GiveDamage(attackDamage, hitEnemyTemp, new KnockBackInfo(Vector3.zero, 100, 0.1f, 0.2f));
                    playerController.SpawnHitEffect(hitEnemyTemp.transform.position);
                }
                playerController.PlayOneShotSound("Swing");
                playerController.Combo();
                attackAE?.Invoke();

            }
        }

        
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
        enemy.TakeDamage(attackDamage, playerController , info);

    }
    public void Heal(float value)
    {
        playerController.HealHealth(value);
        playerController.ActiveUpdatePlayerUI();
    }
    public void ShotBullet()
    {
        float attackDamage = playerController.GetStatController().GetStat(StatInfo.AttackDamage).Value*1.2f;
        bulletController.Shot(playerController.lookDIr_X, 5,attackDamage);
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
