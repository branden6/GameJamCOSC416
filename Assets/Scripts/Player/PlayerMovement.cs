using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private bool midJump = false;
    private Rigidbody rb;
    [SerializeField]private float speed = 1.25f;
    [SerializeField]private float jumpForce = 2.5f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
    
        if (Input.GetKey(KeyCode.A) && !midJump)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        if (Input.GetKey(KeyCode.D) && !midJump)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }


        if (Input.GetKey(KeyCode.A))
        {
            rb.linearVelocity = new Vector3(-speed, rb.linearVelocity.y, 0);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.linearVelocity = new Vector3(speed, rb.linearVelocity.y, 0);
        }
        else
        {
            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0); 
        }

        // Jumping
        if (Input.GetKeyDown(KeyCode.Space) && !midJump)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, 0);
            midJump = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("platform"))
        {
            midJump = false;
        }
    }
}