using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextBox : UIBase
{
    private TMPro.TextMeshProUGUI text;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if(gameObject.activeInHierarchy && Input.anyKeyDown)
        {
            anim.SetTrigger("Disable");
        }
    }
    public void SetTextBox(string contents)
    {
        if(text == null)
            text = GetComponentInChildren<TMPro.TextMeshProUGUI>();
        text.text = contents;
        gameObject.SetActive(true);
    }

}
