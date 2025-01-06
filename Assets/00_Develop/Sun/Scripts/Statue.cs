using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class Statue : MonoBehaviour
{
    private RaycastHit2D hit;
    private int layerMask;
    public PlayerType playerType;
    void Update()
    {
        
        if (Utility.GetPlayer().playerState.CurrentState() == PlayerState.Die)
        {
            if (layerMask == default)
                layerMask = 1 << LayerMask.NameToLayer("Player");

            hit = Physics2D.BoxCast(transform.position, Vector2.one, 0, Vector2.zero, 0, layerMask);
            Utility.GetPlayer().OnTriggerStatue(hit, playerType);
        }
    }
}
public class ReviveInfo
{
    public PlayerType nextPlayer;
    public bool canRevive;
}
