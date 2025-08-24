using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-10000)]
public class QuestManager : MonoBehaviour
{
    public TriggerLuckyCharmQuest charmQuest;
    public ClickAmountQuest amountQuest;
    public RepairQuest repairQuest;

    public static QuestManager Instance { get; private set; }

    public event Action<bool> OnCharmQuest;
    public event Action<bool> OnAmountQuest;
    public event Action<bool> OnRepairQuest;

    [SerializeField] private float questTime;
    [SerializeField] private float remainingTime;

    public int QuestTime()
    {
        return (int)remainingTime;
    }

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

        charmQuest = GetComponent<TriggerLuckyCharmQuest>();
        amountQuest = GetComponent<ClickAmountQuest>(); 
        repairQuest = GetComponent<RepairQuest>();


        ActivateQuest(false);
    }

    private void Start()
    {
        FeverManager.Instance.OnFeverChange += ActivateQuest;

        StartCoroutine(QuestTimer(questTime));
    }

    public void RaiseCharmQuest(bool state) => OnCharmQuest?.Invoke(state);
    public void RaiseAmountQuest(bool state) => OnAmountQuest?.Invoke(state);
    public void RaiseRepairQuest(bool state) => OnRepairQuest?.Invoke(state);

    public void ClaimPrize()
    {

        ResetAllQuest();
        FeverManager.Instance.ActivateFever();
        remainingTime = 0f;

    }

    public void ActivateQuest(bool onFeverTime)
    {
        if (!onFeverTime)
        {
            OnCharmQuest += charmQuest.Add;
            OnAmountQuest += amountQuest.Add;
            OnRepairQuest += repairQuest.Add;

            StartCoroutine(QuestTimer(questTime));

        }
    }


    public void ResetAllQuest()
    {
        OnCharmQuest -= charmQuest.Add;
        OnAmountQuest -= amountQuest.Add;
        OnRepairQuest -= repairQuest.Add;

        amountQuest.ResetProgress();
        repairQuest.ResetProgress();
        charmQuest.ResetProgress();


    }

    private IEnumerator QuestTimer(float seconds)
    {
        float start = Time.realtimeSinceStartup;
        float end = start + seconds;

        while (true)
        {
            float now = Time.realtimeSinceStartup;
            remainingTime = Mathf.Max(0f, end - now);
            if (now >= end) break;
            if (FeverManager.Instance.IsFever) break;
            yield return null;
        }
        if (!FeverManager.Instance.IsFever)
        {
            remainingTime = 0f;
            ResetAllQuest();
            ActivateQuest(false);
        }

    }
}
