using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect : MonoBehaviour
{
    private ObjectPool<HitEffect> objPool;
    public void Spawn(ObjectPool<HitEffect> objPool, Vector3 pos)
    {
        if (this.objPool == default)
            this.objPool = objPool;
        transform.position = pos;
    }
    public void DisableGO()
    {
        objPool.DespawnObject(this);
        gameObject.SetActive(false);
    }
}
