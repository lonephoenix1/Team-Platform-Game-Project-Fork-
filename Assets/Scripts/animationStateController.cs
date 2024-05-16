using ECM2;
using ECM2.Examples.PlanetWalk;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    Animator animator;
    bool isJumping = false;
    bool isLifting = false;
    bool isGathering = false; // Flag to track if gathering action is in progress
    bool isCrouching = false; // Flag to track if crouching action is in progress
    bool isCrouchIdle = false; // Flag to track if crouch idle action is in progress
    public ECM2.Examples.ThirdPerson.ThirdPersonController move;
    public Character myRb;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Coroutine function for waiting before resuming movement after lifting an object
    IEnumerator LiftWait()
    {
        yield return new WaitForSeconds(2.2f);
        move.enabled = true; // Re-enable character movement
        myRb.enabled = true; // Re-enable character physics
        isGathering = false; // Reset gathering flag after the action is completed
    }

    // Coroutine function for enabling jump after a delay
    IEnumerator EnableJumpAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        myRb.canEverJump = true;
    }

    void Update()
    {
        // Retrieve the current animation state information
        AnimatorStateInfo currentStateInfo = animator.GetCurrentAnimatorStateInfo(0);

        // Retrieve input states for various character actions
        bool isRunning = animator.GetBool("isRunning");
        bool isWalking = animator.GetBool("isWalking");
        bool movePressed = Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("d") || Input.GetKey("s");
        bool runPressed = Input.GetKey("left shift");
        bool jumpPressed = Input.GetKeyDown(KeyCode.Space);
        bool liftPressed = Input.GetKeyDown(KeyCode.E);
        bool crouchPressed = Input.GetKey(KeyCode.LeftControl) && movePressed;
        bool crouchIdlePressed = Input.GetKey(KeyCode.LeftControl) && !movePressed;
        bool crouchHeld = Input.GetKey(KeyCode.LeftControl);

        // Check if the character is currently holding or lifting an object
        isLifting = currentStateInfo.IsTag("Lifting");

        // Check if the character is currently jumping (mid-air)
        bool isMidAir = !myRb.IsGrounded();

        // Block certain actions if the character is holding, lifting, or mid-air
        if (isLifting || isMidAir)
        {
            movePressed = false;
            runPressed = false;
            jumpPressed = false;
            crouchPressed = false;
            crouchIdlePressed = false;
            crouchHeld = false;
        }

        // Handle lifting an object
        if (liftPressed && !isLifting && !isGathering && !isMidAir)
        {
            animator.SetTrigger("isLifting");
            move.enabled = false;
            myRb.enabled = false;
            isGathering = true; // Set gathering flag to prevent multiple triggers
            StartCoroutine(LiftWait()); // Start coroutine to wait before resuming movement
        }

        // Handle character jumping
        if (!isLifting && !isMidAir)
        {
            if (jumpPressed && !isJumping)
            {
                isJumping = true;
                if (isRunning)
                {
                    animator.SetTrigger("isRunningJump");
                }
                else if (isWalking)
                {
                    animator.SetTrigger("isWalkingJump");
                }
                else
                {
                    animator.SetTrigger("isIdleJump");
                }
                myRb.Jump(); // Trigger the physical jump
            }
            if (!Input.GetKey(KeyCode.Space))
            {
                isJumping = false; // Reset jumping flag if jump key is released
            }
        }

        // Handle crouching animations
        if (!isLifting && !isJumping && !isMidAir)
        {
            // Handle crouch walking
            if (crouchPressed && !isCrouching)
            {
                isCrouching = true;
                animator.SetBool("isCrouching", true);
                myRb.canEverJump = false; // Disable jumping while crouching
                animator.SetBool("Moving", movePressed); // Set Moving based on player movement
            }
            else if (!crouchHeld && isCrouching)
            {
                isCrouching = false;
                animator.SetBool("isCrouching", false);
                animator.SetBool("Moving", false);
                StartCoroutine(EnableJumpAfterDelay(0.4f)); // Enable jumping after 0.4 seconds
            }

            // Handle crouch idle
            if (crouchIdlePressed && !isCrouchIdle)
            {
                isCrouchIdle = true;
                animator.SetBool("isCrouchIdle", true);
                animator.SetBool("Moving", false);
                myRb.canEverJump = false; // Disable jumping while crouch idle
            }
            else if (!crouchHeld && isCrouchIdle)
            {
                isCrouchIdle = false;
                animator.SetBool("isCrouchIdle", false);
  
                animator.SetBool("Moving", movePressed); // Set Moving based on player movement
                StartCoroutine(EnableJumpAfterDelay(0.4f)); // Enable jumping after 0.4 seconds
            }
        }


        // Handle movement animations while not crouching
        if (!isCrouching && !isCrouchIdle && !isMidAir && !isJumping)
            {
                if (!isWalking && movePressed)
                {
                    animator.SetBool("isWalking", true); // Enable walking animation
                }
                if (isWalking && !movePressed)
                {
                    animator.SetBool("isWalking", false); // Disable walking animation
                }
                if (!isRunning && (movePressed && runPressed))
                {
                    animator.SetBool("isRunning", true); // Enable running animation
                }
                if (isRunning && (!movePressed || !runPressed))
                {
                    animator.SetBool("isRunning", false); // Disable running animation
                }
            }
        }
}















