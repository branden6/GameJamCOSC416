using UnityEngine;

public class BananaSpin : MonoBehaviour
{
    public float spinSpeed = 90f; // Degrees per second

    void Update()
    {
        transform.Rotate(0f, 0f, spinSpeed * Time.deltaTime);
    }
}

