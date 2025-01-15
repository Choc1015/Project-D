using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstallationSkill : MonoBehaviour
{
    public Transform parent;
    protected Vector3 spawnPoint;
    public float value;
    public float despawnTimer;
    public float skillRange;
    public float skillDelayTime;
    public Transform obj;
    void OnEnable()
    {
        SpawnObj(parent.position);
    }
    protected void SpawnObj(Vector3 spawnPoint)
    {
        transform.position = spawnPoint;
        transform.parent = null;
        Invoke("DespawnObj", despawnTimer);
        Invoke("UseSkill", skillDelayTime);
    }
    public void DespawnObj()
    {
        transform.parent = parent;
        gameObject.SetActive(false);
        if (obj)
            obj.localScale = Vector3.zero;
        CancelInvoke();
    }
    public virtual void UseSkill()
    {
        //Invoke("UseSkill", skillDelayTime);
    }
}
