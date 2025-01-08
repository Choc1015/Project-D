using Photon.Chat.UtilityScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetLayer : MonoBehaviour
{
    void Start()
    {
        GameManager.Instance.SetLayerPosition += SetLayerPosition;
    }
    public void SetLayerPosition()
    {
        Vector3 pos = transform.position;
        pos.z = transform.position.y;
        transform.position = pos;
    }
    public void DestroySetLayer()
    {
        GameManager.Instance.SetLayerPosition -= SetLayerPosition;
    }
}
