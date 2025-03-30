using UnityEngine;
using System.Collections;

public class Barrel : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float speed = 6f;
    [SerializeField] private float dropForce = 3f;
    // 1 means moving right, -1 means moving left
    private int horizontalDirection = 1;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Maintain constant horizontal movement while keeping the vertical velocity (gravity) unchanged
        rb.linearVelocity = new Vector3(speed * horizontalDirection, rb.linearVelocity.y, 0);
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check for collision with the right border
        if (collision.gameObject.CompareTag("borderR"))
        {
            // 15% chance to ignore collision (roll through)
            if (Random.value < 0.10f)
            {
                StartCoroutine(TemporarilyIgnoreCollision(collision.collider));
            }
            else
            {
                // Otherwise, reverse direction and apply a downward impulse
                horizontalDirection = -1;
                rb.AddForce(new Vector3(0, -dropForce, 0), ForceMode.VelocityChange);
            }
        }
        // Check for collision with the left border
        else if (collision.gameObject.CompareTag("borderL"))
        {
            if (Random.value < 0.10f)
            {
                StartCoroutine(TemporarilyIgnoreCollision(collision.collider));
            }
            else
            {
                horizontalDirection = 1;
                rb.AddForce(new Vector3(0, -dropForce, 0), ForceMode.VelocityChange);
            }
        }
    }

    private IEnumerator TemporarilyIgnoreCollision(Collider borderCollider)
    {
        Collider barrelCollider = GetComponent<Collider>();
        // Disable collision between the barrel and the border
        Physics.IgnoreCollision(borderCollider, barrelCollider, true);
        // Wait for a short period so the barrel can roll through (adjust duration as needed)
        yield return new WaitForSeconds(0.5f);
        // Re-enable the collision so future interactions work normally
        Physics.IgnoreCollision(borderCollider, barrelCollider, false);
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
