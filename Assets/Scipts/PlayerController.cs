using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f; // Set move speed

    private PlayerControls playerControls; // Input actions object
    private Vector2 movement; // Store the player input

    // references to our components
    private Rigidbody2D rb;
    private Animator myAnimator;
    private SpriteRenderer mySpriteRender;

    private bool isAttacking; // flag to track if the player is currently attacking

    private void Awake()
    {
        playerControls = new PlayerControls(); // Player controls object

        // Attach all the components to our variables
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        mySpriteRender = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        playerControls.Enable(); // enable when script is active
    }

    private void Update()
    {
        PlayerInput();

        // Check for attack input
        if (playerControls.Combat.basicAttack.triggered && !isAttacking)
        {
            Attack(); // Call the Attack method
        }
    }

    private void FixedUpdate()
    {
        ChangePlayerDirection();
        Move();
    }

    private void PlayerInput()
    {
        movement = playerControls.Movement.Move.ReadValue<Vector2>(); // Read input from move action

        myAnimator.SetFloat("moveX", movement.x);
        myAnimator.SetFloat("moveY", movement.y);
    }

    private void Move()
    {
        // Only move if the player is not attacking
        if (!isAttacking)
        {
            // Move the player based on the movement input (current position + movement * move speed)
            rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime)); // use fixed delta time to avoid frame rate issues
        }
    }

    private void ChangePlayerDirection()
    {
        Vector3 mousePos = Input.mousePosition; //set the current position of the mouse on the screen
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position); //convert user's position to screen space coordinates

        //ccheck if the mouse is to the left of the player
        if (mousePos.x < playerScreenPoint.x)
        {
            mySpriteRender.flipX = true; // flip player to face the left
        }
        else
        {
            mySpriteRender.flipX = false; // Flip player to face the right
        }
    }

    private void Attack()
    {
        isAttacking = true; // Set the attacking flag
        myAnimator.SetBool("basicAttack", true); // Set the attack parameter to true
        myAnimator.SetTrigger("basicAttack"); // Trigger the Attack1 animation

        StartCoroutine(ResetAttackState()); // reset 
    }

    private IEnumerator ResetAttackState()
    {
        yield return new WaitForSeconds(1.2f);

        isAttacking = false; // stop attacking
        myAnimator.SetBool("basicAttack", false); // Reset the attack parameter
    }
}


