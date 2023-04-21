using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    //public float speed;
    //public float rotationSpeed;
    //public float jumpSpeed;
    //public float jumpButtonGracePeriod;

    private Animator animator;
    //private CharacterController characterController;
    //private float ySpeed;
    //private float originalStepOffSet;
    //private float? lastGroundedTime;
    //private float? jumpButtonPressedTime;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        bool isRunning = animator.GetBool("IsRunning");
        bool forwardPressed = Input.GetKey("w");
        bool rightPressed = Input.GetKey("d");
        bool leftPressed = Input.GetKey("a");

        //if player presses w key
        if (!isRunning && forwardPressed | rightPressed | leftPressed)
        {
            //set the isWalking boolean to be true
            animator.SetBool("IsRunning", true);
        }
        if (isRunning && !forwardPressed && !rightPressed && !leftPressed) //if player is not pressing w
        {
            //set IsRunning to false
            animator.SetBool("IsRunning", false);
        }
        
    }
}
