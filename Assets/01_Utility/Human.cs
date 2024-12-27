
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour
{
    [SerializeField] protected StatController statController;
    public Movement movement;

    public void TakeDamage(float attackDamage)
    {
        if (statController != null)
        {
            statController.GetStat(StatInfo.Health).Value -= attackDamage;
            if (statController.GetStat(StatInfo.Health).Value <= 0)
                Die();
        }
    }
    public virtual void TakeDamage(float attackDamage, Human attackHuman, string setStateName = "")
    {
        Vector3 dir = (transform.position- attackHuman.transform.position).normalized;
        dir.y = 0;
        if (setStateName == "KnockBack")
            KnockBackHuman(dir);
        else if (setStateName == "Stun")
            StunHuman(dir);
        TakeDamage(attackDamage);
    }
    public void HealHealth(float healValue)
    {
        if (statController != null)
        {
            statController.GetStat(StatInfo.Health).Value += healValue;
        }
    }
    public StatController GetStatController() => statController;
    private void Die()
    {
        gameObject.SetActive(false);
    }

    protected virtual void KnockBackHuman(Vector3 dir)
    {
        movement.KnockBack(dir, 200);

    }
    protected virtual void StunHuman(Vector3 dir)
    {
        movement.KnockBack(dir, 500);

    }
}
