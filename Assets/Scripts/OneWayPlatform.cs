using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{
    private Collider platformCollider;

    private void Start()
    {
        platformCollider = GetComponent<Collider>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Check if player is below the platform
            if (other.transform.position.y < transform.position.y)
            {
                // Ignore collision so player can pass through from below
                Physics.IgnoreCollision(platformCollider, other, true);
            }
            else
            {
                // Enable collision when player is above
                Physics.IgnoreCollision(platformCollider, other, false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Physics.IgnoreCollision(platformCollider, other, false);
        }
    }
}
