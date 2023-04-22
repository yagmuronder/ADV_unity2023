using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationAndMovementController : MonoBehaviour
{
    //declare reference variables
    PlayerInput playerInput;
    CharacterController characterController;
    Animator animator;
    public float speed;//if possible to make player run faster

    Vector2 currentMovementInput;
    Vector3 currentMovement;
    bool isMovementPressed;
    float rotationFactorPerFrame = 10f;

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
        if (characterController.isGrounded)
        {
            float groundedGravity = -0.5f;
            currentMovement.y = groundedGravity;
        }
        else
        {
            float gravity = -9.8f;
            currentMovement.y += gravity;
        }
    }

    // Update is called once per frame
    void Update()
    {
        handleRotation();
        handleAnimation();
        characterController.Move(currentMovement * Time.deltaTime);
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

