using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f; // Set move speed
    [SerializeField] private float dashSpeed = 1f; // Set dash speed
    public static PlayerController Instance; // create a instance of the playercontroller


    private PlayerControls playerControls;
    private Vector2 movement; // store the player input
    private bool isDashing = false;
    private float originalMoveSpeed; // To store the original move speed

    // references to our components
    private Rigidbody2D rb;
    private Animator myAnimator;
    private SpriteRenderer mySpriteRender;

    private BasicAttack basicAttack;

    private void Awake()
    {

        Instance = this;
        playerControls = new PlayerControls(); // player controls object

        // Attach all the components to our variables
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        mySpriteRender = GetComponent<SpriteRenderer>();

        basicAttack = GetComponent<BasicAttack>();
    }

    private void Start()
    {
        playerControls.Combat.Dash.performed += _ => Dash();
    }

    private void OnEnable()
    {
        playerControls.Enable(); // Enable when script is active
    }

    private void Update()
    {
        PlayerInput();

        // Check for attack input and trigger attack if available
        if (playerControls.Combat.basicAttack.triggered && !basicAttack.IsAttacking)
        {
            basicAttack.ExecuteAttack();
        }
    }

    private void FixedUpdate()
    {
        ChangePlayerDirection();
        Move();
    }

    private void PlayerInput()
    {
        movement = playerControls.Movement.Move.ReadValue<Vector2>(); //read input from move action

        myAnimator.SetFloat("moveX", movement.x);
        myAnimator.SetFloat("moveY", movement.y);
    }

    private void Move()
    {
        // Only move if the player is not attacking
        if (!basicAttack.IsAttacking)
        {
            rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime)); // use fixed delta time to avoid frame rate issues
        }
    }

    private void ChangePlayerDirection()
    {
        Vector3 mousePos = Input.mousePosition; // set the current position of the mouse on the screen
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position); // convert player's position to screen space coordinates

        // check if the mouse is to the left of the player
        mySpriteRender.flipX = mousePos.x < playerScreenPoint.x; //flip player to face the left or right
    }

    private void Dash()
    {
        if (!isDashing)
        {
            isDashing = true;
            originalMoveSpeed = moveSpeed; // Store the original speed
            moveSpeed += dashSpeed;

            myAnimator.SetBool("isDashing", true); // Set the isDashing boolean to true
            StartCoroutine(EndDashRoutine());
        }
    }

    private IEnumerator EndDashRoutine()
    {
        float dashTime = .2f;
        float dashCD = .25f;
        yield return new WaitForSeconds(dashTime);

        moveSpeed = originalMoveSpeed; // Reset to original speed

        myAnimator.SetBool("isDashing", false); // Set the isDashing boolean to false
        yield return new WaitForSeconds(dashCD);

        isDashing = false;
    }
}



