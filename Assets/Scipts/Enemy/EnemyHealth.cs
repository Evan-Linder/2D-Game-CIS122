using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    [SerializeField] private int startingHealth = 3;


    private int currentHealth;
    private KnockBack knockback;
    private Flash flash;
    private Animator myAnimator;


    private void Start()
    {
        currentHealth = startingHealth; // set enemy health to starting health
    }

    private void Awake()
    {
        flash = GetComponent<Flash>();  
        knockback = GetComponent<KnockBack>();
        myAnimator = GetComponent<Animator>();
    }


    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Subtract the damage from the current health.
        knockback.GetKnockedBack(PlayerController.Instance.transform, 15f);
        StartCoroutine(flash.FlashRoutine()); // calll detect death from here instead.

    }

    public void DetectDeath()
    {
        myAnimator.SetTrigger("Die"); // set the "Die" trigger to play the death animation.
        Invoke("DestroyEnemy", 1f); // wait for the animation to finish before destroying.
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject); // Destroy the enemy GameObject after animation.
    }
}


