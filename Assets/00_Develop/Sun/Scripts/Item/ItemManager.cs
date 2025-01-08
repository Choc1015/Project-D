using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : Singleton<ItemManager>
{
    public Item[] prefabs;
    private List<ObjectPool<Item>> itemObjPool = new();

    private Item spawnItemTemp;
    void Start()
    {
        foreach(Item prefab in prefabs)
        {
            itemObjPool.Add(new ObjectPool<Item>(prefab, 5, transform));
        }
        
    }

    public void SpawnItem(Vector3 pos)
    {
        int randInt = Random.Range(0, 5);

        if (randInt == 0)
        {
            int randomList = Random.Range(0, itemObjPool.Count);
            spawnItemTemp=itemObjPool[randomList].SpawnObject();
            spawnItemTemp.transform.position = pos;
            spawnItemTemp.Init(randomList);
        }
    }
    public void DespawnItem(int index, Item item)
    {
        itemObjPool[index].DespawnObject(item);
    }
}
