using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    private bool levelComplete = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!levelComplete && other.CompareTag("Player"))
        {
            levelComplete = true;
            AudioManager.Instance.PlaySFX("Success");

            HUDManager hud = FindObjectOfType<HUDManager>();
            if (hud != null)
            {
                hud.AddScore(1000);
                // No need to set level here — GameManager will do it on scene load
            }

            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            int nextSceneIndex = currentSceneIndex + 1;

            if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(nextSceneIndex);
                Debug.Log("Loading next level...");
            }
            else
            {
                SceneManager.LoadScene("GameWon");
                Debug.Log("All levels complete! Game won!");
            }
        }
    }
}
