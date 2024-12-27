
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
    public virtual void TakeDamage(float attackDamage, Human attackHuman)
    {
        Vector3 dir = (attackHuman.transform.position - transform.position).normalized;
        KnockBack(dir);
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

    protected virtual void KnockBack(Vector3 dir)
    {
        movement.KnockBack(dir, 200);

    }
    protected virtual void Stun(Vector3 dir)
    {
        KnockBack(dir);
    }
}
