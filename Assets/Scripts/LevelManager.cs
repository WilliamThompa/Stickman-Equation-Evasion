using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Services.Mediation;
using UnityEngine;
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

    public GameObject activeCheckpoint;
    [SerializeField]
    private Transform player;
    [Range(-10, 10)]
    public float offset = 10;

    private void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(gameObject);
            created = true;
            self = this;
            try
            {
                self.player = GameObject.Find("Player Variant").transform;
            }
            catch
            {
                Debug.LogWarning("fix this lol");
            }
            Debug.Log("New LevelManager created.");
            
        }
        else
        {
            Debug.Log("LevelManager already exists, destroying...");
            self.activeCheckpoint = activeCheckpoint;
            self.livesText = livesText;
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
        LivesCounter = defaultLives;
        gameOverScreen = GameObject.Find("GameOver");
        gameOverScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (gameOverScreen == null)
        {
            gameOverScreen = GameObject.Find("GameOver");
        }
        if (livesCounter > 0)
        {
            gameOverScreen.SetActive(false);
        }
    }

    public void RespawnPlayer()
    {
        
        Vector3 targetPos = activeCheckpoint.transform.position;
        player.position = new Vector3(targetPos.x, targetPos.y + offset, targetPos.z);
    }

    public void TakeLife()
    {

        LivesCounter--;
        if(LivesCounter <= 0)
        {
            gameOverScreen.SetActive(true);
            Time.timeScale = 0;
            Debug.LogWarning(Time.timeScale);
        }
    }

    public void ResetLife()
    {
        gameOverScreen.SetActive(false);
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
        livesText.text = "x " + livesCounter.ToString();
    }
}
