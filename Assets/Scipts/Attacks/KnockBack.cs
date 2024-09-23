using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    public bool gettingKnockedBack { get; private set; }

    [SerializeField] private float knockBackTime = .2f; //duration of knockback

    private Rigidbody2D rb; 

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); 
    }

    public void GetKnockedBack(Transform damageSource, float knockBackThrust)
    {
        gettingKnockedBack = true; // Set knockback state to true
        Vector2 difference = (transform.position - damageSource.position).normalized * knockBackThrust * rb.mass; // calculate knockback direction and force
        rb.AddForce(difference, ForceMode2D.Impulse); //apply knockback force
        StartCoroutine(KnockRoutine()); 
    }

    private IEnumerator KnockRoutine()
    {
        yield return new WaitForSeconds(knockBackTime); // Wait for knockback duration
        rb.velocity = Vector2.zero; // stop the Rigidbody movement
        gettingKnockedBack = false; 
    }
}

