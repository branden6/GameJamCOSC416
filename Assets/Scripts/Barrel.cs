using UnityEngine;

public class Barrel : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField]private float speed = 6f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("borderR"))
        {
            rb.linearVelocity = new Vector3(-speed, rb.linearVelocity.y, 0); // Reverse X direction
        }
        else if (collision.gameObject.CompareTag("borderL"))
        {
            rb.linearVelocity = new Vector3(speed, rb.linearVelocity.y, 0); // Reverse X direction
        }
    }
}
