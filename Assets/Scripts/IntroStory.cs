using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class IntroStory : MonoBehaviour
{
    public TextMeshProUGUI[] slides;
    public float fadeDuration = 1f;
    public float displayDuration = 2.5f;

    public GameObject blackBackground;
    public GameObject dkReveal;
    public DKTextTypewriter dkTypewriter;

    private void Start()
    {
        AudioManager.Instance.musicSource.Pause();
        AudioManager.Instance.backgroundSource.Pause();
        foreach (var text in slides)
        {
            SetAlpha(text, 0f);
        }

        dkReveal.SetActive(false);
        StartCoroutine(PlayIntroSlides());
    }

    IEnumerator PlayIntroSlides()
    {
        foreach (TextMeshProUGUI text in slides)
        {
            yield return StartCoroutine(FadeText(text, 0f, 1f, fadeDuration));
            yield return new WaitForSeconds(displayDuration);
            yield return StartCoroutine(FadeText(text, 1f, 0f, fadeDuration));
        }

        // Wait a second for dramatic pause
        yield return new WaitForSeconds(0.1f);

        blackBackground.SetActive(false);

        // REVEAL THE DK SPHERE!
        dkReveal.SetActive(true);

        // Start DK taunt typing
        dkTypewriter.BeginTyping();

        yield return new WaitUntil(() => Input.anyKeyDown);
        AudioManager.Instance.musicSource.UnPause();
        SceneManager.LoadScene("LevelOne");

    }

    void SetAlpha(TextMeshProUGUI text, float alpha)
    {
        Color c = text.color;
        c.a = alpha;
        text.color = c;
    }

    IEnumerator FadeText(TextMeshProUGUI text, float fromAlpha, float toAlpha, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            float alpha = Mathf.Lerp(fromAlpha, toAlpha, elapsed / duration);
            SetAlpha(text, alpha);
            elapsed += Time.deltaTime;
            yield return null;
        }
        SetAlpha(text, toAlpha);
    }
}
