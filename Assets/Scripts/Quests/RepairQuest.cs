using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairQuest : MonoBehaviour
{
    [SerializeField] private int idQuest = 2;
    [SerializeField] private int maxRepair = 3;
    [SerializeField] private int totalRepair;
    [SerializeField] private bool isComplete;
    [TextArea][SerializeField] public string description = "";

    public void Add(bool value)
    {
        if (isComplete) return;
        totalRepair++;

        if (totalRepair >= maxRepair)
        {
            isComplete = true;
            QuestManager.Instance.ClaimPrize();
            return;
        }
    }

    public void ResetProgress()
    {
        isComplete = false;
        totalRepair = 0;
    }

    private void FixedUpdate()
    {
        description = "Repair " + totalRepair + "/" + maxRepair;
    }
}
