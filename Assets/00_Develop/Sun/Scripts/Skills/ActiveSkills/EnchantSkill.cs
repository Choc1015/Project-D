using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnchantSkill : MonoBehaviour
{
    public PlayerSkill playerSkill;
    protected int layerMask;
    public float value;
    public float despawnTimer;
    public float skillRange;
    public void DetectRange()
    {
        if (!gameObject.activeInHierarchy)
            return;

        RaycastHit2D[] hits = Physics2D.BoxCastAll(Utility.playerController.attackPos.position, Vector2.one * skillRange, 0, Utility.playerController.lookDir_X, 1, layerMask);

        foreach (RaycastHit2D hit in hits)
        {
            UseSkill(hit);
        }
    }
    protected void SetLayerMask(int newLayerMask)
    {
        layerMask = newLayerMask;
    }
    protected virtual void UseSkill(RaycastHit2D hit)
    {
        
    }
    public void DespawnObj()
    {
        gameObject.SetActive(false);
    }
}
