using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            int nextSceneIndex = currentSceneIndex + 1;

            if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
            {
                // Load next level if it exists
                SceneManager.LoadScene(nextSceneIndex);
                Debug.Log("Loading next level...");
            }
            else
            {
                // No more levels, go to GameWon
                SceneManager.LoadScene("GameWon");
                Debug.Log("All levels complete! Game won!");
            }
        }
    }
}
