using UnityEngine;

public class LaughJiggle : MonoBehaviour
{
    public float jiggleSpeed = 5f;
    public float jiggleAmount = 0.1f;
    public float bounceSpeed = 2f;
    public float bounceHeight = 0.15f;

    private Vector3 originalScale;
    private Vector3 originalPos;

    void Start()
    {
        originalScale = transform.localScale;
        originalPos = transform.position;
    }

    void Update()
    {
        float t = Time.time;

        // Jiggle side to side like laughter
        float scaleX = originalScale.x + Mathf.Sin(t * jiggleSpeed) * jiggleAmount;
        float scaleY = originalScale.y + Mathf.Cos(t * jiggleSpeed * 1.5f) * jiggleAmount;

        // Bounce up and down
        float yOffset = Mathf.Sin(t * bounceSpeed) * bounceHeight;

        transform.localScale = new Vector3(scaleX, scaleY, originalScale.z);
        transform.position = originalPos + new Vector3(0f, yOffset, 0f);
    }
}
