using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Human
{
    [SerializeField] private SkillCommandController skillController;

    public Transform attackPos;
    public SpriteRenderer sprite;
    public Animator spriteAnime;

    public Vector3 lookDIr_X;

    //private bool isJumpInput, isJump;
    //private float jumpStartPoint;

    void Start()
    {
        statController.Init();
        Utility.playerController = this;
    }

    void Update()
    {
        skillController.ControllerAction();
        sprite.flipX = lookDIr_X.x == -1 ? true : false; 
    }
}
