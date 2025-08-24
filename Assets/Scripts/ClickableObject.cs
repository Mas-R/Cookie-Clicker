using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[DefaultExecutionOrder(-90000)]
public class ClickableObject : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public event Action<bool> OnObjectClick;
    private bool OnClick = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (CurrencyManager.Instance == null) return;
        if (GameManager.Instance.GetAmountPerClick() <= 0) return;

        CurrencyManager.Instance.Add(GameManager.Instance.GetAmountPerClick());
        OnClick = true;
        OnObjectClick?.Invoke(OnClick);
        QuestManager.Instance.RaiseAmountQuest(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnClick = false;
        OnObjectClick?.Invoke(OnClick);

    }
}
