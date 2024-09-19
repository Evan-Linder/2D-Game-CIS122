using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f; // set move speed

    private PlayerControls playerControls; // input actions object
    private Vector2 movement; // store the player input

    // references to our components
    private Rigidbody2D rb; 
    private Animator myAnimator; 
    private SpriteRenderer mySpriteRender; 

    private void Awake()
    {
        playerControls = new PlayerControls(); // player controls object

        // attach all the componets to our variables.
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
    }
    private void FixedUpdate()
    {
        ChangePlayerDirection();
        Move();  
    }
    private void PlayerInput()
    {
        movement = playerControls.Movement.Move.ReadValue<Vector2>(); // read input from move action

        myAnimator.SetFloat("moveX", movement.x);
        myAnimator.SetFloat("moveY", movement.y);
    }
    private void Move()
    {
        // move the player based on the movement input (current position + movement * move speed)
        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime)); // use delta fixed time to avoid frame rate issues.
    }

    private void ChangePlayerDirection()
    {
        Vector3 mousePos = Input.mousePosition; // set the current position of the mouse on the screen
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position); // convert users position to screen space cord. (used in 2D systems)

        // check if the mouse is to the left of the player
        if (mousePos.x < playerScreenPoint.x)
        {
            mySpriteRender.flipX = true; // flip player to face the left
        }
        else
        {
            mySpriteRender.flipX = false; // flip player to face the right
        }
    }
}
