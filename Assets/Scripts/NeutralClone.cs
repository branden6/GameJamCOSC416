using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NeutralClone : MonoBehaviour
{
    public float vaultForce = 0.0000001f;
    private bool playerNearby = false;
    [HideInInspector]


    private GameObject player;
    public Player playerScript;

    private void Start()
    {
        StartCoroutine(PopInEffect());
    }

    private void Update()
    {
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

            playerRb.AddForce(Vector3.up * vaultForce, ForceMode.Impulse);
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
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
            player = null;
        }
    }

    private IEnumerator PopInEffect()
    {
        Vector3 originalScale = transform.localScale;
        transform.localScale = Vector3.zero;

        float duration = 0.3f;
        float elapsed = 0;
        while (elapsed < duration)
        {
            transform.localScale = Vector3.Lerp(Vector3.zero, originalScale, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localScale = originalScale;
    }
private IEnumerator TemporarilyDisablePlatformCollision(GameObject player)
{
    Collider playerCollider = player.GetComponent<Collider>();
    List<Collider> ignored = new List<Collider>();

    playerScript.isBoosted = true; // Temporarily invincible

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

    yield return new WaitForSeconds(0.3f); // Duration of invincibility

    foreach (Collider platformCollider in ignored)
    {
        if (platformCollider != null)
        {
            Physics.IgnoreCollision(playerCollider, platformCollider, false);
        }
    }

    Debug.Log("Platform collisions restored.");
    playerScript.isBoosted = false; //  Damage allowed again

    playerScript.ClearNeutralClone();
    Destroy(gameObject);
}
}
