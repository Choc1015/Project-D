using Photon.Chat.UtilityScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldTxt : MonoBehaviour
{
    private TMPro.TextMeshProUGUI txt;

    private void OnEnable()
    {
        CurrencyManager.Instance.updateGold?.Invoke();
    }
    void Awake()
    {
        txt = GetComponent<TMPro.TextMeshProUGUI>();
        CurrencyManager.Instance.updateGold += () => txt.text = CurrencyManager.Instance.Gold.ToString();
    }

}
