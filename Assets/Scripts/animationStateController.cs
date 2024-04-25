using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationStateController : MonoBehaviour
{
    Animator animator;
    bool isJumping = false;

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




