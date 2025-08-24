using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Shop Item/LuckyCharm", fileName = "_Lucky_Charm")]
public class LuckyCharmSO : ShopItemBase
{
    [Header("Lucky Charm Config")]
    [SerializeField] public int itemValue = 0;
    [SerializeField] bool isActive = false;

    protected override void OnPurchased()
    {
        if (!isActive) isActive = true;
        itemValue = Level * 10;
        CurrencyManager.Instance.SetCurrencyMultChance(itemValue);
    }
}
