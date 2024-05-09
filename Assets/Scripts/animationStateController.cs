using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationStateController : MonoBehaviour
{
    Animator animator;

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
        bool jumpPressed = Input.GetKeyDown(KeyCode.Space); // SprawdŸ, czy spacja zosta³a naciœniêta

        // Jeœli wciœniêty jest klawisz W, A, S, D
        if (!isWalking && movePressed)
        {
            animator.SetBool("isWalking", true);
        }
        // Jeœli gracz nie wciska przycisku W, A, S, D
        if (isWalking && !movePressed)
        {
            animator.SetBool("isWalking", false);
        }
        // Jeœli gracz wciska klawisz W, A, S, D i lewy shift
        if (!isRunning && (movePressed && runPressed))
        {
            animator.SetBool("isRunning", true);
        }
        // Jeœli gracz nie wciska przycisku W, A, S, D lub lewy shift
        if (isRunning && (!movePressed || !runPressed))
        {
            animator.SetBool("isRunning", false);
        }
        // Jeœli gracz naciœnie spacjê i nie jest ju¿ w trakcie skoku
        if (jumpPressed && !animator.GetBool("isJumping"))
        {
            animator.SetBool("isJumping", true); // Ustaw wartoœæ "isJumping" na true
        }
        else
        {
            animator.SetBool("isJumping", false); // Ustaw wartoœæ "isJumping" na false, jeœli gracz nie nacisn¹³ spacjê
        }
    }
}

