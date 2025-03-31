using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject HowToPlayCanvas;
    [SerializeField] private GameObject MainMenuCanvas;

    public void StartGame()
    {
        SceneManager.LoadScene("IntroductionStory");
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

    public void QuitGame()
    {
        Debug.Log("Quit Game");

#if UNITY_EDITOR
    UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
