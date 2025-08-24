using System.Collections;
using TMPro;
using UnityEngine;

public class CurrencyPopupSpawner : MonoBehaviour
{
    [Header("Assign in Inspector")]
    [SerializeField] private TextMeshProUGUI popupPrefab;     
    [SerializeField] private RectTransform container;         
    [SerializeField] private RectTransform spawnPoint;        

    [Header("Style (opsional)")]
    [SerializeField] private Color normalColor = new Color(1f, 0.95f, 0.4f);
    [SerializeField] private float lifetime = 0.8f;           
    [SerializeField] private float riseDistance = 40f;        


    public void Spawn(long amount, bool isBonus = false)
    {
        if (popupPrefab == null)
        {
            Debug.LogWarning("[CurrencyPopupSpawner] popupPrefab missing.");
            return;
        }

        var parent = container != null ? container : (RectTransform)transform;
        var txt = Instantiate(popupPrefab, parent);
        var rt = txt.rectTransform;


        rt.anchoredPosition = spawnPoint ? spawnPoint.anchoredPosition : Vector2.zero;


        txt.text = $"+{amount:N0}";
        txt.color = isBonus ? Color.green : normalColor;
        txt.alpha = 0f; 

        StartCoroutine(FadeAndRise(txt));
    }

    private IEnumerator FadeAndRise(TextMeshProUGUI t)
    {
        float tElapsed = 0f;
        var rt = t.rectTransform;
        Vector2 start = rt.anchoredPosition;

        while (tElapsed < lifetime)
        {
            tElapsed += Time.unscaledDeltaTime;
            float k = Mathf.Clamp01(tElapsed / lifetime);

            rt.anchoredPosition = start + Vector2.up * (riseDistance * k);

            float fadeIn = 0.2f; 
            float a = k <= fadeIn ? (k / fadeIn)
                                  : (1f - (k - fadeIn) / (1f - fadeIn));
            t.alpha = Mathf.Clamp01(a);

            yield return null;
        }

        Destroy(t.gameObject);
    }
}