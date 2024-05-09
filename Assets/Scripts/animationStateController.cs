using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    Animator animator;
    bool isJumping = false;
    bool isLifting = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        bool isRunning = animator.GetBool("isRunning");
        bool isWalking = animator.GetBool("isWalking");
        bool movePressed = Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("d") || Input.GetKey("s");
        bool runPressed = Input.GetKey("left shift");
        bool jumpPressed = Input.GetKeyDown(KeyCode.Space);
        bool liftPressed = Input.GetKeyDown(KeyCode.E); // Check for 'E' key press

        // Check if currently in lifting animation state
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Lifting"))
        {
            // Disable movement and jumping while lifting
            movePressed = false;
            runPressed = false;
            jumpPressed = false;
        }

        if (liftPressed)
        {
            // Trigger the lifting animation
            animator.SetTrigger("isLifting");
        }

        // Jump logic (only allow if not lifting)
        if (!animator.GetCurrentAnimatorStateInfo(0).IsTag("Lifting"))
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
            }
            if (!Input.GetKey(KeyCode.Space))
            {
                isJumping = false;
            }
        }

        // Movement animations (only allow if not lifting)
        if (!animator.GetCurrentAnimatorStateInfo(0).IsTag("Lifting"))
        {
            if (!isWalking && movePressed)
            {
                animator.SetBool("isWalking", true);
            }
            if (isWalking && !movePressed)
            {
                animator.SetBool("isWalking", false);
            }
            if (!isRunning && (movePressed && runPressed))
            {
                animator.SetBool("isRunning", true);
            }
            if (isRunning && (!movePressed || !runPressed))
            {
                animator.SetBool("isRunning", false);
            }
        }
    }
}






