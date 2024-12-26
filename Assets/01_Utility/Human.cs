
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
        Destroy(gameObject);
    }
}
