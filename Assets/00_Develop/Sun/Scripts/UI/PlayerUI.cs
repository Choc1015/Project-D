using Photon.Pun;
using Photon.Pun.Demo.PunBasics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon;

public class PlayerUI : UIBase, IPunObservable
{
    [SerializeField] private Image hpValue;
    [SerializeField] private Image manaValue;
    public PlayerController owner;
    private void Start()
    {
        transform.parent = GameObject.Find("PlayerUI").transform;
        transform.localScale = Vector3.one;
        
    }
    public void SetValue(StatInfo stat, float maxValue, float curValue)
    {
        if (stat == StatInfo.Health)
            hpValue.fillAmount = curValue / maxValue;
        else if(stat == StatInfo.Mana)
            manaValue.fillAmount = curValue / maxValue;
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            
            stream.SendNext(owner);
        }
        else
        {
            owner = (PlayerController)stream.ReceiveNext();
            if (owner.playerUI == null)
                owner.playerUI = this;
        }
    }
}