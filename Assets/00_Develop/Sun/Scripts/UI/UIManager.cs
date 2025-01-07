using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public HitImage hitImage;

    public Image fadeImage;
    public void SetActiveFadeImage(bool isActive, float alpha,float timer)
    {
        fadeImage.gameObject.SetActive(isActive);
        fadeImage.DOFade(alpha, timer);
    }
}
