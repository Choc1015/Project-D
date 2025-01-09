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
        if(gameObject.activeInHierarchy && (Input.GetKeyDown(KeyCode.Space)||Input.GetMouseButtonDown(0)))
        {
            anim.SetTrigger("Disable");
        }
    }
    public void SetTextBox(string contents, Color textColor)
    {
        if(text == null)
            text = GetComponentInChildren<TMPro.TextMeshProUGUI>();
        text.color = textColor;
        text.text = contents;
        gameObject.SetActive(true);
    }

}
