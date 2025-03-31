using System.Collections;
using TMPro;
using UnityEngine;

public class DKTextTypewriter : MonoBehaviour
{
    public TextMeshProUGUI textBox;
    [TextArea(3, 10)] public string fullText;
    public float delay = 0.1f;

    public void BeginTyping()
    {
        textBox.color = new Color(textBox.color.r, textBox.color.g, textBox.color.b, 1f);
        StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        textBox.text = "";
        foreach (char c in fullText)
        {
            textBox.text += c;
            yield return new WaitForSeconds(delay);
        }
    }
}
