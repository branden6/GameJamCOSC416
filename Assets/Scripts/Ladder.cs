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
        input = FindObjectOfType<UserInput>(); // Note: Use FindFirstObjectByType if on Unity 2023+
    }

    private void Update()
    {
        bool isTouchingLadder = IsTouchingLadder();
        bool isGrounded = IsGrounded();

        float verticalInput = input.Up ? 1f : input.Down ? -1f : 0f;

        if (isTouchingLadder && !isGrounded && verticalInput != 0f)
        {
            // Actively climbing
            rb.useGravity = false;
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, verticalInput * climbSpeed, 0f);
            animator.SetBool("climbing", true);
            animator.SetFloat("climbSpeed", 1f); // play animation
        }
        else if (isTouchingLadder && !isGrounded)
        {
            // On ladder but not moving
            rb.useGravity = false;
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, 0f);
            animator.SetBool("climbing", true);
            animator.SetFloat("climbSpeed", 0f); // pause animation
        }
        else
        {
            // Not climbing
            rb.useGravity = true;
            animator.SetBool("climbing", false);
            animator.SetFloat("climbSpeed", 1f); // normal animation speed
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
        Vector3 origin = transform.position + Vector3.up * 0.1f;
        return Physics.Raycast(origin, Vector3.down, groundCheckDistance, groundLayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Vector3 boxCenter = transform.position;
        Vector3 boxHalfExtents = new Vector3(0.3f, 1f, 0.3f);
        Gizmos.DrawWireCube(boxCenter, boxHalfExtents * 2);
    }
}
