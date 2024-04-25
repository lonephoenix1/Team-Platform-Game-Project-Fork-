using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECM2;

public class SprintController : MonoBehaviour
{
    public Character character; // Referencja do skryptu "Character"
    public float sprintMultiplier = 1.5f; // Mno¿nik prêdkoœci sprintu

    private float originalMaxWalkSpeed; // Oryginalna wartoœæ prêdkoœci chodzenia

    private void Start()
    {
        originalMaxWalkSpeed = character.maxWalkSpeed; // Zapamiêtaj oryginaln¹ prêdkoœæ chodzenia
    }

    private void Update()
    {
        // SprawdŸ, czy przycisk LShift jest wciœniêty
        if (Input.GetKey(KeyCode.LeftShift))
        {
            // Jeœli tak, ustaw prêdkoœæ chodzenia na mno¿nik prêdkoœci sprintu
            character.maxWalkSpeed = originalMaxWalkSpeed * sprintMultiplier;
        }
        else
        {
            // Jeœli nie, przywróæ oryginaln¹ prêdkoœæ chodzenia
            character.maxWalkSpeed = originalMaxWalkSpeed;
        }
    }
}

