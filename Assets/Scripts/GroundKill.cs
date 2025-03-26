using UnityEngine;

public class GroundKill : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("barrel"))
        {
            GameManager.Instance.DestroyBarrel(other.gameObject);
        }
        else if (other.CompareTag("Player"))
        {
            Debug.Log("Player touched the ground!");
            GameManager.Instance.PlayerDied(other.gameObject); // Applies full HP damage
        }
    }
}
