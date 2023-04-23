using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationAndMovementController : MonoBehaviour
{
    //declare reference variables
    PlayerInput playerInput; //NOTE: PlayerInput class must be generated from New Input System in Inspector
    CharacterController characterController;
    Animator animator;
    
    //variables to store player input values
    Vector2 currentMovementInput;
    Vector3 currentMovement;
    bool isMovementPressed;

    //constants 
    float rotationFactorPerFrame = 10f;
    public float speed;//used to have Michelle move faster

    //gravity variables
    float gravity = -9.8f;
    float groundedGravity = -0.5f;
    //arcGravity to solve problem of character needing to fall faster rather than floating in air
    float arcGravity = 28.0f; 

    //jumping variables
    bool isJumpPressed = false;
    float initialJumpVelocity;
    float maxJumpHeight = 400f;
    float maxJumpTime = 60f;
    bool isJumping = false;

    //Awake is called earlier than Start in Unity's event life cycle
    void Awake()
    {
        //initially set reference variables
        playerInput = new PlayerInput();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();


        //poor coding practices because of repeated code - struggled with debugging here 
        playerInput.CharacterControls.Move.started += context =>
        {
            currentMovementInput = context.ReadValue<Vector2>();
            currentMovement.x = currentMovementInput.x * speed; //ChatGPT helped me figure out that multiple this by speed increases the running speed of Michelle
            currentMovement.z = currentMovementInput.y * speed; //ChatGPT helped me figure out that multiple this by speed increases the running speed of Michelle
            isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
        };

        playerInput.CharacterControls.Move.canceled += context =>
        {
            currentMovementInput = context.ReadValue<Vector2>();
            currentMovement.x = currentMovementInput.x;
            currentMovement.z = currentMovementInput.y;
            isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
        };

        //jumped callbacks
        playerInput.CharacterControls.Jump.started += context =>
        {
            isJumpPressed = context.ReadValueAsButton();
        };

        playerInput.CharacterControls.Jump.canceled += context =>
        {
            isJumpPressed = context.ReadValueAsButton();
        };

        setupJumpVariables();

        //playerInput.CharacterControls.Jump.canceled += onJump;
    }

    void setupJumpVariables()
    {
        float timeToApex = maxJumpTime / 2;
        gravity = (-2 * maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        initialJumpVelocity = (2 * maxJumpHeight) / timeToApex;
    }

    //jumping function 
    void handleJump()
    {
        if(!isJumping && characterController.isGrounded && isJumpPressed)
        {
            //set animator here
            animator.SetBool("isJumping", true);
            isJumpPressed = true;
            currentMovement.y = initialJumpVelocity * 0.5f;
        } else if (!isJumpPressed && isJumping && characterController.isGrounded)
        {
            isJumping = false;
        }

        

        // Apply gravity force
        if (!characterController.isGrounded)
        {
            // Apply arc gravity to create parabolic arc fall
            currentMovement.y -= arcGravity * Time.deltaTime;
        }
    }

    void handleRotation()
    {
        Vector3 positionToLookAt;
        //the change in position our character should point to 
        positionToLookAt.x = currentMovement.x;
        positionToLookAt.y = 0.0f;
        positionToLookAt.z = currentMovement.z;

        //the current rotation of our character
        Quaternion currentRotation = transform.rotation;

        if (isMovementPressed)
        {
            //creates a new rotation based on where the player is currently pressing
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactorPerFrame * Time.deltaTime);
        }

    }

    //control the animations of switching between animation states 
    void handleAnimation()
    {
        bool isRunning = animator.GetBool("isRunning");

        if (isMovementPressed)
        {
            animator.SetBool("isRunning", true);
        }
        else if (!isMovementPressed)
        {
            animator.SetBool("isRunning", false);
        }
    }

    void handleGravity()
    {
        bool isFalling = currentMovement.y <= 0.0f || !isJumpPressed;
        float fallMultiplier = 2.0f;

        if (characterController.isGrounded)
        {
            animator.SetBool("isJumping", false);
            currentMovement.y = groundedGravity;
        } else if (isFalling) { //make the fall more realistic with arc of dist vs time 
            float previousYVelocity = currentMovement.y;
            float newYVelocity = currentMovement.y + (gravity * fallMultiplier * Time.deltaTime);
            float nextYVelocity = (previousYVelocity + newYVelocity) * 0.5f;
            currentMovement.y = nextYVelocity;
        }
        else
        {
            float previousYVelocity = currentMovement.y;
            float newYVelocity = currentMovement.y + (gravity * Time.deltaTime);
            float nextYVelocity = (previousYVelocity + newYVelocity) * 0.5f;
            currentMovement.y = nextYVelocity;
        }
    }

    // Update is called once per frame
    void Update()
    { 
        handleRotation();
        handleAnimation();
        characterController.Move(currentMovement * Time.deltaTime);

        handleGravity();
        handleJump();
    }

    //enable and disable functions used for character to move in game mode
    void OnEnable()
    {
        //enable the character controls action map
        playerInput.CharacterControls.Enable();
    }

    void OnDisable()
    {
        playerInput.CharacterControls.Disable();
    }
}