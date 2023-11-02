using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Services.Mediation;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{

    private static bool created = false;
    public static LevelManager self;
    public bool enabledLevel1 = true;
    public bool enabledLevel2 = false;
    public bool enabledLevel3 = false;
    public bool enabledLevel4 = false;
    public bool enabledLevel5 = false;

    public int defaultLives = 3;
    [SerializeField]
    private int livesCounter;
    public string levelSelect;
    public UIManager uim;

    public Animator anim;
    public GameObject activeCheckpoint;

    public int LivesCounter
    {
        get
        {
            return livesCounter;
        }
        set
        {
            int tmpVal = value;
            if(tmpVal < 0)
            {
                //prevents from being less than 0
                tmpVal = 0;
            }
            livesCounter = tmpVal;
            //updates UI
            UpdateLivesCounter();
        }
    }

    public TMP_Text livesText;
    public GameObject gameOverScreen;
    public Canvas gameover;

    [SerializeField]
    private Transform player;
    [Range(-10, 10)]
    public float offset = 10;

    private void Awake()
    {
        if (!created)
        {
            uim = GameObject.Find("UIManager").GetComponent<UIManager>();
            uim.Load();
            DontDestroyOnLoad(gameObject);
            created = true;
            self = this;
            if (self == null)
            {
                self = new LevelManager();
            }
            try
            {
                self.player = GameObject.Find("Player Variant").transform;
            }
            catch
            {
                Debug.LogWarning("fix this lol");
            }
            Debug.Log("New LevelManager created.");

            // Respawn transition
            try
            {
                anim = GameObject.Find("Transition").GetComponent<Animator>();

            }
            catch(Exception e) { print(e); }

        }
        else
        {
            Debug.Log("LevelManager already exists, destroying...");
            self.activeCheckpoint = activeCheckpoint;
            livesText = GameObject.Find("Life Count").GetComponent<TMP_Text>();
            try
            {
                self.player = GameObject.Find("Player Variant").transform;
                
            }
            catch
            {
                Debug.LogWarning("fix this lol");
            }
            livesText.text = "x " + self.livesCounter.ToString();
            Destroy(gameObject);
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        uim = self.GetComponent<UIManager>();
        LivesCounter = defaultLives;
    }

    // Update is called once per frame
    void Update()
    {
        if (activeCheckpoint == null) activeCheckpoint = GameObject.Find("Spawn");
        if (livesCounter > 0 && gameOverScreen == null)
        {
            gameOverScreen = GameObject.Find("GameOver");
            gameover = gameOverScreen.GetComponent<Canvas>();
            gameover.enabled = false;
        }
        if (livesText == null) livesText = GameObject.Find("Life Count").GetComponent<TMP_Text>();
        if (livesText.text == "")
        {
            UpdateLivesCounter();
        }
    }

    public void RespawnPlayer()
    {
        
        Vector3 targetPos = activeCheckpoint.transform.position;
        if (player == null) self.player = GameObject.Find("Player Variant").transform;
        player.position = new Vector3(targetPos.x, targetPos.y + offset, targetPos.z);
    }

    public void TakeLife()
    {

        LivesCounter--;
        if(LivesCounter <= 0)
        {
            gameover.enabled = true;
            Time.timeScale = 0;
            Debug.LogWarning(Time.timeScale);
        }
    }

    public void ResetLife()
    {
        gameover.enabled = true;
        livesCounter = 3;
        Time.timeScale = 1;
     }

    public void AddLife()
    {
        //print($"First part: {livesCounter}");
        livesCounter = livesCounter + 1;
        //print($"Second part: {livesCounter}");
        UpdateLivesCounter();
    }

    public void UpdateLivesCounter()
    {
        if (livesText == null) livesText = GameObject.Find("Life Count").GetComponent<TMP_Text>();
        livesText.text = "x " + livesCounter.ToString();
    }
}
