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
   
    private bool isDisable;
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClickItem);
        image = transform.Find("Icon").GetComponent<Image>();
        priceTxt = transform.Find("Price").GetComponent<TMPro.TextMeshProUGUI>();
        priceTxt.text = price.ToString();
    }
    public void OnClickItem()
    {
        if (!isDisable && CurrencyManager.Instance.Gold >= price)
        {
            isDisable = true;
            item.UseItem();
            image.DOColor(Color.black, 0.1f);
            CurrencyManager.Instance.SpendGold(price);
        }
        
    }
}
