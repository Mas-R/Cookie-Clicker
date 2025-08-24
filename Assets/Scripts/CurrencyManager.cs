using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

[DefaultExecutionOrder(-10000)]
public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance { get; private set; }

    public event Action<long> OnCurrencyChanged;

    [SerializeField] private long currency = 0;

    [SerializeField] private int currencyMultChance = 0;
    [SerializeField] private CurrencyPopupSpawner popupSpawner;


    private const string SaveKey = "CURRENCY_V1";
    private bool isFever = false;

    private void Awake()
    {
        // Singleton Instance
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Load save data
        Load();


        // Check Listener
        OnCurrencyChanged?.Invoke(currency);

    }
    private void Start()
    {
        FeverManager.Instance.OnFeverChange += FeverTime;
        popupSpawner = FindAnyObjectByType<CurrencyPopupSpawner>();

    }

    private void FeverTime(bool value)
    {
        isFever = value;
    }

    public void Add(long amount)
    {
        if (amount <= 0) return;
        long totalAmount = 0;
        if (isFever)
        {
            totalAmount = amount * 2;
            currency += totalAmount;
        }
        else if (currencyMultChance > 0)
        {
            int rollDice = UnityEngine.Random.Range(1, 100);

            if (rollDice <= currencyMultChance)
            {
                totalAmount = amount * 2;
                currency += totalAmount;
                QuestManager.Instance.RaiseCharmQuest(true);
            }
            else
            {
                totalAmount = amount;
                currency = currency + totalAmount;
            }
        }
        else
        {
            totalAmount = amount;
            currency = currency + totalAmount;
        }

        popupSpawner?.Spawn(totalAmount);


        OnCurrencyChanged?.Invoke(currency);
    }

    public bool Spend(long amount)
    {
        if (amount <= 0) return true;
        if (currency < amount) return false;
        currency -= amount;
        OnCurrencyChanged?.Invoke(currency);
        return true;
    }

    public void Save()
    {
        PlayerPrefs.SetString(SaveKey, currency.ToString());
        PlayerPrefs.Save();
    }

    public void Load()
    {
        if (PlayerPrefs.HasKey(SaveKey))
        {
            var s = PlayerPrefs.GetString(SaveKey);
            if (long.TryParse(s, out var val))
                currency = val;
        }
    }

    public long GetCurrency()
    {
        return currency;
    }

    public void SetCurrencyMultChance(int multChange)
    {
        currencyMultChance = multChange;
    }
}
