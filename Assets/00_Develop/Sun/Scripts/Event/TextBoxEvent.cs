using UnityEngine;

public class TextBoxEvent : EventTrigger
{
    public string contents;
    public Transform posObject;
    public Vector3 basePos;
    public Vector3 offset;
    public bool isCutScene;
    private TextBox tbTemp;
    public Color textColor;
    public override void Enter()
    {
        if (isCutScene)
        {
            tbTemp = UIManager.Instance.cutSceneTextBox;
        }
        else
        {
            tbTemp = UIManager.Instance.textBox;
            UIManager.Instance.textBox.transform.position = Camera.main.WorldToScreenPoint(Camera.main.transform.position+basePos + offset);
            //UIManager.Instance.textBox.transform.position = basePos + offset;
        }

        tbTemp.SetTextBox(contents, textColor);

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
