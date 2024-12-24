using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    public void Move_X(float x)
    {
        float moveSpeed = Utility.playerController.GetStatController().GetStat(StatInfo.MoveSpeed).Value;
        Utility.playerController.lookDIr_X = Vector3.right * x;
        Utility.playerController.movement.MoveToTrans(Utility.playerController.lookDIr_X, moveSpeed);
        Utility.playerController.animTrigger.TriggerAnim("isMove", AnimationType.Bool, true);
    }
    public void Move_Y(float y)
    {
        float moveSpeed = Utility.playerController.GetStatController().GetStat(StatInfo.MoveSpeed).Value;
        Utility.playerController.movement.MoveToTrans(Vector3.up * y, moveSpeed);
        Utility.playerController.animTrigger.TriggerAnim("isMove", AnimationType.Bool, true);
    }
    public void Jump(float x)
    {
        float moveSpeed = Utility.playerController.GetStatController().GetStat(StatInfo.MoveSpeed).Value;
        Utility.playerController.movement.MoveToRigid(Vector3.right * x, moveSpeed);
        Utility.playerController.animTrigger.TriggerAnim("JumpTrigger", AnimationType.Trigger);
        Debug.Log($"{x }Jump");
    }
    public void Defense()
    {
        Debug.Log("¹æ¾î");
    }
    public void Sliding(float dirX)
    {
        Utility.playerController.movement.AddForce(Vector3.right * dirX, 1000);
        Utility.playerController.animTrigger.TriggerAnim("SlidingTrigger", AnimationType.Trigger);
    }

    public void Attack()
    {
        int layerMask = 1 << LayerMask.NameToLayer("Enemy");

        RaycastHit2D[] hits = Physics2D.BoxCastAll(Utility.playerController.attackPos.position, Vector2.one*1.5f, 0, Utility.playerController.lookDIr_X, 1, layerMask);

        foreach (RaycastHit2D hit in hits)
        {
            float attackDamage = Utility.playerController.GetStatController().GetStat(StatInfo.AttackDamage).Value;
            hit.collider.GetComponent<Human>().TakeDamage(attackDamage);
        }
        //StartCoroutine(ControlDisableTime(statController.GetStat(StatInfo.AttackDelay).Value));

        Debug.Log("Attack");
    }
}
