using UnityEngine;

public class LadderClimb : MonoBehaviour
{
    private bool onLadder = false;
    private Rigidbody rb;
    [SerializeField] private float climbSpeed = 0.75f;

    private UserInput input;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        input = FindObjectOfType<UserInput>();
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
        rb.useGravity = false;

        if (input.Up)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, climbSpeed, 0);
        }
        else if (input.Down)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, -climbSpeed, 0);
        }
        else
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ladder"))
        {
            onLadder = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ladder"))
        {
            onLadder = false;
            rb.useGravity = true;
        }
    }
}
