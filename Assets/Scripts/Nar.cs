using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nar : MonoBehaviour
{
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Doors_Base" ||
            collision.name == "Doors_Base (1)" ||
            collision.name == "Doors_Base (2)" ||
            collision.name == "Doors_Base (3)" ||
            collision.name == "Doors_Base (4)"
            )
        {
            animator.Play("DoorEnter",0);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "Doors_Base" || 
            collision.name == "Doors_Base (1)" ||
            collision.name == "Doors_Base (2)" ||
            collision.name == "Doors_Base (3)" ||
            collision.name == "Doors_Base (4)"
            )
        {
            animator.Play("DoorLeave", 0);
        }
    }

}
