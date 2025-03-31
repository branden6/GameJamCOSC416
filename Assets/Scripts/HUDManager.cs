using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text timerText;
    public TMP_Text levelText;
    public TMP_Text livesText;

    public Image[] heartIcons;

    public float timeRemaining = 420f;
    private bool timerRunning = true;

    private void Start()
    {
        UpdateHUD();
    }

    private void Update()
    {
        if (timerRunning)
        {
            timeRemaining -= Time.deltaTime;

            if (timeRemaining <= 0)
            {
                timeRemaining = 0;
                timerRunning = false;
                Debug.Log("Time's up! Game Over.");
            }

            UpdateHUD();
        }
    }

    public void UpdateHUD()
    {
        scoreText.text = "SCORE: " + GameManager.Instance.score.ToString("D6");
        timerText.text = "TIME: " + Mathf.CeilToInt(timeRemaining).ToString();
        int levelNumber = GameManager.Instance.currentLevelIndex - GameManager.Instance.firstPlayableLevelBuildIndex + 1;
        levelText.text = "LEVEL: " + levelNumber;
        timerText.color = timeRemaining <= 60f ? Color.red : Color.white;
    }

    public void AddScore(int points)
{
    GameManager.Instance.score += points;

    // Prevent score from going below 0
    if (GameManager.Instance.score < 0)
    {
        GameManager.Instance.score = 0;
    }

    UpdateHUD();
}


    public void SetHealth(int hp)
    {
        for (int i = 0; i < heartIcons.Length; i++)
            heartIcons[i].enabled = false;

        for (int i = 0; i < hp && i < heartIcons.Length; i++)
            heartIcons[i].enabled = true;
    }

    public void SetLives(int lives)
    {
        livesText.text = "LIVES: " + lives.ToString();
    }
}
