using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public BulletInfo[] bullets;
    private Dictionary<BulletKind, BulletInfo> bulletDatas = new Dictionary<BulletKind, BulletInfo>();


    private Bullet bulletTemp;
    void Start()
    {
        Transform spawnManager = GameObject.Find("SpawnManager").transform;
        foreach(BulletInfo info in bullets)
        {
            info.bulletObjPool = new ObjectPool<Bullet>(info.bulletPrefab, info.createCount, spawnManager);
            bulletDatas.Add(info.bulletKind, info);
        }

    }

    public void Shot(BulletKind kind ,Vector3 dir, Vector3 pos, float speed, float attackDamage)
    {
        bulletTemp = bulletDatas[kind].bulletObjPool.SpawnObject();
        bulletTemp.transform.position = transform.position + pos;
        bulletTemp.Shot(kind,dir, speed, attackDamage, this);
        
    }
    public void DespawnBullet(BulletKind kind,Bullet bullet)
    {
        bulletDatas[kind].bulletObjPool.DespawnObject(bullet);
    }
}
[System.Serializable]
public class BulletInfo
{
    public BulletKind bulletKind;
    public ObjectPool<Bullet> bulletObjPool;
    public Bullet bulletPrefab;
    public int createCount;

}
