using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lobby : MonoBehaviour
{
    public GameObject setting;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (setting.activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                SetActiveSetting(false);
        }
    }

    public void SetActiveSetting(bool isActive)
    {
        setting.SetActive(isActive);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
