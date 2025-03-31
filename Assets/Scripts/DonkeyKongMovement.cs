using UnityEngine;

public class DonkeyKongMovement : MonoBehaviour
{
    public float shuffleSpeed = 2f;
    public float shuffleAmount = 0.1f;
    public float bounceSpeed = 2f;
    public float bounceHeight = 0.1f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float shuffle = Mathf.Sin(Time.time * shuffleSpeed) * shuffleAmount;
        float bounce = Mathf.Sin(Time.time * bounceSpeed) * bounceHeight;

        transform.position = new Vector3(startPos.x + shuffle, startPos.y + bounce, startPos.z);
    }
}
