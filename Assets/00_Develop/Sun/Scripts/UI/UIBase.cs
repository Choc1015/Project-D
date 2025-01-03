using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBase : MonoBehaviour
{
    public string parentName;
    private void Start()
    {
        transform.parent = GameObject.Find(parentName).transform;
        transform.localScale = Vector3.one;
    }
    public void DisableGO()
    {
        gameObject.SetActive(false);
    }
}
