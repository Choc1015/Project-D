using Photon.Pun;
using Photon.Pun.Demo.PunBasics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon;

public class PlayerUI : UIBase
{
    [SerializeField] private Image hpValue;
    [SerializeField] private Image manaValue;
    public PhotonView pv;
    private void Start()
    {
        StartPhotonUI();
    }
    [PunRPC]
    public void SetValue(StatInfo stat, float maxValue, float curValue)
    {
        if (stat == StatInfo.Health)
            hpValue.fillAmount = curValue / maxValue;
        else if(stat == StatInfo.Mana)
            manaValue.fillAmount = curValue / maxValue;
    }
}