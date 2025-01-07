using Photon.Chat.UtilityScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldTxt : MonoBehaviour
{
    private Text txt;

    private void OnEnable()
    {
        CurrencyManager.Instance.updateGold?.Invoke();
    }
    void Awake()
    {
        txt = GetComponent<Text>();
        CurrencyManager.Instance.updateGold += () => txt.text = CurrencyManager.Instance.Gold.ToString();
    }

}
