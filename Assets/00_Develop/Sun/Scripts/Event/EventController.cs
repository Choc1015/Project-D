using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EventController : MonoBehaviour
{
    public EventTrigger[] triggers;
    private EventTrigger curTrigger;
    [SerializeField] private int index = -1;
    public GameObject nextController;
    private void Start()
    {
        triggers = transform.GetComponentsInChildren<EventTrigger>();
        NextEvent();
        
        GameManager.Instance.SetGameState(GameState.Stop);
    }
    void Update()
    {
        curTrigger?.Execute(this);
    }

    public void NextEvent()
    {
        curTrigger?.Exit();
        index++;
        if(index < triggers.Length)
        {
            curTrigger = triggers[index];
            curTrigger.Enter();
        }
        else
        {
            curTrigger = null;
            gameObject.SetActive(false);
            if(nextController)
                nextController.SetActive(true);
            GameManager.Instance.SetGameState(GameState.Play);
        }
    }
}
