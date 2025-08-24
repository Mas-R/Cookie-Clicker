using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickAmountQuest : MonoBehaviour
{
    [SerializeField] private int idQuest = 3;
    [SerializeField] private int maxClick = 300;
    [SerializeField] private int totalClick = 0;
    [SerializeField] private bool isComplete;
    [TextArea][SerializeField] public string description = "";

    public void Add(bool value)
    {
        if (isComplete) return;
        totalClick++;

        if (totalClick >= maxClick)
        {
            isComplete = true;
            QuestManager.Instance.ClaimPrize();
            return;
        }
    }

    public void ResetProgress() 
    {
        isComplete = false;
        totalClick = 0;
    }

    private void FixedUpdate()
    {
        description = "Click total " + totalClick + "/" + maxClick;
    }
}
