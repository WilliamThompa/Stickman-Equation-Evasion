using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{

    public string levelSelect;
    public LevelManager levelManager;
    public GameObject gameoverscreen;

    private void Start()
    {
        levelManager = LevelManager.self;
        gameoverscreen = GameObject.Find("GameOver");
    }

    public void LevelSelect()
    {
        if (gameoverscreen == null) gameoverscreen = GameObject.Find("GameOver");
        gameoverscreen.SetActive(false);
        Time.timeScale = 1;
        levelManager.LivesCounter = 3;
        SceneManager.LoadSceneAsync(levelSelect);
    }
}

