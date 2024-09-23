using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNavigation : MonoBehaviour
{
    // variables
    [SerializeField] private float moveSpeed = 2f;
    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private KnockBack knockback;

    private void Awake()
    {
        knockback = GetComponent<KnockBack>();
        rb = GetComponent<Rigidbody2D>(); 
    }
    
    private void FixedUpdate()
    {
        if (knockback.gettingKnockedBack) { return; }
        rb.MovePosition(rb.position + moveDirection * (moveSpeed * Time.fixedDeltaTime)); // move enemy
    }

    public void MoveTo(Vector2 targetPosition)
    {
        moveDirection = targetPosition; // set move direction to targeted position
    }
}
