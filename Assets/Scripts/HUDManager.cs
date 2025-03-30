using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text timerText;
    public TMP_Text levelText;
    public TMP_Text livesText; // purely for displaying Player's lives

    public Image[] heartIcons;

    private int score = 0;
    public int level = 1;

    public float timeRemaining = 420f;
    private bool timerRunning = true;

    void Update()
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
        scoreText.text = "SCORE: " + score.ToString("D6");
        timerText.text = "TIME: " + Mathf.CeilToInt(timeRemaining).ToString();
        levelText.text = "LEVEL: " + level;

        timerText.color = timeRemaining <= 60f ? Color.red : Color.white;
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateHUD();
    }

    public void SetLevel(int newLevel)
    {
        level = newLevel;
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
