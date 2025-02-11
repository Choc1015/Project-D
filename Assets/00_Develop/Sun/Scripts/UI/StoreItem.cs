using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StoreItem : MonoBehaviour
{
    public Item item;
    public float price;
    private Image image;
    private TMPro.TextMeshProUGUI priceTxt;
    private SoundController soundController;

    private bool isDisable;
    void Start()
    {
        soundController= GetComponent<SoundController>();
        GetComponent<Button>().onClick.AddListener(OnClickItem);
        image = transform.Find("Icon").GetComponent<Image>();
        priceTxt = transform.Find("Price").GetComponent<TMPro.TextMeshProUGUI>();
        priceTxt.text = price.ToString();
    }
    void OnEnable()
    {
        isDisable = false;
        image.DOColor(Color.white, 0.01f);
    }
    public void OnClickItem()
    {
        
        if (!isDisable && CurrencyManager.Instance.Gold >= price)
        {
            soundController.PlayOneShotSound("gold");
            isDisable = true;
            item.UseItem();
            image.DOColor(Color.black, 0.1f);
            CurrencyManager.Instance.SpendGold(price);
        }
        
    }
    
}
