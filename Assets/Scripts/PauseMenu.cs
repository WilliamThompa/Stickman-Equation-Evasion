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
        SceneManager.LoadSceneAsync(levelSelect);
    }

    public void Settings()
    {
        SceneManager.LoadSceneAsync(settings);
    }

    public void QuitToMenu()
    {
        SceneManager.LoadSceneAsync(quitToMenu);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    void Update()
    {
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

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
        }
    }
}
