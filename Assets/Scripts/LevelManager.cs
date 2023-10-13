using System.Collections;
using System.Collections.Generic;
using Unity.Services.Mediation;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public GameObject activeCheckpoint;
    private PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RespawnPlayer()
    {
        player.transform.position = activeCheckpoint.transform.position;
    }
}
