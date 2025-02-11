using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    
    [Header("Hit Image")]
    public HitImage hitImage;

    [Space(30)]
    [Header("TextBox Image")]
    public TextBox textBox, cutSceneTextBox;

    [Space(30)]
    [Header("Fade Image")]
    public Image fadeImage;

    [Space(30)]
    [Header("Player UI")]
    public PlayerUI playerUIPrefab;
    private PlayerUI playerUI;

    [Space(30)]
    [Header("CutScene Image")]
    public Image cutSceneImage;

    [Space(30)]
    [Header("Store")]
    public GameObject store;

    [Space(30)]
    [Header("Options")]
    public OptionController optionController;

    [Space(30)]
    [Header("BossHPBar")]
    public BossHealthBar bossHealthBar;

    [Space(30)]
    [Header("Settimg")]
    public GameObject settingUI;

    protected override void Awake()
    {
        playerUIPrefab = Resources.Load<PlayerUI>("Prefabs/UI/PlayerUI");
        base.Awake();
    }

    public PlayerUI GetPlayerUI()
    {
        if (playerUI == null)
            playerUI = Instantiate(playerUIPrefab, transform);
        playerUI.GetComponent<RectTransform>().SetAsFirstSibling();
        return playerUI;
    }
    public void SetActiveFadeImage(bool isActive, float alpha, float timer, Color color)
    {
        fadeImage.gameObject.SetActive(true);
        fadeImage.color = new Color(color.r, color.g, color.b, 1 - alpha);
        fadeImage.DOFade(alpha, timer);
        if (!isActive)
            Invoke("DisableFadeImage", timer);
    }
    private void DisableFadeImage() => fadeImage.gameObject.SetActive(false);

    public void SetCutSceneImage(Sprite sprite)
    {
        cutSceneImage.sprite = sprite;
    }
    public SkillSwap SpawnSkillSwapUI(SkillSwap skillSwap)
    {
        return Instantiate(skillSwap, transform);

    }
    public void ActiveStoreUI()
    {
        store.SetActive(true);
        
    }
    public void DisaableStoreUI()
    {
        store.SetActive(false);
    }
}
