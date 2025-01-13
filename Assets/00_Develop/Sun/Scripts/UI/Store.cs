using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Store : UIBase
{
    public void DisableStore()
    {
        StartCoroutine(DisableStoreCou());
    }
    IEnumerator DisableStoreCou()
    {
        UIManager.Instance.SetActiveFadeImage(true, 1, 1, Color.black);
        yield return new WaitForSeconds(1.5f);
        gameObject.SetActive(false);
        UIManager.Instance.SetActiveFadeImage(false, 0, 1, Color.black);
        yield return new WaitForSeconds(1f);
        GameManager.Instance.SetGameState(GameState.Play);

    }
}
