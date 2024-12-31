using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Pun;
public class ChoicePlayer : MonoBehaviour
{
    public string playerName;
    public void OnClick_Choice()
    {
        PhotonNetwork.Instantiate($"Prefabs/Player/{playerName}", Vector3.zero, Quaternion.identity);
        transform.parent.gameObject.SetActive(false);
    }
}
