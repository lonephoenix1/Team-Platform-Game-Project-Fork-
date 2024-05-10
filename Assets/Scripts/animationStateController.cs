using ECM2;
using ECM2.Examples.PlanetWalk;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    Animator animator; // Reference to the Animator component for managing character animations
    bool isJumping = false; // Flag indicating whether the character is currently performing a jump
    bool isLifting = false; // Flag indicating whether the character is holding or lifting an object
    public ECM2.Examples.ThirdPerson.ThirdPersonController move; // Reference to the script controlling character movement
    public Character myRb; // Reference to the character's Rigidbody component

    void Start()
    {
        // Initialize animator reference
        animator = GetComponent<Animator>();
    }

    // Coroutine function for waiting before resuming movement after lifting an object
    IEnumerator LiftWait()
    {
        // Wait for a specific duration before re-enabling character movement and physics
        yield return new WaitForSeconds(2.2f);
        move.enabled = true; // Re-enable character movement
        myRb.enabled = true; // Re-enable character physics
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

        // Check if the character is currently holding or lifting an object
        isLifting = currentStateInfo.IsTag("Lifting");

        // Block certain actions if the character is holding or lifting an object
        if (isLifting)
        {
            movePressed = false; // Prevent movement input
            runPressed = false; // Prevent running input
            jumpPressed = false; // Prevent jump input
        }

        // Handle lifting an object
        if (liftPressed && !isLifting)
        {
            // Trigger the lifting animation and disable character movement and physics temporarily
            animator.SetTrigger("isLifting");
            move.enabled = false;
            myRb.enabled = false;
            StartCoroutine(LiftWait()); // Start a coroutine to wait before resuming movement
        }

        // Handle character jumping
        if (!isLifting)
        {
            if (jumpPressed && !isJumping)
            {
                // Set the jumping flag to true and trigger the appropriate jump animation based on movement state
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
            }
            if (!Input.GetKey(KeyCode.Space))
            {
                isJumping = false; // Reset the jumping flag if the jump key is released
            }
        }

        // Handle movement animations
        if (!isLifting)
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











