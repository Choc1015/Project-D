using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    private T prefab;
    private T objTemp;
    private Queue<T> objects = new();
    private Transform parent;
    public ObjectPool(T prefab, int createCount, Transform parent)
    {
        this.prefab = prefab;
        this.parent = parent;
        CreatePrefab(createCount, parent);
    }
    public void CreatePrefab(int createCount, Transform parent)
    {
        for(int i = 0; i < createCount; i++)
        {
            objTemp = GameObject.Instantiate(prefab, parent);
            objTemp.gameObject.SetActive(false);
            objects.Enqueue(objTemp);
        }
    }
    public T SpawnObject()
    {
        if(objects.Count > 0)
        {
            objTemp = objects.Dequeue();
            objTemp.gameObject.SetActive(true);
            return objTemp;
        }
        CreatePrefab(1, parent);
        return SpawnObject();
    }
    public void DespawnObject(T obj)
    {
        obj.gameObject.SetActive(false);
        objects.Enqueue(obj);
    }

}
