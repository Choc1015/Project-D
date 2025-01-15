using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionUI : UIBase
{
    public OptionData data;
    public Button btn;
    public TextMeshProUGUI text;
    public NextStage nextStage;
    public int currentStage;
    private GameObject cutScene;
    private SoundController soundController;
    private void Start()
    {
        btn.onClick.AddListener(() => OnClick_Btn());
        soundController = GetComponent<SoundController>();
    }
    public void Init(OptionData data, NextStage nextStage, int currentStage)
    {
        this.nextStage = nextStage;
        this.data = data;
        text.text = data.contents;
        this.currentStage = currentStage;
        this.cutScene = data.cutScene;
        gameObject.SetActive(true);
    }
    public void OnClick_Btn()
    {
        nextStage.GoNextStage(this, cutScene);
    }
}
