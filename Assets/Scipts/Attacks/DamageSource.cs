using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSource : MonoBehaviour
{

    [SerializeField] private int damageAmount = 1;
    private void OnTriggerEnter2D(Collider2D other)
    {
        // check if the object that collided has an EnemyHealth component.
        if (other.gameObject.GetComponent<EnemyHealth>())
        {
            // get the component from the collided object.
            EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();

            // Call the method on the enemy and pass the damage amount.
            enemyHealth.TakeDamage(damageAmount);
        }
    }
}


