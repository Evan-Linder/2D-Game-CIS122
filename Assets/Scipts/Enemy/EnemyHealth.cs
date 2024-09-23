using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    [SerializeField] private int startingHealth = 3;


    private int currentHealth;
    private KnockBack knockback;
    private Flash flash;


    private void Start()
    {
        currentHealth = startingHealth; // set enemy health to starting health
    }

    private void Awake()
    {
        flash = GetComponent<Flash>();  
        knockback = GetComponent<KnockBack>();
    }


    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Subtract the damage from the current health.
        knockback.GetKnockedBack(PlayerController.Instance.transform, 15f);
        StartCoroutine(flash.FlashRoutine()); // calll detect death from here instead.

    }


    public void DetectDeath()
    {
        if (currentHealth <= 0)
        {

            Destroy(gameObject); // Destroy the enemy GameObject.
        }
    }
}


