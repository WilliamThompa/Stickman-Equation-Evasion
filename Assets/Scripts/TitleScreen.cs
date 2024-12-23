using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    public string firstLevel;
    public string levelSelect;
    public string settings;
    public LevelManager levelManager;
    
    public void NewGame()
    {
        SceneManager.LoadSceneAsync(levelSelect);
    }

    public void Settings()
    {
        SceneManager.LoadSceneAsync(settings);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
