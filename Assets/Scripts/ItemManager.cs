using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    [SerializeField] public ShopItemBase itemSO;
    [SerializeField] Button btnItem;
    private void Awake()
    {
        itemSO.RunAwake();

        if(btnItem == null) btnItem = GetComponent<Button>();
        btnItem.onClick.RemoveAllListeners();
        btnItem.onClick.AddListener(TryPurchase);

    }

    private void Update()
    {
        itemSO.RunUpdate();
    }

    public void TryPurchase()
    {
        itemSO.TryPurchase();
    }
}
