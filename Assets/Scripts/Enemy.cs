using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Animator e_animator; 

    public int maxHealth = 100;
    private int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        e_animator = GetComponent<Animator>();
        currentHealth = maxHealth;    
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Hurt Animation
        e_animator.SetTrigger("Hurt");

    }

    public void EnemyDeath()
    {
        if(currentHealth <= 0 )
        {
            Debug.Log("Enemy dead");

            // Die Animation
            e_animator.SetTrigger("Death");

            // Disable enemy
            
            GetComponent<Collider2D>().enabled = false;
            this.enabled = false;
        }
    }
}
