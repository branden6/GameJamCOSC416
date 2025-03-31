using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int firstPlayableLevelBuildIndex = 5;

    public static GameManager Instance { get; private set; }

    public int score = 0;
    public int currentLevelIndex = 1; // Default to Level 1

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Update level index based on scene build index
        currentLevelIndex = scene.buildIndex;

        // Update the HUD if it exists in the new scene
        HUDManager hud = FindObjectOfType<HUDManager>();
        if (hud != null)
        {
            hud.UpdateHUD();
        }
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
            player.TakeDamage(player.maxHealth);
        }
    }

    public void LoadNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            SceneManager.LoadScene("GameWon");
            Debug.Log("All levels complete! Game won!");
        }
    }

    public void RestartGame()
    {
        score = 0;
        currentLevelIndex = 1;
        SceneManager.LoadScene(currentLevelIndex);
    }

    public void LoadGameOverScene()
    {
        SceneManager.LoadScene("GameOver");
    }
}
