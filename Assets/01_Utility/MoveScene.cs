using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveScene : MonoBehaviour
{
    public string sceneName;

    private SoundController soundController;

    private void Start()
    {
        soundController = GetComponent<SoundController>();
    }

    public void OnClick_SceneLoad()
    {
        soundController.PlayOneShotSound("Å¬¸¯");
        StartCoroutine(LoadSceneCou());
    }

    IEnumerator LoadSceneCou()
    {
        UIManager.Instance.SetActiveFadeImage(true, 1, 1, Color.black);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(sceneName);
    }
}
