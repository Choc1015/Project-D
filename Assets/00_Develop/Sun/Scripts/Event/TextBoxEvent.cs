using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextBoxEvent : EventTrigger
{
    public string contents;
    public Transform posObject;
    public Vector3 basePos;
    public Vector3 offset;
    public bool isCutScene;
    private TextBox tbTemp;
    private Color colorTemp;
    public override void Enter()
    {
        if (isCutScene)
        {
            tbTemp = UIManager.Instance.cutSceneTextBox;
            colorTemp = Color.white;
        }
        else
        {
            tbTemp = UIManager.Instance.textBox;
            colorTemp = Color.black;
            UIManager.Instance.textBox.transform.position = Camera.main.WorldToScreenPoint(basePos + offset);
            //UIManager.Instance.textBox.transform.position = basePos + offset;
        }

        tbTemp.SetTextBox(contents, colorTemp);

        if (posObject != default)
        {
            basePos = transform.position;
        }
        
    }
    public override void Execute(EventController eventController)
    {
        

        if (!tbTemp.gameObject.activeInHierarchy)
        {
            eventController.NextEvent();
        }
    }
    public override void Exit()
    {
        UIManager.Instance.textBox.transform.position = Camera.main.ScreenToWorldPoint(Vector3.zero);
    }
}
