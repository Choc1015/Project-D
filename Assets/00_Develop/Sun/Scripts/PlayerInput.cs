using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Vector3 InputMove()
    {
        Vector3 dir = Vector3.zero;

        if (Input.GetKey(KeyCode.LeftArrow))
            dir.x -= 1;
        else if (Input.GetKey(KeyCode.RightArrow))
            dir.x += 1;

        if (Input.GetKey(KeyCode.UpArrow))
            dir.y += 1;
        else if (Input.GetKey(KeyCode.DownArrow))
            dir.y -= 1;

        return dir;
    }
    public bool InputJump() => Input.GetKeyDown(KeyCode.D);

    public bool InputAttack() => Input.GetKeyDown(KeyCode.A);
    public bool InputSkill() => Input.GetKeyDown(KeyCode.S);
}
