using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private BulletController bulletController;
    [SerializeField] private PlayerSkill skills;
    [SerializeField] private float timer;
    public void UseSkill()
    {
        Invoke("InvokeUseSkill", timer);
    }
    private void InvokeUseSkill() => skills.ShotBullet();
}
