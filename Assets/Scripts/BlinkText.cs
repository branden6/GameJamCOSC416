using TMPro;
using UnityEngine;
using System.Collections;

public class BlinkText : MonoBehaviour
{
    public TextMeshProUGUI text;
    public float fadeSpeed = 0.3f;
    public float visibleDuration = 0.5f;
    public float delayBetweenBlinks = 3.5f;

    private void Start()
    {
        StartCoroutine(Blink());
    }

    IEnumerator Blink()
    {
        while (true)
        {
            // Fade In
            yield return StartCoroutine(FadeText(0f, 1f, fadeSpeed));

            // Stay visible briefly
            yield return new WaitForSeconds(visibleDuration);

            // Fade Out
            yield return StartCoroutine(FadeText(1f, 0f, fadeSpeed));

            // Wait before next blink
            yield return new WaitForSeconds(delayBetweenBlinks);
        }
    }

    IEnumerator FadeText(float from, float to, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            float alpha = Mathf.Lerp(from, to, elapsed / duration);
            SetAlpha(alpha);
            elapsed += Time.deltaTime;
            yield return null;
        }
        SetAlpha(to);
    }

    void SetAlpha(float a)
    {
        if (text != null)
        {
            Color c = text.color;
            c.a = a;
            text.color = c;
        }
    }
}
