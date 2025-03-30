using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject HowToPlayCanvas;
    [SerializeField] private GameObject MainMenuCanvas;

    public void StartGame()
    {
        // Load level scene later
    }

    public void ShowHowToPlay()
    {
        MainMenuCanvas.SetActive(false);
        HowToPlayCanvas.SetActive(true);
    }

    public void ShowMainMenu()
    {
        HowToPlayCanvas.SetActive(false);
        MainMenuCanvas.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game Closed");
    }
}
