using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    public Animator anim;
    public PlayerAnimationController controller;
    public bool attacking;

    public Transform attackPoint;
    public LayerMask enemyLayers;

    public float attackRange = 0.5f;
    public int attackDamage = 100;

    public float attackRate = 2f;
    float nextAttackTime = 0f;

    private AudioSource punch;

    private void Start()
    {
        attacking = false;
        punch = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        attacking = false;
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
                punch.Play();
            }
        }
    }

    void Attack()
    {
        anim.SetTrigger("Attack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}
