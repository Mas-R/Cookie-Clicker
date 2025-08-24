using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Shop Item/Auto Clicker", fileName = "_Auto_Click")]
public class AutoClickerSO : ShopItemBase
{
    [Header("Auto Config")]
    [SerializeField] float cooldownClick = 1f;
    [SerializeField] bool isActive = false;
    [SerializeField] private int itemAmount;
    [SerializeField] private float lifeTime = 0;
    [SerializeField] private float brokenTime = 300;
    [SerializeField] bool isBroken = false;
    [SerializeField] int repairPrice = 10;
    private float _timer;


    public override void RunAwake()
    {
        itemAmount = Level;
    }

    public override void RunUpdate()
    {

        if (Level <= 0 || !isActive) return;
        lifeTime += Time.deltaTime;

        if (lifeTime > brokenTime)
        {
            isBroken = true;
            isActive = false;
            return;
        }

        _timer += Time.deltaTime;

        while (_timer >= cooldownClick)
        {
            _timer -= cooldownClick;
            TriggerClick();
        
        }
    }

    protected override void OnPurchased()
    {

        if(!isActive) isActive = true;
        itemAmount++;
        if (cooldownClick - ((float)Level / 10f) <= 0.1f)
        {
            cooldownClick = 0.1f;
            return;
        }
        cooldownClick = cooldownClick - ((float)Level / 10f);
    }

    private void TriggerClick()
    {
        CurrencyManager.Instance.Add(itemAmount);
    }

    public override bool CanPurchase()
    {
        
        if (CurrencyManager.Instance == null)
        {
            Debug.Log("CurrencyManager Missing");
            return false;
        }
        if (isBroken && CurrencyManager.Instance.GetCurrency() >= repairPrice)
        {
            return true;
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

    public override bool TryPurchase()
    {

        if (!CanPurchase()) return false;

        if (isBroken)
        {
            CurrencyManager.Instance.Spend(repairPrice);
            isBroken = false;
            isActive = true;
            lifeTime = 0;
            QuestManager.Instance.RaiseRepairQuest(true);
            Debug.Log("item repair successfully");
            return true;
        }

        var cost = GetPrice();

        CurrencyManager.Instance.Spend(GetPrice());

        SetPrice();

        IncreaseLevel();

        OnPurchased();



        Debug.Log("item purchased successfully");
        return true;
    }

    public override bool GetIsBroken()
    {
        return isBroken;
    }

    public override int GetRepairPrice()
    {
        return repairPrice;
    }
}
