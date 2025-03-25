using UnityEngine;

public class LadderClimb : MonoBehaviour
{
    private bool onLadder = false;
    private Rigidbody rb;
    [SerializeField] private float climbSpeed = 3f;  // Speed of climbing

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (onLadder)
        {
            ClimbLadder();
        }
    }

    void ClimbLadder()
    {
        // Disable gravity while climbing
        rb.useGravity = false;

        // Get the vertical input (W and S keys, or up and down arrows)
        float verticalInput = Input.GetAxisRaw("Vertical");

        if (verticalInput != 0)
        {
            // Move the player vertically along the Y axis
            rb.linearVelocity = new Vector3(0, verticalInput * climbSpeed, 0);
        }
        else
        {
            // Stop any vertical movement when no input
            rb.linearVelocity = new Vector3(0, 0, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ladder"))
        {
            // Enable climbing when the player touches the ladder
            onLadder = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ladder"))
        {
            // Disable climbing when the player exits the ladder
            onLadder = false;
            rb.useGravity = true;  // Re-enable gravity when off the ladder
        }
    }
}
