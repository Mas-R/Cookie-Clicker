using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerLuckyCharmQuest : MonoBehaviour
{
    [SerializeField] private int idQuest = 3;
    [SerializeField] private int maxTrigger = 300;
    [SerializeField] private int totalTrigger = 0;
    [SerializeField] private bool isComplete;
    [TextArea][SerializeField] public string description = "";

    public void Add(bool value)
    {
        if (isComplete) return;
        totalTrigger++;

        if (totalTrigger >= maxTrigger)
        {
            isComplete = true;
            QuestManager.Instance.ClaimPrize();
            return;
        }
    }

    public void ResetProgress()
    {
        isComplete = false;
        totalTrigger = 0;
    }

    private void FixedUpdate()
    {
        description = "Trigger Sugar " + totalTrigger + "/" + maxTrigger;
    }
}
