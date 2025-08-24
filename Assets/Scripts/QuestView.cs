using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class QuestView : MonoBehaviour
{
    QuestManager questManager;
    [SerializeField] TextMeshProUGUI quest1;
    [SerializeField] TextMeshProUGUI quest2;
    [SerializeField] TextMeshProUGUI quest3;
    [SerializeField] TextMeshProUGUI questTime;

    int questTimer;

    private void Awake()
    {
        questManager = GetComponent<QuestManager>();
    }

    private void FixedUpdate()
    {
        if (FeverManager.Instance.GetIsFever())
        {
            quest1.text = "Fever Time";
            quest2.text = "All Click 2x";
            quest3.text = "Fever Time";
            questTime.text = FeverManager.Instance.GetFeverTime() + " Second";
        }
        else
        {
            quest1.text = questManager.charmQuest.description;
            quest2.text = questManager.amountQuest.description;
            quest3.text = questManager.repairQuest.description;
            questTime.text = questManager.QuestTime() + " Second";
        }
    }
}
