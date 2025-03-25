using UnityEngine;

public class GroundKill : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("barrel"))
        {
            GameManager.Instance.DestroyBarrel(other.gameObject);
        }
    }

}
