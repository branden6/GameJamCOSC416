using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Goal : MonoBehaviour
{
    private bool levelComplete = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!levelComplete && other.CompareTag("Player"))
        {
            levelComplete = true;
            Time.timeScale = 0f;
            AudioManager.Instance.musicSource.Pause();
            AudioManager.Instance.backgroundSource.Pause();
            AudioManager.Instance.PlaySFX("Success");

            HUDManager hud = FindObjectOfType<HUDManager>();
            if (hud != null)
            {
                hud.AddScore(1000);
            }

            StartCoroutine(WaitForSFX(2.7f));
        }
    }

    private IEnumerator WaitForSFX(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        ResumeAfterSFX();
    }

    private void ResumeAfterSFX()
    {
        Time.timeScale = 1f;
        AudioManager.Instance.musicSource.UnPause();
        AudioManager.Instance.backgroundSource.UnPause();

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
