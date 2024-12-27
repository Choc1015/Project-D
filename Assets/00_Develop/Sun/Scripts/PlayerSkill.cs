using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    private PlayerController playerController;
    private bool useDashAttack;
    void Start()
    {
        playerController = Utility.playerController;
    }
    public void Move_X(float x)
    {
        if (playerController.playerState.CurrentState() != PlayerState.Idle)
            return;

        float moveSpeed = playerController.GetStatController().GetStat(StatInfo.MoveSpeed).Value;
        playerController.lookDIr_X = Vector3.right * x;
        playerController.movement.MoveToTrans(playerController.lookDIr_X, moveSpeed);
        playerController.animTrigger.TriggerAnim("isMove", AnimationType.Bool, true);
        Debug.Log("X");
    }
    public void Move_Y(float y)
    {
        if (playerController.playerState.CurrentState() != PlayerState.Idle)
            return;

        float moveSpeed = playerController.GetStatController().GetStat(StatInfo.MoveSpeed).Value;
        playerController.movement.MoveToTrans(Vector3.up * y, moveSpeed);
        playerController.animTrigger.TriggerAnim("isMove", AnimationType.Bool, true);
        Debug.Log("Y");
    }
    public void Jump(float x)
    {
        if (playerController.playerState.CurrentState() != PlayerState.Idle)
            return;

        float moveSpeed = playerController.GetStatController().GetStat(StatInfo.MoveSpeed).Value;
        playerController.movement.MoveToRigid(Vector3.right * x, moveSpeed);
        playerController.animTrigger.TriggerAnim("JumpTrigger", AnimationType.Trigger);
        Debug.Log($"{x }Jump");
    }
    public void Defense(string defenseType)
    {
        if (playerController.playerState.CurrentState() != PlayerState.Idle)
            return;

        playerController.ChangeDefenseType(defenseType);
    }
    public void CancelDefense()
    {
        playerController.ChangeDefenseType();
    }
    public void Sliding()
    {
        if (playerController.playerState.CurrentState() != PlayerState.Idle)
            return;

        playerController.movement.AddForce(Vector3.right * playerController.lookDIr_X.x, 1000);
        playerController.animTrigger.TriggerAnim("SlidingTrigger", AnimationType.Trigger);
    }
    public void DashAttack()
    {
        Sliding();
        useDashAttack = true;
    }
    public void StopDashAttack()
    {
        useDashAttack = false;
    }
    public void Attack()
    {
        if (playerController.playerState.CurrentState() != PlayerState.Idle)
            return;

        int layerMask = 1 << LayerMask.NameToLayer("Enemy");

        RaycastHit2D[] hits = Physics2D.BoxCastAll(playerController.attackPos.position, Vector2.one*1.5f, 0, playerController.lookDIr_X, 1, layerMask);

        foreach (RaycastHit2D hit in hits)
        {
            float attackDamage = playerController.GetStatController().GetStat(StatInfo.AttackDamage).Value;
            GiveDamage(attackDamage, hit.collider.GetComponent<Human>());
        }
    }
    public void GiveDamage(float attackDamage, Human enemy)
    {
        enemy.TakeDamage(attackDamage);
    }
    public void Heal(float value)
    {
        playerController.HealHealth(value);
    }

    public void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.CompareTag("Enemy") && useDashAttack)
        {
            coll.GetComponent<Human>().TakeDamage(playerController.GetStatController().GetStat(StatInfo.AttackDamage).Value);
        }
    }
}
