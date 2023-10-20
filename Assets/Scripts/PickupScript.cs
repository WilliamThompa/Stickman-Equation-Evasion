using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScript : MonoBehaviour
{
    public LevelManager levelManager;

    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
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
        }
    }
}
