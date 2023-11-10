using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    public LevelManager levelManager;

    private AudioSource checkpoint;

    // Start is called before the first frame update
    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        checkpoint = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Player Variant")
        {
            if (levelManager == null) levelManager = FindObjectOfType<LevelManager>();
            levelManager.activeCheckpoint = gameObject;
            if (checkpoint == null) checkpoint = GetComponent<AudioSource>();
            checkpoint.Play();
        }
    }
}