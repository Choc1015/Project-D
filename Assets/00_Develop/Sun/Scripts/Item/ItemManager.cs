using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemManager : Singleton<ItemManager>
{
    public ItemPrefabInfo[] items;
    private List<ObjectPool<Item>> itemObjPool = new();

    private Item spawnItemTemp;
    protected override void Start()
    {
        foreach(ItemPrefabInfo prefab in items)
        {
            itemObjPool.Add(new ObjectPool<Item>(prefab.prefab, 5, transform));
        }
        base.Start();
    }

    public void SpawnItem(Vector3 pos)
    {
        int randInt = Random.Range(0, 4);

        if (randInt == 0)
        {
            int randomList = Random.Range(0, 100);
            int itemIndex = 0;
            for(int i=0; i< items.Length; i++)
            {
                if (items[i].GetItem(randInt))
                {
                    itemIndex = i;
                    break;
                }
            }
            spawnItemTemp=itemObjPool[itemIndex].SpawnObject();
            spawnItemTemp.transform.position = pos;
            spawnItemTemp.Init(itemIndex);
        }
    }
    public void DespawnItem(int index, Item item)
    {
        itemObjPool[index].DespawnObject(item);
    }
}
[System.Serializable]
public class ItemPrefabInfo
{
    public Item prefab;
    public int minRate,maxRate;
    public bool GetItem(int number)
    {
        if (number >= minRate && number <= maxRate)
            return true;
        return false;
    }
}