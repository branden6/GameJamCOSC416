using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public GameObject howToPlayPanel;

    public void StartGame()
    {
        // Load level scene later
    }

    public void ShowHowToPlay()
    {
        howToPlayPanel.SetActive(true);
    }

    public void HideHowToPlay()
    {
        howToPlayPanel.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game Closed");
    }
}
