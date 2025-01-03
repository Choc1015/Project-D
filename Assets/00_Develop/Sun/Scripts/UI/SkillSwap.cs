using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using Photon;
using Photon.Pun;
public class SkillSwap : UIBase
{
    public PhotonView pv;
    [SerializeField] private List<Transform> skillImages= new();
    [SerializeField] private Vector3[] pos;
    [SerializeField] private SkillFunctionController skill;
    
    private void Update()
    {
        transform.position = Camera.main.WorldToScreenPoint(Utility.GetPlayerTr().position);
        if (gameObject.activeInHierarchy&&(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.Q)))
        {
            skill.SetAllDisable(0.2f);
            DisableSkillSwap();
            pv.RPC("DisableSkillSwap", RpcTarget.All);
        }
    }
    public void Init(SkillFunctionController skill)
    {
        this.skill = skill;
        gameObject.SetActive(false);
    }
    [PunRPC]
    public void ActiveSkillSwap()
    {
        if (!gameObject.activeInHierarchy)
        {
            gameObject.SetActive(true);
            ActiveAnimation();
            transform.position = Camera.main.WorldToScreenPoint(Utility.GetPlayerTr().position);
        }
        else
            Swap();
    }
    [PunRPC]
    public void DisableSkillSwap()
    {
        Invoke("DisableGO", 0.05f);
        DisableAnimation();
    }
    public void Swap()
    {
        Transform temp = skillImages[0];
        for(int i=0; i< skillImages.Count-1; i++)
        {
            SettingSkillImage(i, skillImages[i + 1]);
        }
        SettingSkillImage(skillImages.Count - 1, temp);
        skill.ChangeSkill();
    }
    private void SettingSkillImage(int index,Transform t)
    {
        skillImages[index] = t;      
        skillImages[index].DOLocalMove(pos[index], 0.2f, true);
    }
    private void ActiveAnimation()
    {
        for (int i = 0; i < skillImages.Count; i++)
        {
            skillImages[i].DOLocalMove(pos[i], 0.05f, true);
        }
    }
    private void DisableAnimation()
    {
        for (int i = 0; i < skillImages.Count; i++)
        {
            skillImages[i].DOLocalMove(Vector3.zero, 0.05f, true);
        }
    }
}
