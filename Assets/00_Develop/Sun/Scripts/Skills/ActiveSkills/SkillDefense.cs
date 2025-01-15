using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDefense : MonoBehaviour
{
    public string defenseType;
    public float value;
    
    public void UseSkill()
    {
        Utility.GetPlayer().ChangeDefenseType(defenseType, value);
        Utility.GetPlayer().shieldImage.gameObject.SetActive(true);
        Invoke("DisableSkill", 10);
    }

    private void DisableSkill()
    {
        Utility.GetPlayer().ResetDefenseType();
        gameObject.SetActive(false);
        Utility.GetPlayer().shieldImage.gameObject.SetActive(false);
    }
}
