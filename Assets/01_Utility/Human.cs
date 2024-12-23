
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour
{
    [SerializeField] protected StatController statController;
    [SerializeField] protected Movement movement;

    public void TakeDamage(float attackDamage)
    {
        if (statController != null)
        {
            statController.GetStat(StatInfo.Health).Value -= attackDamage;
            if (statController.GetStat(StatInfo.Health).Value <= 0)
                Die();
        }
    }
    private void Die()
    {
        Destroy(gameObject);
    }
}
