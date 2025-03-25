using UnityEngine;

public class NeutralClone : MonoBehaviour
{
    public float vaultForce = 10f;
    private bool playerNearby = false;
    private GameObject player;
    public Player playerScript;

    void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.F))
        {
            Rigidbody playerRb = player.GetComponent<Rigidbody>();
            playerRb.linearVelocity = new Vector3(playerRb.linearVelocity.x, vaultForce, 0);

            playerScript.ClearNeutralClone();

            Destroy(gameObject);
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
}
