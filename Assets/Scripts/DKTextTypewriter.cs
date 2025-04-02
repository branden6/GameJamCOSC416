using System.Collections;
using TMPro;
using UnityEngine;

public class DKTextTypewriter : MonoBehaviour
{
    public TextMeshProUGUI textBox;
    [TextArea(3, 10)] public string fullText;
    public float delay = 0.1f;
    public float soundDuration = 1.6f;
    public float minBreakDuration = 0.8f;
    public float maxBreakDuration = 1.2f; 
    private float currentBreakDuration;
    private float lastSoundTime = 0f;

    public void BeginTyping()
    {
        textBox.color = new Color(textBox.color.r, textBox.color.g, textBox.color.b, 1f);
        StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        textBox.text = "";
        lastSoundTime = -soundDuration;
        currentBreakDuration = Random.Range(minBreakDuration, maxBreakDuration);
        foreach (char c in fullText)
        {
            textBox.text += c;
            if(Time.time - lastSoundTime >= soundDuration + currentBreakDuration){
                AudioManager.Instance.PlaySFX("Dialogue");
                lastSoundTime = Time.time;
                currentBreakDuration = Random.Range(minBreakDuration, maxBreakDuration);
            }
            yield return new WaitForSeconds(delay);
        }
    }
}
