using UnityEngine;

public class Barrel : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float speed = 6f;
    // This controls how much the barrel drops when it hits a border.
    [SerializeField] private float dropForce = 3f; 
    // 1 means moving right, -1 means moving left
    private int horizontalDirection = 1;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Maintain constant horizontal speed while preserving vertical velocity (gravity)
        rb.linearVelocity = new Vector3(speed * horizontalDirection, rb.linearVelocity.y, 0);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("borderR"))
        {
            horizontalDirection = -1;
            // Apply a downward impulse so the barrel drops a bit
            rb.AddForce(new Vector3(0, -dropForce, 0), ForceMode.VelocityChange);
        }
        else if (collision.gameObject.CompareTag("borderL"))
        {
            horizontalDirection = 1;
            rb.AddForce(new Vector3(0, -dropForce, 0), ForceMode.VelocityChange);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Barrel hit the player!");
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                player.TakeDamage(1);
                GameManager.Instance.DestroyBarrel(gameObject);
            }
        }
    }
}
