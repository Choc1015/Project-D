using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public HitImage hitImage;
    public TextBox textBox, cutSceneTextBox;

    public Image fadeImage;
    public void SetActiveFadeImage(bool isActive, float alpha,float timer)
    {
        fadeImage.gameObject.SetActive(true);
        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 1-alpha); 
        fadeImage.DOFade(alpha, timer);
        if (!isActive)
            Invoke("DisableFadeImage", timer);
    }
    private void DisableFadeImage() => fadeImage.gameObject.SetActive(false);

    public Image cutSceneImage;
    public void SetCutSceneImage(Sprite sprite)
    {
        cutSceneImage.sprite = sprite;
    }
}
