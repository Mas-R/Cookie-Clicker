using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Shop Item/Manual Clicker", fileName = "_Manual_Click")]
public class ManualClickerSO : ShopItemBase
{
    [Header("Manual Config")]
    [SerializeField] private int itemAmount = 1;

    public override void RunAwake()
    {
        itemAmount = Level;
        
        //Set Default Amount Per Click
        GameManager.Instance.SetAmountPerClick(itemAmount);
    }

    protected override void OnPurchased()
    {
        itemAmount++;
        GameManager.Instance.SetAmountPerClick(itemAmount);
    }
}
