using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private BulletKind kind;
    [SerializeField] private BulletController bulletController;
    [SerializeField] private PlayerSkill skills;
    [SerializeField] private float timer;
    [SerializeField] private int spawnCount;
    public Vector3 spawnOffset;
    public void UseSkill()
    {
        Invoke("InvokeUseSkill", timer);
    }
    private void InvokeUseSkill() => skills.ShotBullet(kind, spawnOffset, spawnCount);

}
