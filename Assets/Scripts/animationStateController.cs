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
    public ECM2.Examples.ThirdPerson.ThirdPersonController move;
    public Character myRb;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    IEnumerator LiftWait()
    {
        yield return new WaitForSeconds(2.2f);
        move.enabled = true;
        myRb.enabled = true;
    }

    void Update()
    {
        // Pobieranie aktualnych informacji o animacji
        AnimatorStateInfo currentStateInfo = animator.GetCurrentAnimatorStateInfo(0);

        bool isRunning = animator.GetBool("isRunning");
        bool isWalking = animator.GetBool("isWalking");
        bool movePressed = Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("d") || Input.GetKey("s");
        bool runPressed = Input.GetKey("left shift");
        bool jumpPressed = Input.GetKeyDown(KeyCode.Space);
        bool liftPressed = Input.GetKeyDown(KeyCode.E);

        isLifting = currentStateInfo.IsTag("Lifting");

        if (isLifting)
        {
            movePressed = false;
            runPressed = false;
            jumpPressed = false;
        }

        if (liftPressed && !isLifting)
        {
            animator.SetTrigger("isLifting");
            move.enabled = false;
            myRb.enabled = false;
            StartCoroutine(LiftWait());
        }

        // Logika skoku 
        if (!isLifting)
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

        // Animacje ruchu
        if (!isLifting)
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









