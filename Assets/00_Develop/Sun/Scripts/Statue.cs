using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class Statue : MonoBehaviour
{
    private RaycastHit2D hit;
    private int layerMask;
    public PlayerType playerType;

    public SpriteRenderer sprite;
    public Sprite[] sprites;
    void Update()
    {
        
        if (Utility.GetPlayer().GetPlayerState().CurrentState() == PlayerState.Die)
        {
            if (layerMask == default)
                layerMask = 1 << LayerMask.NameToLayer("Player");

            hit = Physics2D.BoxCast(transform.position, Vector2.one*3, 0, Vector2.zero, 0, layerMask);
            Utility.GetPlayer().OnTriggerStatue(hit, playerType);
            sprite.sprite = hit ? sprites[1] : sprites[0];
        }
        else if(sprite.sprite == sprites[1])
            sprite.sprite = sprites[0];
    }
}
public class ReviveInfo
{
    public PlayerType nextPlayer;
    public bool canRevive;
}
