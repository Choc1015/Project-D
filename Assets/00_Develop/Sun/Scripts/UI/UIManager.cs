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

    private void Awake()
    {
        //playerUI = Resources.Load<PlayerUI>("Prefabs/UI/PlayerUI");
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
}
