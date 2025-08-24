using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickableObjectView : MonoBehaviour
{
    [SerializeField] private Image imageObject;
    [SerializeField] private Sprite spriteNormal;
    [SerializeField] private Sprite spritePressed;
    [SerializeField] private ClickableObject clickableObject;

    private void Awake()
    {
        if (!spriteNormal) Debug.Log("Sprite objectNormal missing");
        if (!spritePressed) Debug.Log("Sprite objectPressed missing");
        if (!imageObject) imageObject = GetComponent<Image>();

        if (!clickableObject) clickableObject = GetComponent<ClickableObject>();
    }

    private void OnEnable()
    {
        if (CurrencyManager.Instance != null)
        {
            clickableObject.OnObjectClick += HandleChanged;
        }
        else
        {
            Debug.Log("ClickableObject Missing");
        }
    }

   

    private void OnDisable()
    {
        if (CurrencyManager.Instance != null)
        {
            clickableObject.OnObjectClick -= HandleChanged;
        }
        else
        {
            Debug.Log("ClickableObject Missing");
        }
    }

    private void HandleChanged(bool onClick)
    {
        Debug.Log("Asset Change");
        if (onClick)
        {
            imageObject.sprite = spritePressed;
        }
        else
        {
            imageObject.sprite = spriteNormal;
        }

    }

}
