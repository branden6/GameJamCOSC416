using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NeutralClone : MonoBehaviour
{
    private float duration => GameManager.Instance.vaultDuration;

    public float vaultForce = 0.0000001f;
    private bool playerNearby = false;

    private Animator animator;
    private GameObject player;
    public Player playerScript;

    private Transform visual;
    private float lastFacingDirection = 0;

    private void Start()
    {
        visual = transform.Find("Visual");
        if (visual != null)
        {
            animator = visual.GetComponent<Animator>();
            StartCoroutine(PopInEffect(visual));
        }
    }

    private void Update()
    {
        if (playerNearby)
        {
            FacePlayer();
        }

        if (playerNearby && Input.GetKeyDown(KeyCode.F))
        {
            VaultPlayer();
        }
    }

    private void VaultPlayer()
    {
        if (player != null)
        {
            Rigidbody playerRb = player.GetComponent<Rigidbody>();

            if (animator != null)
            {
                animator.SetTrigger("vault");
            }

            playerRb.AddForce(Vector3.up * vaultForce, ForceMode.Impulse);
            AudioManager.Instance.PlaySFX("Boost");
            Debug.Log("Vault Triggered: Force Applied = " + vaultForce);

            StartCoroutine(TemporarilyDisablePlatformCollision(player));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
            player = other.gameObject;

            if (animator != null)
                animator.SetBool("infront", true);

            FacePlayer();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
            player = null;

            if (animator != null)
                animator.SetBool("infront", false);
        }
    }

    private IEnumerator PopInEffect(Transform target)
    {
        Vector3 targetScale = new Vector3(3, 3, 1);
        target.localScale = Vector3.zero;

        float elapsed = 0;
        while (elapsed < duration)
        {
            target.localScale = Vector3.Lerp(Vector3.zero, targetScale, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        target.localScale = targetScale;
    }


    private IEnumerator TemporarilyDisablePlatformCollision(GameObject player)
    {
        Collider playerCollider = player.GetComponent<Collider>();
        List<Collider> ignored = new List<Collider>();

        playerScript.isBoosted = true;

        GameObject[] platforms = GameObject.FindGameObjectsWithTag("platform");

        foreach (GameObject platform in platforms)
        {
            Collider platformCollider = platform.GetComponent<Collider>();
            if (platformCollider != null)
            {
                Physics.IgnoreCollision(playerCollider, platformCollider, true);
                ignored.Add(platformCollider);
            }
        }

        yield return new WaitForSeconds(duration);

        foreach (Collider platformCollider in ignored)
        {
            if (platformCollider != null)
            {
                Physics.IgnoreCollision(playerCollider, platformCollider, false);
            }
        }

        Debug.Log("Platform collisions restored.");
        playerScript.isBoosted = false;

        playerScript.ClearNeutralClone();
        Destroy(gameObject);
    }

    private void FacePlayer()
    {
        if (player == null || visual == null) return;

        float directionToPlayer = player.transform.position.x - transform.position.x;

        if (Mathf.Abs(directionToPlayer) > 0.05f) // 
        {
            float targetFacing = Mathf.Sign(directionToPlayer) * -1; 

            if (targetFacing != lastFacingDirection)
            {
                lastFacingDirection = targetFacing;

                Vector3 scale = visual.localScale;
                visual.localScale = new Vector3(3*targetFacing, scale.y, scale.z);
            }
        }
    }


}
