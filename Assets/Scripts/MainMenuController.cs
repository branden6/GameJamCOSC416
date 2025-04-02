using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject HowToPlayCanvas;
    [SerializeField] private GameObject MainMenuCanvas;

    public void StartGame()
    {
        SceneManager.LoadScene("IntroductionStory");
        AudioManager.Instance.PlaySFX("Play");
    }

    public void ShowHowToPlay()
    {
        MainMenuCanvas.SetActive(false);
        HowToPlayCanvas.SetActive(true);
        AudioManager.Instance.PlaySFX("Inventory");
    }

    public void ShowMainMenu()
    {
        HowToPlayCanvas.SetActive(false);
        MainMenuCanvas.SetActive(true);
        AudioManager.Instance.PlaySFX("Inventory");
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        AudioManager.Instance.PlaySFX("Inventory");

#if UNITY_EDITOR
    UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
