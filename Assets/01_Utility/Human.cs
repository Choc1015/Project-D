
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StatController), typeof(Movement))]
public class Human : MonoBehaviour
{
    [SerializeField] protected StatController statController;
    public Movement movement;
    protected KnockBackInfo info;
    public bool useKnockBack = true;
    public bool isObject = false;
    public void TakeDamage(float attackDamage)
    {
        if (statController != null)
        {
            CameraShake.cameraShake.ActiveCameraShake(0.02f);
            statController.GetStat(StatInfo.Health).Value -= attackDamage;
            if (statController.GetStat(StatInfo.Health).Value <= 0)
                DieHuman();
        }
    }

    public virtual void TakeDamage(float attackDamage, Human attackHuman, KnockBackInfo info=null)
    {
        TakeDamage(attackDamage);

        if (info != null && statController.GetStat(StatInfo.Health).Value > 0)
        {
            this.info = info;
            info.dir = transform.position - attackHuman.transform.position;
            info.dir.y = 0;
            if(useKnockBack)
                movement.KnockBack(info);
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
    protected virtual void DieHuman()
    {
        gameObject.SetActive(false);
    }

    public virtual void Revive()
    {

    }

}
