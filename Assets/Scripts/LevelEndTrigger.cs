using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEndTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            int totalScenes = SceneManager.sceneCountInBuildSettings;

            if (currentSceneIndex + 1 < totalScenes)
            {
                SceneManager.LoadScene(currentSceneIndex + 1);
            }
            else
            {
                Debug.Log("Reached last level. No more scenes to load.");
            }
        }
    }
}
