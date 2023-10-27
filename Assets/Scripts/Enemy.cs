using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int maxHealth = 100;
    int currentHealth;
    public Animator animator;
    public BoxCollider2D collider;

    private static readonly int Idle = Animator.StringToHash("Enemy Idle");
    private static readonly int Death = Animator.StringToHash("Enemy_Death");

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        collider = gameObject.GetComponent<BoxCollider2D>();
    }

    public int curState;
    private float _lockedUntil;

    private void Update()
    {

    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if(currentHealth <= 0)
        {
            animator.SetTrigger("Dead");
            collider.enabled = false;
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy died!");
        Destroy(gameObject, 0.5f);
    }

}
