using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndUI : MonoBehaviour
{
    [Header("UI References")]
    public TMP_Text messageText;
    public TMP_Text scoreText;

    [Header("Optional Settings")]
    public string defaultMessage = "Game Over - You Died";

    void Start()
{
    int finalScore = GameManager.Instance != null ? GameManager.Instance.score : 0;

    string messageToShow = defaultMessage;


    if (SceneManager.GetActiveScene().name == "GameWon")
    {
        messageToShow = "You Got the BANANA!!";
        AudioManager.Instance.ChangeMusicPitch(1.34f);
    }
    else {
        AudioManager.Instance.ChangeMusicPitch(0.48f);
    }

    Setup(messageToShow, finalScore);
}


    public void Setup(string message, int finalScore)
    {
        if (messageText != null)
            messageText.text = message;

        if (scoreText != null)
            scoreText.text = "Your score is: " + finalScore.ToString("D6");

        gameObject.SetActive(true);
    }

    public void ReturnToMainMenu()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.score = 0;
            GameManager.Instance.currentLevelIndex = GameManager.Instance.firstPlayableLevelBuildIndex;
        }

        SceneManager.LoadScene("IntroScene");
        AudioManager.Instance.PlaySFX("Inventory");
        AudioManager.Instance.ChangeMusicPitch(1.07f);
    }

    public void QuitGame()
    {
        if (GameManager.Instance != null)
        {
            AudioManager.Instance.PlaySFX("Inventory");
            GameManager.Instance.score = 0;
            GameManager.Instance.currentLevelIndex = GameManager.Instance.firstPlayableLevelBuildIndex;
        }

        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
