using UnityEngine;

public class LadderClimb : MonoBehaviour
{
    private Rigidbody rb;
    private Animator animator;
    public Transform visual;

    [SerializeField] private float climbSpeed = 0.75f;
    [SerializeField] private float groundCheckDistance = 0.1f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask ladderLayer;

    private UserInput input;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = visual.GetComponent<Animator>();
        input = FindObjectOfType<UserInput>();
    }

    private void Update()
    {
        bool isTouchingLadder = IsTouchingLadder();
        bool isGrounded = IsGrounded();
        float verticalInput = input.Up ? 1f : input.Down ? -1f : 0f;

        // Only climb if we're touching ladder, NOT grounded, and moving vertically
        if (isTouchingLadder && !isGrounded && verticalInput != 0f)
        {
            rb.useGravity = false;
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, verticalInput * climbSpeed, 0f);
            animator.SetBool("climbing", true);
            animator.SetFloat("climbSpeed", 1f);
        }
        // Pause on ladder mid-air
        else if (animator.GetBool("climbing") && isTouchingLadder && !isGrounded)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, 0f);
            rb.useGravity = false;
            animator.SetBool("climbing", true);
            animator.SetFloat("climbSpeed", 0f);
        }
        // Not climbing
        else
        {
            rb.useGravity = true;
            animator.SetBool("climbing", false);
            animator.SetFloat("climbSpeed", 1f);
        }
    }


    private bool IsTouchingLadder()
    {
        Vector3 boxCenter = transform.position;
        Vector3 boxHalfExtents = new Vector3(0.3f, 1f, 0.3f);
        Collider[] hits = Physics.OverlapBox(boxCenter, boxHalfExtents, Quaternion.identity, ladderLayer);
        return hits.Length > 0;
    }

    private bool IsGrounded()
    {
        Vector3 origin = transform.position + Vector3.down * 0.05f;
        float rayLength = groundCheckDistance;

        Debug.DrawRay(origin, Vector3.down * rayLength, Color.red);
        return Physics.Raycast(origin, Vector3.down, rayLength, groundLayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Vector3 boxCenter = transform.position;
        Vector3 boxHalfExtents = new Vector3(0.3f, 1f, 0.3f);
        Gizmos.DrawWireCube(boxCenter, boxHalfExtents * 2);
    }
}
