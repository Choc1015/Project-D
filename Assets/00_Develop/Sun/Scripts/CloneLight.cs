using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CloneLight : MonoBehaviour
{
    [SerializeField] private Light2D spriteLight;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private bool isUpdate;

    private void Update()
    {
        if (isUpdate)
            ChangeSprite();
    }
    public void ChangeSprite()
    {
        spriteLight.lightCookieSprite = sprite.sprite;
        Vector3 l_Dir = Vector3.one;
        l_Dir.y = sprite.flipX ? 180 : 0;
        spriteLight.transform.rotation = Quaternion.Euler(l_Dir);
    }
}
