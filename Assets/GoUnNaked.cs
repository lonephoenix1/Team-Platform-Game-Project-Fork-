using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GoUnNaked : MonoBehaviour
{
    public GameObject character; // Referencja do obiektu postaci
    public GameObject[] elementsToDisable; // Tablica elementów do wy³¹czenia

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // SprawdŸ, czy obiekt w triggerze to postaæ
        {
            foreach (GameObject element in elementsToDisable)
            {
                element.SetActive(true); // Wy³¹cz ka¿dy element z tablicy
            }
        }
    }
}

