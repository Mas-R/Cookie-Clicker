using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemView : MonoBehaviour
{
    [SerializeField] private ItemManager itemManager;
    [SerializeField] TextMeshProUGUI itemName;
    [SerializeField] TextMeshProUGUI itemDescription;
    [SerializeField] TextMeshProUGUI itemPrice;
    [SerializeField] Image itemSprite;

    private void Awake()
    {
        itemManager = GetComponentInChildren<ItemManager>();
    }

    private void FixedUpdate()
    {
        itemSprite.sprite = itemManager.itemSO.Icon;
        itemName.text = itemManager.itemSO.DisplayName;
        if (itemManager.itemSO.Level < 1)
        {
            itemDescription.text = "Item Lock";
        }
        else if (itemManager.itemSO.ItemType == ShopItemType.SupportItem)
        {
            itemDescription.text = itemManager.itemSO.Description + " " + itemManager.itemSO.Level + " * 0.01";
        }
        else if (itemManager.itemSO.GetIsBroken())
        {
            itemDescription.text = "Item Broken, Fix to continue";
        }
        else
        {
            itemDescription.text = itemManager.itemSO.Description + " +" + itemManager.itemSO.Level;
        }

        if (itemManager.itemSO.GetIsBroken())
        {
            itemPrice.text = itemManager.itemSO.GetRepairPrice() +" Cookie";
        }
        else if(itemManager.itemSO.Level < itemManager.itemSO.MaxLevel)
        {
            itemPrice.text = "Upgrade " + itemManager.itemSO.GetPrice() + " Cookie";
        }
        else
        {
            itemPrice.text = "Max Level";
        }
    }
}
