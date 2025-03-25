using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Stats")]
    public int maxHealth = 3;
    public int currentHealth;
    public int lives = 3;

    [Header("HUD Interact")]
    public HUDManager hudManager;

    [Header("Clone Summoning")]
    public GameObject neutralClonePrefab;
    public Transform cloneSpawnPoint;
    private GameObject activeNeutralClone;

    [Header("Movement Settings")]
    [SerializeField] private float speed = 1.25f;
    [SerializeField] private float jumpForce = 2.5f;

    private Rigidbody rb;
    private UserInput input;
    private bool midJump = false;

private void Start()
{
    rb = GetComponent<Rigidbody>();
    input = FindObjectOfType<UserInput>(); // Look for the global input manager
    currentHealth = maxHealth;
}


    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (input.Left)
        {
            if (!midJump) transform.eulerAngles = new Vector3(0, 0, 0);
            rb.linearVelocity = new Vector3(-speed, rb.linearVelocity.y, 0);
        }
        else if (input.Right)
        {
            if (!midJump) transform.eulerAngles = new Vector3(0, 180, 0);
            rb.linearVelocity = new Vector3(speed, rb.linearVelocity.y, 0);
        }
        else
        {
            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
        }

        if (input.Jump && !midJump)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, 0);
            midJump = true;
            input.ResetJump();
        }

        if (Input.GetKeyDown(KeyCode.E) && activeNeutralClone == null)
        {
            SummonNeutralClone();
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("platform"))
        {
            midJump = false;
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log("Player took damage. Current HP: " + currentHealth);
        hudManager.LoseLife();
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        lives--;
        Debug.Log("Player died. Lives remaining: " + lives);

        if (lives > 0)
        {
            Respawn();
        }
        else
        {
            Debug.Log("Game Over!");
            Destroy(gameObject);
        }
    }

    private void Respawn()
    {
        currentHealth = maxHealth;
        Debug.Log("Player respawned.");
    }

    private void SummonNeutralClone()
    {
        GameObject clone = Instantiate(neutralClonePrefab, cloneSpawnPoint.position, Quaternion.identity);
        activeNeutralClone = clone;

        clone.GetComponent<NeutralClone>().playerScript = this;
    }

    public void ClearNeutralClone()
    {
        activeNeutralClone = null;
    }


}
