using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator animator;

    public int maxHealth = 100;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }

        Debug.Log("Enemy health" + currentHealth);
    }
    void Die()
    {
        Destroy(this.gameObject);

        animator.SetBool("isDead", true);

        this.enabled = false;
    }
   
}
