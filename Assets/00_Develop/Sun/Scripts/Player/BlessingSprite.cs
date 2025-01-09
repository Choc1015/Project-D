using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlessingSprite : MonoBehaviour
{
    public SpriteRenderer spriteTarget, thisSprite;
    void Update()
    {
        thisSprite.flipX = spriteTarget.flipX;

    }
}
