using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public string levelSelect;
    public string settings;
    public string quitToMenu;
    public bool isPaused;
    public GameObject pauseCanvas;

    public void LevelSelect()
    {
        Time.timeScale = 1;
        isPaused = false;
        SceneManager.LoadSceneAsync(levelSelect);
    }

    public void Settings()
    {
        Time.timeScale = 1;
        isPaused = false;
        SceneManager.LoadSceneAsync(settings);
    }

    public void QuitToMenu()
    {
        Time.timeScale = 1;
        isPaused = false;
        SceneManager.LoadSceneAsync(quitToMenu);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            if (isPaused)
            {
                pauseCanvas.SetActive(true);
                Time.timeScale = 0f;
            }
            else
            {
                pauseCanvas.SetActive(false);
                Time.timeScale = 1f;
            }

        }
    }
}
