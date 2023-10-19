using System.Collections;
using System.Collections.Generic;
using Unity.Services.Mediation;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    private static bool created = false;
    public static LevelManager self;
    public bool enabledLevel1 = true;
    public bool enabledLevel2 = false;
    public bool enabledLevel3 = false;
    public bool enabledLevel4 = false;
    public bool enabledLevel5 = false;

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
            try
            {
                self.player = GameObject.Find("Player Variant").transform;
            }
            catch
            {
                Debug.LogWarning("fix this lol");
            }
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //player = GameObject.Find("Player").transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RespawnPlayer()
    {
        
        Vector3 targetPos = activeCheckpoint.transform.position;
        player.position = new Vector3(targetPos.x, targetPos.y + offset, targetPos.z);
    }
}
