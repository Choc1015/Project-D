using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Human
{
    [SerializeField] private SkillCommandController skillController;
    private Vector3 lookDIr_X;

    private RaycastHit2D[] hits;
    private int layermask = 0;
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
    }
}
