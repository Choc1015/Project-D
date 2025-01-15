using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lobby : MonoBehaviour
{
    public GameObject setting;

    private SoundController soundController;

    void Start()
    {
        soundController = GetComponent<SoundController>();
        soundController.PlayLoopSound("로비");
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
        soundController.PlayOneShotSound("클릭");
        setting.SetActive(isActive);
    }
    public void QuitGame()
    {
        soundController.PlayOneShotSound("클릭");
        Application.Quit();
    }
}
