using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeStop : MonoBehaviour
{
    [SerializeField] private PlayerSkill playerSkill;
    public void UseSkill()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject enemy in enemies)
        {
            playerSkill.GiveDamage(5, enemy.GetComponent<EnemyStateMachine>(), new KnockBackInfo(Vector3.zero, 100, 0.1f, 10f), true);
            
        }
        gameObject.SetActive(true);
        Invoke("InvokeDisable", 3);
    }

    public void InvokeDisable()
    {
        gameObject.SetActive(false);
    }
}
