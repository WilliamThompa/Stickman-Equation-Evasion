using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScript : MonoBehaviour
{
    public LevelManager levelManager;
    private Camera cam;
    private Animator camAnimator;

    private AudioSource health;

    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        cam = Camera.main;
        camAnimator = cam.GetComponent<Animator>();
        health = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Pickup")
        {
            print("Triggered");
            levelManager.AddLife();
            GameObject obj = GameObject.Find(collision.gameObject.name);
            print(obj.name);
            Destroy(obj);
            health.Play();
        }

        if (collision.gameObject.tag == "CameraCheckpoint")
        {
            camAnimator.CrossFade("PanOut", 0, 0);
        }
        if (collision.gameObject.tag == "CameraExitCheckpoint")
        {
            camAnimator.CrossFade("PanIn", 0, 0);
        }

    }
}
