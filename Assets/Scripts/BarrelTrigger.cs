using UnityEngine;

public class BarrelTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("🔥 BarrelTrigger activated! Collided with: " + other.name);
    }
}
