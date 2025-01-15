using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemKind { Buff, Heal };
public class Item : MonoBehaviour
{
    [SerializeField] private ItemKind kind;
    [SerializeField] private StatInfo statInfo;
    public float value;
    private int index;
    public SoundController controller;
    public void Init(int index)
    {
        this.index = index;
    }
    public virtual void UseItem()
    {
        if(kind == ItemKind.Buff)
            Utility.GetPlayerStat().BuffStat(statInfo, value);
        else
            Utility.GetPlayer().Heal(statInfo, value);
        if (controller == null)
            controller = GetComponent<SoundController>();
        controller.PlayOneShotSound("GetItem");
        Despawn();
    }
    public void Despawn()
    {
        ItemManager.Instance.DespawnItem(index, this);
    }
}
