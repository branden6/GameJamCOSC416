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
