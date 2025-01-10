using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    private ObjectPool<Effect> objPool;

    public bool isFollow;
    public Vector3 offset;
    private Transform target;
    public void Spawn(ObjectPool<Effect> objPool, Vector3 pos, Transform target)
    {
        if (this.objPool == default)
            this.objPool = objPool;
        transform.position = pos;
        if (isFollow)
            this.target = target;
    }
    void Update()
    {
        if (isFollow)
            transform.position = target.position + offset;
    }
    public void DisableGO()
    {
        objPool.DespawnObject(this);
        gameObject.SetActive(false);
    }
}
