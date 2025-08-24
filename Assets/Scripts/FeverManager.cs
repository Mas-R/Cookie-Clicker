using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-10001)]
public class FeverManager : MonoBehaviour
{
    [SerializeField] private float durationTime = 1f;
    [SerializeField] private float remaining;
    [SerializeField] private bool isFever = false;

    public bool IsFever => isFever;
    private float _timer;
    public event Action<bool> OnFeverChange;

    public static FeverManager Instance { get; private set; }


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
    }

    public bool GetIsFever() {
        return isFever;
    }
    public int GetFeverTime()
    {
        return (int)remaining;
    }
    public void ActivateFever()
    {
        if (isFever) return;

        isFever = true;
        OnFeverChange?.Invoke(isFever);
        StartCoroutine(FeverTimer(durationTime));
    }

    private IEnumerator FeverTimer(float seconds)
    {
        float start = Time.realtimeSinceStartup;
        float end = start + seconds;

        while (true)
        {
            float now =Time.realtimeSinceStartup;
            remaining = Mathf.Max(0f, end - now);
            if (now >= end) break;
            yield return null;
        }

        isFever = false;
        remaining = 0f;
        OnFeverChange?.Invoke(isFever);

    }
}
