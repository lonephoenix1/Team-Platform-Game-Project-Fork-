using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECM2;


public class SprintController : MonoBehaviour
{
    public Character character; // Referencja do skryptu "Character"
    private float originalmaxWalkSpeed; // Oryginalna wartoœæ prêdkoœci chodzenia

    private void Start()
    {
        originalmaxWalkSpeed = character.maxWalkSpeed; // Zapamiêtaj oryginaln¹ prêdkoœæ chodzenia
    }

    private void Update()
    {
        // SprawdŸ, czy przycisk LShift jest wciœniêty
        if (Input.GetKey(KeyCode.LeftShift))
        {
            // Jeœli tak, ustaw prêdkoœæ chodzenia na 1.5x oryginalnej wartoœci
            character.maxWalkSpeed = originalmaxWalkSpeed * 1.5f;
        }
        else
        {
            // Jeœli nie, przywróæ oryginaln¹ prêdkoœæ chodzenia
            character.maxWalkSpeed = originalmaxWalkSpeed;
        }
    }
}
