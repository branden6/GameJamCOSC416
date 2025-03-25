using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text timerText;
    public TMP_Text levelText;
    public Image[] heartIcons;

    private int score = 0;
    private int lives = 3;
    public int level = 1;

    public float timeRemaining = 420f;  // Timer start
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
                // We can call game over here
            }
            UpdateHUD();
        }
    }

    public void UpdateHUD()
    {
        scoreText.text = "SCORE: " + score.ToString("D6");
        timerText.text = "TIME: " + Mathf.CeilToInt(timeRemaining).ToString();
        levelText.text = "LEVEL: 1";

        if (timeRemaining <= 60f)
            timerText.color = Color.red;
        else
            timerText.color = Color.white;

        for (int i = 0; i < heartIcons.Length; i++)
        {
            heartIcons[i].enabled = i < lives;
        }
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateHUD();
    }

    public void LoseLife()
    {
        if (lives > 0)
            lives--;
        UpdateHUD();
    }

    public void SetLevel(int newLevel)
    {
        level = newLevel;
        UpdateHUD();
    }
}
