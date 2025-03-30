using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int currentLevelIndex = 1; // Assuming LevelOne = Build Index 1

    private void Awake()
    {
        // Ensure only one GameManager exists
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Keep GameManager alive between scenes
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

    public void LoadNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            currentLevelIndex = nextSceneIndex;
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("No more levels. Game completed!");
        }
    }

    public void RestartGame()
    {
        currentLevelIndex = 1; // Reset back to LevelOne
        SceneManager.LoadScene(currentLevelIndex);
    }

    public void LoadGameOverScene()
    {
        SceneManager.LoadScene("GameOver"); // Make sure GameOver is added to Build Settings
    }
}
