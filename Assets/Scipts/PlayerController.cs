using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f; // set move speed

    private PlayerControls playerControls; // input actions object
    private Vector2 movement; // store the player input
    private Rigidbody2D rb; // reference rb componet

    private void Awake()
    {
        playerControls = new PlayerControls(); // player controls object
        rb = GetComponent<Rigidbody2D>(); // get rb component attached to player 
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
        Move();  
    }
    private void PlayerInput()
    {
        movement = playerControls.Movement.Move.ReadValue<Vector2>(); // read input from move action
    }
    private void Move()
    {
        // move the player based on the movement input (current position + movement * move speed)
        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime)); // use delta fixed time to avoid frame rate issues.
    }
}
