using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance;
    [Header("UI Panels")]
    public GameObject pauseMenuUI;
    public GameObject howToPlayPanel;

    public Slider MusicSlider, SoundSlider;

    private bool isPaused = false;

        private void Awake(){
        if(Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (howToPlayPanel.activeSelf)
            {
                howToPlayPanel.SetActive(false);
                return;
            }

            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        Time.timeScale = isPaused ? 0f : 1f;
        pauseMenuUI.SetActive(isPaused);
        AudioManager.Instance.PlaySFX("Pause");
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
        AudioManager.Instance.PlaySFX("Play");
    }

    public void OpenHowToPlay()
    {
        howToPlayPanel.SetActive(true);
        AudioManager.Instance.PlaySFX("Inventory");
    }
    public void CloseHowToPlay()
    {
        howToPlayPanel.SetActive(false);
        AudioManager.Instance.PlaySFX("Inventory");
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("IntroScene");
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
    
public void ToggleMusic(){
    AudioManager.Instance.ToggleMusic();
}
public void ToggleSFX(){
    AudioManager.Instance.ToggleSFX();
}
public void MusicVolume(){
    AudioManager.Instance.MusicVolume(MusicSlider.value);
}
public void SFXVolume(){
    AudioManager.Instance.SFXVolume(SoundSlider.value);
}

}
