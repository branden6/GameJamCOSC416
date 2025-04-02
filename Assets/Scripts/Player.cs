using UnityEngine;
using System.Collections;

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
    public GameObject guardClonePrefab;
    public Transform cloneSpawnPoint;
    private GameObject activeNeutralClone;
    private GameObject activeGuardClone;

    private bool canSummonNeutralClone = true;
    private bool cooldownRunning = false;
    public float neutralCloneCooldown = 10.0f;

    [Header("Movement Settings")]
    [SerializeField] private float speed = 1.25f;
    [SerializeField] private float jumpForce = 2.5f;

    [Header("Visual Reference")]
    public Transform visual;
    private Animator animator;

    private Rigidbody rb;
    private UserInput input;
    private bool midJump = false;

    [Header("Sound Effects")]
    public float footstepTimer = 0f;
    public float footstepInterval = 0.25f;
    public float ladderTimer = 0f;
    public float ladderInterval = 0.5f;
    public bool isOnLadder = false;

    [HideInInspector]
    public bool isBoosted = false;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        input = FindObjectOfType<UserInput>();
        currentHealth = maxHealth;
        animator = visual.GetComponent<Animator>();
        hudManager.SetLives(lives);
    }

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        Vector3 velocity = rb.linearVelocity;
        bool isMoving = false;
        footstepTimer += Time.deltaTime;


        if (input.Left)
        {
            visual.localScale = new Vector3(1, 1, 1);
            velocity.x = -speed;
            isMoving = true;
            FlipCloneSpawnPoint(false);
            if (!midJump && footstepTimer >= footstepInterval){
                PlayFootstep();
        }
        }
        else if (input.Right)
        {
            visual.localScale = new Vector3(-1, 1, 1);
            velocity.x = speed;
            isMoving = true;
            FlipCloneSpawnPoint(true);
            if (!midJump && footstepTimer >= footstepInterval){
                PlayFootstep();
        }
        }
        else
        {
            velocity.x = 0;
        }

        if (input.Jump && !midJump)
        {
            velocity.y = jumpForce;
            midJump = true;
            animator.SetTrigger("Jump");
            input.ResetJump();
            AudioManager.Instance.PlaySFX("Jump");
        }

        rb.linearVelocity = velocity;
        animator.SetBool("isRunning", isMoving);
        animator.SetBool("isJumping", midJump);

        // Clone summoning logic
        if (Input.GetKeyDown(KeyCode.E))
        {
            if ((input.Left || input.Right) && activeGuardClone == null)
            {
                float dir = input.Left ? -1f : 1f;
                SummonGuardClone(dir);
            }
            else if (canSummonNeutralClone)
            {
                if (activeNeutralClone == null)
                {
                    SummonNeutralClone();
                }

                StartCoroutine(NeutralCloneCooldownTimer());
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("platform"))
        {
            midJump = false;
            animator.SetBool("isJumping", false);
        }
    }

    private void OnTriggerEnter(Collider other)
{
    if (other.CompareTag("ladder"))
    {
        isOnLadder = true;
        ladderTimer = 0f;
    }
}

private void OnTriggerStay(Collider other)
{
    if (other.CompareTag("ladder") && isOnLadder)
    {
        if (Mathf.Abs(rb.linearVelocity.y) > 0.1f) 
        {
            ladderTimer += Time.deltaTime;
            if (ladderTimer >= ladderInterval)
            {
                AudioManager.Instance.PlaySFX("Ladder1");
                ladderTimer = 0f;
            }
        }
    }
}


private void OnTriggerExit(Collider other)
{
    if (other.CompareTag("ladder"))
    {
        isOnLadder = false;
        ladderTimer = 0f;
    }
}


    public void TakeDamage(int amount)
    {
        if (isBoosted) return;

        currentHealth -= amount;
        animator.SetTrigger("Hit");
        hudManager.SetHealth(currentHealth);
        AudioManager.Instance.PlaySFX("Hit");
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        lives--;
        animator.SetTrigger("Die");
        AudioManager.Instance.PlaySFX("Low Health");

        if (lives > 0)
        {
            Invoke(nameof(Respawn), 1.5f);
        }
        else
        {
            AudioManager.Instance.PlaySFX("Hit");
            GameManager.Instance.LoadGameOverScene();
            Destroy(gameObject);
        }
    }

    private void Respawn()
    {
        currentHealth = maxHealth;
        hudManager.SetHealth(currentHealth);
        hudManager.SetLives(lives);

        GameObject spawnPoint = GameObject.FindGameObjectWithTag("PlayerSpawn");

        if (spawnPoint != null)
        {
            transform.position = spawnPoint.transform.position;
        }
    }

    private void SummonNeutralClone()
    {
        Vector3 spawnPos = cloneSpawnPoint.position;
        float heightOffset = 0.3f;

        RaycastHit[] hits = Physics.SphereCastAll(spawnPos + Vector3.up * 0.5f, 0.3f, Vector3.down, 10f);
        RaycastHit? closestPlatformHit = null;
        float closestDistance = float.MaxValue;

        foreach (var hit in hits)
        {
            if (hit.collider.CompareTag("platform") && hit.distance < closestDistance)
            {
                closestPlatformHit = hit;
                closestDistance = hit.distance;
            }
        }

        if (closestPlatformHit.HasValue)
        {
            RaycastHit hit = closestPlatformHit.Value;
            spawnPos.y = hit.point.y + heightOffset;

            GameObject clone = Instantiate(neutralClonePrefab, spawnPos, Quaternion.identity);
            activeNeutralClone = clone;

            clone.transform.up = hit.normal;

            Transform cloneVisual = clone.transform.Find("Visual");
            if (cloneVisual != null)
            {
                float playerFacing = visual.localScale.x;
                cloneVisual.localScale = new Vector3(-playerFacing * 3f, cloneVisual.localScale.y, cloneVisual.localScale.z);
            }

            clone.GetComponent<NeutralClone>().playerScript = this;
        }
    }

    private IEnumerator NeutralCloneCooldownTimer()
    {
        if (cooldownRunning) yield break;

        cooldownRunning = true;
        canSummonNeutralClone = false;

        yield return new WaitForSeconds(neutralCloneCooldown);

        canSummonNeutralClone = true;
        cooldownRunning = false;
    }

    private void SummonGuardClone(float direction)
    {
        if (activeGuardClone != null) return;

        Vector3 spawnPos = cloneSpawnPoint.position;
        GameObject clone = Instantiate(guardClonePrefab, spawnPos, Quaternion.identity);

        Vector3 walkDirection = new Vector3(direction, 0, 0);
        var guard = clone.GetComponent<GuardClone>();
        guard.Initialize(walkDirection);

        RaycastHit hit;
        int platformLayer = LayerMask.GetMask("Platform");

        if (Physics.Raycast(clone.transform.position + Vector3.up * 0.5f, Vector3.down, out hit, 3f, platformLayer))
        {
            float heightOffset = 0.01f;

            Renderer rend = clone.GetComponentInChildren<Renderer>();
            if (rend != null)
            {
                heightOffset = rend.bounds.extents.y;
            }

            Vector3 correctedPos = clone.transform.position;
            correctedPos.y = hit.point.y + heightOffset;
            clone.transform.position = correctedPos;
        }

        activeGuardClone = clone;
    }

    public void ClearNeutralClone()
    {
        activeNeutralClone = null;
    }

    public void ClearGuardClone()
    {
        activeGuardClone = null;
    }

    private void FlipCloneSpawnPoint(bool facingLeft)
    {
        Vector3 spawnLocalPos = cloneSpawnPoint.localPosition;
        spawnLocalPos.x = Mathf.Abs(spawnLocalPos.x) * (facingLeft ? -1 : 1);
        cloneSpawnPoint.localPosition = spawnLocalPos;
    }

        public void PlayFootstep(){
            int index = AudioManager.Instance.getFootstepNumber();
            AudioManager.Instance.PlayFootstep(AudioManager.Instance.footsteps[index].name);
            footstepTimer = 0f;
    }
}
