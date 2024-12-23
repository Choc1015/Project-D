using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Vector3 InputMove() => (Vector3.up * Input.GetAxisRaw("Vertical")) + (Vector3.right*Input.GetAxisRaw("Horizontal"));
    public bool InputJump() => Input.GetKeyDown(KeyCode.Space);
}
