using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        // Ensure only one GameManager exists
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Keep GameManager alive between scenes (optional for now)
    }

    public void DestroyBarrel(GameObject barrel)
    {
        Debug.Log("Destroying barrel: " + barrel.name);
        Destroy(barrel);
    }

    public void PlayerDied(GameObject playerObject)
{
    Player player = playerObject.GetComponent<Player>();

    if (player != null)
    {
        player.TakeDamage(player.maxHealth); // Force full HP damage
    }
}

}