using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBase : MonoBehaviour
{
    public string parentName;
    public void StartPhotonUI()
    {
        transform.parent = GameObject.Find(parentName).transform;
        transform.localScale = Vector3.one;
    }
    public void DisableGO()
    {
        gameObject.SetActive(false);
    }
}
