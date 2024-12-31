using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private ObjectPool<Bullet> bulletObjPool;
    public Bullet bulletPrefab;
    public int createCount;

    private Bullet bulletTemp;
    void Start()
    {
        Transform spawnManager = GameObject.Find("SpawnManager").transform;
        bulletObjPool = new ObjectPool<Bullet>(bulletPrefab, createCount, spawnManager);
        Debug.Log("Spawn");
    }

    public void Shot(Vector3 dir, float speed, float attackDamage)
    {
        bulletTemp = bulletObjPool.SpawnObject();
        bulletTemp.transform.position = transform.position;
        bulletTemp.Shot(dir, speed, attackDamage, this);
        
    }
    public void DespawnBullet(Bullet bullet)
    {
        bulletObjPool.DespawnObject(bullet);
    }
}
