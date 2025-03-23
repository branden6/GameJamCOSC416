using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private bool midJump = false;
    private bool onLadder = false;  // Tracks if the player is on a ladder
    private Rigidbody rb;

    [SerializeField] private float speed = 1.75f;
    [SerializeField] private float jumpForce = 2.5f;
    [SerializeField] private float climbSpeed = 1f; // Speed for climbing ladders

    private Vector3 lastVelocity;  // To store the previous horizontal velocity for movement

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (onLadder)
        {
            // Disable gravity while on ladder
            rb.useGravity = false;
            rb.angularVelocity = Vector3.zero; // Stop rotation

            // Climb up (W) and down (S)
            if (Input.GetKey(KeyCode.W))
            {
                rb.linearVelocity = new Vector3(0, climbSpeed, 0);  // Move up
            }
            else if (Input.GetKey(KeyCode.S))
            {
                rb.linearVelocity = new Vector3(0, -climbSpeed, 0);  // Move down
            }
            else
            {
                rb.linearVelocity = new Vector3(0, 0, 0);  // Stay still if no input
            }

            // If no climbing input, allow player to move left and right without climbing
            if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
            {
                rb.linearVelocity = new Vector3(lastVelocity.x, rb.linearVelocity.y, 0);  // Preserve the previous horizontal movement
            }
        }
        else
        {
            // Re-enable gravity when off the ladder
            rb.useGravity = true;

            // Player Rotation
            if (Input.GetKey(KeyCode.A) && !midJump)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            if (Input.GetKey(KeyCode.D) && !midJump)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }

            // Player Movement (Running)
            if (Input.GetKey(KeyCode.A))
            {
                rb.linearVelocity = new Vector3(-speed, rb.linearVelocity.y, 0);  // Move left
                lastVelocity = rb.linearVelocity;  // Store the last velocity
            }
            else if (Input.GetKey(KeyCode.D))
            {
                rb.linearVelocity = new Vector3(speed, rb.linearVelocity.y, 0);  // Move right
                lastVelocity = rb.linearVelocity;  // Store the last velocity
            }
            else
            {
                rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);  // Stop horizontal movement
            }

            // Jumping
            if (Input.GetKeyDown(KeyCode.Space) && !midJump)
            {
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, 0);  // Jump with horizontal velocity
                midJump = true;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("platform"))
        {
            midJump = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ladder"))
        {
            onLadder = true;
            rb.linearVelocity = Vector3.zero; // Stop falling when touching ladder
            rb.useGravity = false; // Disable gravity while on ladder
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ladder"))
        {
            onLadder = false;
            rb.useGravity = true; // Re-enable gravity when exiting the ladder
        }
    }
}
