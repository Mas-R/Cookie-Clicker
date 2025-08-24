using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrencyView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textCurr;

    private void Awake()
    {
        if (!textCurr) textCurr = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        if (CurrencyManager.Instance != null)
        {
            CurrencyManager.Instance.OnCurrencyChanged += HandleChanged;
            HandleChanged(CurrencyManager.Instance.GetCurrency());
        }
        else
        {
            Debug.Log("CurrencyManager Missing");
        }
    }

    private void OnDisable()
    {
        if (CurrencyManager.Instance != null)
        {
            CurrencyManager.Instance.OnCurrencyChanged -= HandleChanged;
        }
        else
        {
            Debug.Log("CurrencyManager Missing");
        }
    }

    // Handle if Currency Change
    private void HandleChanged(long value)
    {
        textCurr.text = FormatCompact(value) + " Cookie";

    }

    // Change To suffix
    private static readonly (long threshold, string suffix)[] Steps = new[]
    {
        (1_000_000_000_000_000_000L, "E"),
        (1_000_000_000_000_000L, "P"),
        (1_000_000_000_000L, "T"),
        (1_000_000_000L, "B"),
        (1_000_000L, "M"),
        (1_000L, "K"),
    };

    private static string FormatCompact(long v)
    {
        foreach (var (th, sf) in Steps)
        {
            if (v >= th)
            {
                long whole = v / th;
                return $"{whole}{sf}";
            }
        }
        return v.ToString();
    }
}
