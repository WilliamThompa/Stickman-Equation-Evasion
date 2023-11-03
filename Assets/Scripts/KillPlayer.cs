using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{

    public LevelManager levelManager;

    private AudioSource deathSound;

    // Start is called before the first frame update
    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        deathSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.name == "Player Variant")
        {
            if (levelManager == null) levelManager = FindObjectOfType<LevelManager>();
            levelManager.RespawnPlayer();
            levelManager.TakeLife();
            deathSound.Play();
        }
    }
}
