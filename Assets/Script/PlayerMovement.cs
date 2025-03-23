using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    ///<summary>
    ///Author:Kehan Gong 
    ///Date:2025-02-06
    ///Description:Player Movement
    ///</summary>
    public enum PlayerState
    {
        Idle,
        Normal,
        SpeedUp,
    }

    public float moveSpeed = 5f;
    public float rotationSpeed = 100f;
    public float speedMultiplier = 1f;
    
    public Animator animator;
    public PlayerState playerState = PlayerState.Idle;

    private Rigidbody2D rb;
    private Vector2 moveDirection;
        private SpriteRenderer spriteRenderer;  // Add this at class level

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();  // Add this line
        
        if(animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

       void Update()
    {
        //player input in WASD
        float moveInputX = Input.GetAxis("Vertical");    // W/S
        float moveInputY = Input.GetAxis("Horizontal");  // A/D

        MoveBoat(moveInputX, moveInputY);

        //if(Input.GetKeyDown(KeyCode.Space))
        //{
        //     SwitchMovementMode();
        //}
    }

    void MoveBoat(float moveX, float moveY)
    {
        switch(playerState)
        {
            case PlayerState.Idle:
                speedMultiplier = 1f;
                break;
            case PlayerState.Normal:
                speedMultiplier = 2f;
                break;
            case PlayerState.SpeedUp:
                speedMultiplier = 3f;
                break;
        }

        // Calculate the direction of the movement according to the input
        moveDirection = new Vector2(moveY, moveX).normalized;  // Swapped and negated for correct 2D movement

        if(moveY != 0)
        {
            spriteRenderer.flipX = (moveX > 0);
        }

        // According to the direction and speed to move the boat
        rb.velocity = moveDirection * moveSpeed * speedMultiplier;
        
        
        if(moveDirection != Vector2.zero)
        {
            // Calculate the rotation of the boat according to the direction
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg + 180f;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

// ... rest of existing code ...


    void SwitchMovementMode()
    {
        switch (playerState)
        {
            case PlayerState.Idle:
                playerState = PlayerState.Normal;
                break;
            case PlayerState.Normal:
                playerState = PlayerState.SpeedUp;
                break;
            case PlayerState.SpeedUp:
                playerState = PlayerState.Idle;
                break;
        }
    }
}