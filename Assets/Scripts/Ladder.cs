using UnityEngine;

public class LadderClimb : MonoBehaviour
{
    private bool onLadder = false;
    private Rigidbody rb;
    [SerializeField] private float climbSpeed = 0.75f;  

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

        rb.useGravity = false;

    
        float verticalInput = Input.GetAxisRaw("Vertical");

        if (verticalInput != 0)
        {
           
            rb.linearVelocity = new Vector3(0, verticalInput * climbSpeed, 0);
        }
        else
        {
         
            rb.linearVelocity = new Vector3(0, 0, 0);
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
