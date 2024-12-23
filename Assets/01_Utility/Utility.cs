using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility
{
    public static PlayerController playerController;
    public static PlayerController GetPlayer() => playerController;
    public static Transform GetPlayerTr() => playerController.transform;

}
