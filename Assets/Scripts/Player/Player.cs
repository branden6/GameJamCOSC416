using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Stats")]
    public int maxHealth = 3;
    public int currentHealth;
    public int lives = 3;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log("Player took damage. Current HP: " + currentHealth);

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
            // TODO: trigger game over logic here
            Destroy(gameObject);
        }
    }

    private void Respawn()
    {
        currentHealth = maxHealth;
        // Temporary simple respawn – could reset position later
        Debug.Log("Player respawned.");
    }
}
