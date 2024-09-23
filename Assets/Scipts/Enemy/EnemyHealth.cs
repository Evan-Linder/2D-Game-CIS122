using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    [SerializeField] private int startingHealth = 3;


    private int currentHealth;


    private void Start()
    {
        currentHealth = startingHealth; // set enemy health to starting health
    }


    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Subtract the damage from the current health.
        Debug.Log(currentHealth); // Output the current health to the console for debugging.
        DetectDeath(); // Check if the enemy should be destroyed.
    }


    private void DetectDeath()
    {
        if (currentHealth <= 0)
        {
            Destroy(gameObject); // Destroy the enemy GameObject.
        }
    }
}


