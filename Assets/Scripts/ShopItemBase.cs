using System;
using UnityEngine;

public abstract class ShopItemBase : ScriptableObject
{
    [Header("Identity")]
    [SerializeField] private int id = 1;
    [SerializeField] private string displayName = "New Item";
    [TextArea][SerializeField] private string description = "";
    [SerializeField] private ShopItemType itemType = ShopItemType.Auto;
    [SerializeField] private Sprite icon;
   
    [Header("Economy")]
    [Min(0)][SerializeField] private long basePrice = 10;
    [Min(1f)][SerializeField] private double costMultiplier = 1.5f;
    [Tooltip("-1 = Whiout maximum level")]
    [SerializeField] private int level = 0;
    [SerializeField] protected int maxLevel = 20;

    public int Id => id;
    public string DisplayName => displayName;
    public string Description => description;
    public ShopItemType ItemType => itemType;
    public Sprite Icon => icon;
    public int MaxLevel => maxLevel;
    public int Level => level;

    
    public virtual void RunAwake()
    {
        return;
    }

    public virtual void RunUpdate()
    {
        return;
    }
    public virtual long GetPrice()
    {
        return basePrice;
    }
    public virtual bool GetIsBroken()
    {
        return false;
    }
    public virtual int GetRepairPrice()
    {
        return 0;
    }
    protected virtual void SetPrice()
    {
        double p = basePrice * costMultiplier;
        if (p <= basePrice) p = basePrice + 1;
        basePrice = (long)Math.Round(p);
    }

    public virtual bool CanPurchase()
    {
        if (CurrencyManager.Instance == null)
        {
            Debug.Log("CurrencyManager Missing");
            return false;
        }
        if (maxLevel >= 0 && Level >= maxLevel)
        {
            Debug.Log("Level Max");
            return false;
        }
        if (CurrencyManager.Instance.GetCurrency() < GetPrice())
        {
            Debug.Log("Not Enough Money");
            return false;
        }
        return true;
    }

    public virtual bool TryPurchase()
    {
        
        if (!CanPurchase()) return false;

        var cost = GetPrice();

        CurrencyManager.Instance.Spend(GetPrice());

        SetPrice();
        
        IncreaseLevel();

        OnPurchased();



        Debug.Log("item purchased successfully");
        return true;
    }

    protected void IncreaseLevel()
    {
        level++;
    }
    /// Effect Purchased Item
    protected abstract void OnPurchased();
}
