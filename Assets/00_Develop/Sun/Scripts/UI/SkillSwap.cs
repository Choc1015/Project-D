using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SkillSwap : MonoBehaviour
{
    [SerializeField] private List<Transform> skillImages= new();
    [SerializeField] private Vector3[] pos;
    [SerializeField] private SkillFunctionController skill;
    
    private void Update()
    {
        transform.position = Camera.main.WorldToScreenPoint(Utility.GetPlayerTr().position);
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.Q))
        {
            skill.SetAllDisable();
            DisableSkillSwap();
        }
    }
    public void Init(SkillFunctionController skill)
    {
        this.skill = skill;
    }
    public void ActiveSkillSwap()
    {
        if (!gameObject.activeInHierarchy)
        {
            gameObject.SetActive(true);
            transform.position = Camera.main.WorldToScreenPoint(Utility.GetPlayerTr().position);
        }
        else
            Swap();
    }
    public void DisableSkillSwap()
    {
        if(gameObject.activeInHierarchy)
            gameObject.SetActive(false);
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

}
